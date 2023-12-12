using Application.DTOs;
using Application.DTOs.TableManagementDtos;
using Application.Implementations.Modules.MenuSettings.Services;
using Application.Interfaces.Modules.MenuSettings.Repositories;
using Application.Interfaces.Modules.RolePermission.Repository;
using Application.Interfaces.Repositories.Modules.Users.IRepository;
using Application.Interfaces;
using Application.Interfaces.Services.Modules.TableManagement.Services;
using Domain.Domain.Modules.Tables;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Repositories.Modules.Table.Repositories;
using Domain.Domain.Modules.MenuSettings;
using System.ComponentModel.Design;
using System.Net.NetworkInformation;
using ZXing;
using QRCoder;
using ZXing.QrCode.Internal;
using Application.Interfaces.Repositories.Modules.MenuSettings.Repositories;
using Application.DTOs.MenuSettingDtos;
using Application.DTOs.TicketDtos;
using ZXing.Rendering;
using System.Drawing.Imaging;
using System.Drawing;
using ZXing.QrCode;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities.Enums;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Application.Exceptions;

namespace Application.Implementations.Modules.TableManagement.Services
{
    public class TableService : ITableService
    {
        private readonly ITableRepository _tableRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IIdentityService _identityService;
        private readonly IUserRepository _userRepository;
        private readonly IRolePermissionsRepository _rolePermissionsRepository;
        private readonly ILogger<TableService> _logger;
        private readonly IMemoryCache _memoryCache;

        public TableService(ITableRepository tableRepository, IMenuRepository menuRepository, IIdentityService identityService, IUserRepository userRepository, IRolePermissionsRepository rolePermissionsRepository, ILogger<TableService> logger, IMemoryCache memoryCache)
        {
            _tableRepository = tableRepository;
            _menuRepository = menuRepository;
            _identityService = identityService;
            _userRepository = userRepository;
            _rolePermissionsRepository = rolePermissionsRepository;
            _logger = logger;
            _memoryCache = memoryCache;
        }

        
        public async Task<BaseResponse<List<Table>>> AddTablesPreviewAsync(string userToken, string branchName, CreateTablesRequestModel request)
        {
            //Get logged in user
            var claims = _identityService.ValidateToken(userToken);
            var email = claims.SingleOrDefault(c => c.Type == "email");

            var user = await _userRepository.GetUserByEmail(email.Value);


            if (user == null)
            {
                _logger.LogError("User not authenticated");
                return new BaseResponse<List<Table>>
                {
                    Message = "User not authenticated!",
                    Status = false
                };
            }

            // check if user is verified
            if (!user.EmailConfirmed) throw new BadRequestException("User unverified");

            if (user.UserType == Domain.Entities.Enums.UserType.Employee)
            {
                var userHasPermission = await _rolePermissionsRepository.UserHasPermissionAsync(user.Id, "Table Ordering");

                if (!userHasPermission)
                {
                    _logger.LogError("Unauthorized!");
                    return new BaseResponse<List<Table>>
                    {
                        Message = "Unauthorized!",
                        Status = false
                    };
                }
            }

            if (request is null)
            {
                _logger.LogError("Fields cannot be empty!");
                return new BaseResponse<List<Table>>
                {
                    Message = "Fields cannot be empty!",
                    Status = false
                };
            }

            // get existing tables belonging to branch and company
            var existingTables = user.UserType == UserType.SuperAdmin
                ? await _tableRepository.GetAllTablesByBranchAndCompanyNameAsync(user.BusinessName, branchName)
                : await _tableRepository.GetAllTablesByBranchAndCompanyNameAsync(user.CreatedBy, branchName);

            int startingTableNumber = existingTables.Count + 1;

            var previewTables = new List<Table>();
            int tablesCount = 0;

            for (int i = 0; i < request.TableNumber; i++)
            {
                var table = new Table
                {
                    TableNumber = (startingTableNumber + i).ToString("D2"),
                    TableId = GenerateAlphanumericTableId(startingTableNumber + i),
                    CompanyName = user.UserType == UserType.SuperAdmin ? user.BusinessName : user.CreatedBy,
                    CreatedOn = DateTime.UtcNow,
                    CreatedBy = user.UserType == UserType.SuperAdmin ? user.BusinessName : $"{user.Name} {user.LastName}",
                    QrCode = "dhgsfdgfshgs"
                };

                previewTables.Add(table);
                tablesCount++;
            }

            // set cache key
            var cacheKey = $"PreviewTables_{user.Id}";

            // store preview tables to in-memory cache
            CacheData(cacheKey, previewTables);

            if (tablesCount > 0)
            {
                _logger.LogInformation("Tables(s) previewed.");
                return new BaseResponse<List<Table>>
                {
                    Message = $"{tablesCount} tables(s) previewed.",
                    Status = true,
                    Data = previewTables
                };
            }

            _logger.LogWarning("No tables to preview.");
            return new BaseResponse<List<Table>>
            {
                Message = "No tables were preview.",
                Status = false
            };

            


        }

        public async Task<BaseResponse<List<Table>>> SaveTablesAsAsync(string userToken, string branchName)
        {
            // Get logged in user
            var claims = _identityService.ValidateToken(userToken);
            var email = claims.SingleOrDefault(c => c.Type == "email");
            var user = await _userRepository.GetUserByEmail(email.Value);

            if (user == null)
            {
                _logger.LogError("User not authenticated");
                return new BaseResponse<List<Table>>
                {
                    Message = "User not authenticated!",
                    Status = false
                };
            }

            // set key to retrieve cache data
            var cacheKey = $"PreviewTables_{user.Id}";

            // Retrieve from the in-memory cache
            var previewTables = RetrieveCachedData(cacheKey);

            if (previewTables == null || !previewTables.Any())
            {
                _logger.LogWarning("No previewed tables found in the cache.");
                return new BaseResponse<List<Table>>
                {
                    Message = "No previewed tables found in the cache.",
                    Status = false
                };
            }
            
            int tableCount = 0;
            var newTables = new List<Table>();
            foreach (var previewTable in previewTables)
            {
                previewTable.BranchName = branchName;
                    
                var saveTables = await _tableRepository.SaveTableAs(previewTable);
                if (saveTables is null)
                {
                    _logger.LogError("Tables(s) couldn't be added to the branch name.");
                    return new BaseResponse<List<Table>>
                    {
                        Message = "Tables(s) couldn't be added to the branch name.",
                        Status = false
                    };
                }
                newTables.Add(previewTable);
                tableCount++;
            }
            
     
            if (tableCount > 0)
            {
                _logger.LogInformation($"{tableCount} tables(s) saved with SaveAsName: {branchName}.");
                return new BaseResponse<List<Table>>
                {
                    Message = $"{tableCount} tables(s) saved with SaveAsName: {branchName}.",
                    Status = true,
                    Data = newTables
                };
            }

            // remove the current key
            // from in-memory after its data
            // has been saved successfully 
            _memoryCache.Remove(cacheKey);

            _logger.LogWarning("No tables were saved.");
            return new BaseResponse<List<Table>>
            {
                Message = "No tables were saved.",
                Status = false
            };


        }


        public async Task<BaseResponse<bool>> DeleteTableAsync(Guid Id)
        {
            var getTable = await _tableRepository.GetTableByIdAsync(Id);
            if (getTable is null)
            {
                _logger.LogError("Table does not exist.");
                return new BaseResponse<bool>
                {
                    Message = "Table does not exist.",
                    Status = false
                };
            }

            var result = await _tableRepository.DeleteTable(getTable);

            if (!result)
            {
                _logger.LogError("Table couldn't be deleted.");
                return new BaseResponse<bool>
                {
                    Message = $"Table couldn't be deleted.",
                    Status = false,

                };
            }

            _logger.LogError("Table deleted successfully.");
            return new BaseResponse<bool>
            {
                Message = $"Table deleted successfully",
                Status = true,

            };
        }

        public async Task<BaseResponse<bool>> EnableOrDisableTable(Guid Id, bool isActive)
        {
            // get the table to disable or enable by Id
            var getTable = await _tableRepository.GetTableByIdAsync(Id);

            if (getTable is null)
            {
                _logger.LogError("Table does not exist.");
                return new BaseResponse<bool>
                {
                    Message = "Table does not exist.",
                    Status = false
                };
            }

            // enable or disable table based on request
            getTable.SetTableToActiveOrInActiveStatus(isActive);

            // update the database for the changes
            var result = await _tableRepository.EditTableAsync(getTable);

            if (result is null)
            {
                if (!isActive)
                {
                    _logger.LogError("Table couldn't be disabled.");
                    return new BaseResponse<bool>
                    {
                        Message = $"Table couldn't be disabled.",
                        Status = false,

                    };
                }

                _logger.LogError("Table couldn't be enabled.");
                return new BaseResponse<bool>
                {
                    Message = $"Table couldn't be enabled.",
                    Status = false,

                };

            }
            else
            {
                if (isActive)
                {
                    _logger.LogError("Table enabled successfully.");
                    return new BaseResponse<bool>
                    {
                        Message = $"Table enabled successfully",
                        Status = true,

                    };
                }

                _logger.LogError("Table disabled successfully.");
                return new BaseResponse<bool>
                {
                    Message = $"Table disabled successfully",
                    Status = true,

                };
            }
            
            
        }

        
        public async Task<BaseResponse<TableDto>> GetTableByIdAsync(Guid Id)
        {
            var table = await _tableRepository.GetTableByIdAsync(Id);
            if (table is null)
            {
                _logger.LogError("Table couldn't be fetched");
                return new BaseResponse<TableDto>
                {
                    Message = $"Table couldn't be fetched",
                    Status = false,

                };
            }

            var tablesDto = new TableDto
            {
                Id = table.Id,
                TableNumber = table.TableNumber,
                TableId = table.TableId,
                QrCode = table.QrCode,
                Status = table.Status,
                IsActive = table.IsActive,
                BranchName = table.BranchName,
                CreatedBy = table.CreatedBy,
                CreatedOn = table.CreatedOn,
                ModifiedOn = table.LastModifiedOn,
                CompanyName = table.CompanyName,
                Guests = table.Guests.Select(tn => new GuestDto
                {
                    Id = tn.Id,
                    NumberOfGuest = tn.NumberOfGuest,
                    TableId = tn.TableId,
                    
                }).ToList(),
                Tabs = table.Tabs.Select(tb => new TabDto
                {
                    TabId = tb.Id,
                    TabName = tb.TabName,
                    TableId = tb.TableId,
                    Orders = tb.Orders.Select(o => new OrderDto
                    {
                        OrderNumber = o.OrderNumber,
                        TabId = o.TabId,
                        Channel = o.Channel,
                        CustomerName = o.CustomerName,
                        CustomerReferenceNumber = o.CustomerReferenceNumber,
                        WaiterName = o.WaiterName,
                        Status = o.Status,
                        Bill = o.Bill,
                        Tip = o.Tip,
                        OrderItems = o.OrderItems.Select(oi => new OrderItemDto
                        {
                            Id = oi.Id,
                            OrderId = oi.OrderId,
                            MenuItemId = oi.MenuItemId,
                            MenuItemName = oi.MenuItemName,
                            Quantity = oi.Quantity,
                            UnitPrice = oi.UnitPrice
                        }).ToList()

                    }).ToList()

                }).ToList()


            };

            return new BaseResponse<TableDto>
            {
                Message = "Table fetched successfully",
                Status = true,
                Data = tablesDto
            };
        }

        public async Task<BaseResponse<TableDto>> GetTableByTableIdAsync(string TableId)
        {
            // get existing table by Id
            var table = await _tableRepository.GetTableByTableIdAsync(TableId);

            if (table is null)
            {
                _logger.LogError("Table couldn't be fetched");
                return new BaseResponse<TableDto>
                {
                    Message = $"Table couldn't be fetched",
                    Status = false,

                };
            }

            // map tables
            var tablesDto = new TableDto
            {
                Id = table.Id,
                TableNumber = table.TableNumber,
                TableId = table.TableId,
                QrCode = table.QrCode,
                Status = table.Status,
                IsActive = table.IsActive,
                BranchName = table.BranchName,
                CreatedBy = table.CreatedBy,
                CreatedOn = table.CreatedOn,
                ModifiedOn = table.LastModifiedOn,
                CompanyName = table.CompanyName,
                Guests = table.Guests.Select(tn => new GuestDto
                {
                    Id = tn.Id,
                    NumberOfGuest = tn.NumberOfGuest,
                    TableId = tn.TableId,

                }).ToList(),
                Tabs = table.Tabs.Select(tb => new TabDto
                {
                    TabId = tb.Id,
                    TabName = tb.TabName,
                    TableId = tb.TableId,
                    Orders = tb.Orders.Select(o => new OrderDto
                    {
                        OrderNumber = o.OrderNumber,
                        TabId = o.TabId,
                        Channel = o.Channel,
                        CustomerName = o.CustomerName,
                        CustomerReferenceNumber = o.CustomerReferenceNumber,
                        WaiterName = o.WaiterName,
                        Status = o.Status,
                        Bill = o.Bill,
                        Tip = o.Tip,
                        OrderItems = o.OrderItems.Select(oi => new OrderItemDto
                        {
                            Id = oi.Id,
                            OrderId = oi.OrderId,
                            MenuItemId = oi.MenuItemId,
                            MenuItemName = oi.MenuItemName,
                            Quantity = oi.Quantity,
                            UnitPrice = oi.UnitPrice
                        }).ToList()

                    }).ToList()

                }).ToList()


            };

            return new BaseResponse<TableDto>
            {
                Message = "Table fetched successfully",
                Status = true,
                Data = tablesDto
            };
        }

        public async Task<BaseResponse<IList<TableDataResponseDto>>> GetTablesDataByCompanyNameAsync(string businessName)
        {
            var tables = await _tableRepository.GetTablesDataByCompanyNameAsync(businessName);

            if (tables is null || !tables.Any())
            {
                _logger.LogError("Tables are empty, couldn't be fetched");
                return new BaseResponse<IList<TableDataResponseDto>>
                {
                    Message = $"Tables are empty, or couldn't be fetched",
                    Status = false,

                };
            }

            var tablesDto = tables.Select(tb => new TableDataResponseDto
            {
                Id = tb.Id,
                TableNumber = tb.TableNumber,
                TableId = tb.TableId,
                IsActive = tb.IsActive,
                BranchName = tb.BranchName,
                QrCode = tb.QrCode,

            }).ToList();

            return new BaseResponse<IList<TableDataResponseDto>>
            {
                Message = "Table fetched successfully",
                Status = true,
                Data = tablesDto
            };
        }

        public async Task<BaseResponse<IList<GroupedTablesResponseDto>>> GetGroupedTablesFrorBranchByCompanyNameAsync(string businessName)
        {
            var existingTables = await _tableRepository.GetTablesDataByCompanyNameAsync(businessName);

            if (existingTables is null || !existingTables.Any())
            {
                _logger.LogError("Tables are empty, couldn't be fetched");
                return new BaseResponse<IList<GroupedTablesResponseDto>>
                {
                    Message = $"Tables are empty, or couldn't be fetched",
                    Status = false,

                };
            }

            // group tables by branch name
            var groupedTables = existingTables.GroupBy(t => t.BranchName);

            // instantiate a list to store grouped tables
            var responseList = new List<GroupedTablesResponseDto>();

            foreach(var group in groupedTables)
            {
                responseList.Add(new GroupedTablesResponseDto
                {
                    BranchName = group.Key,
                    TableNumbers = group.Select(t => new TableInfo
                    {
                        Id = t.Id,
                        TableNumber = t.TableNumber
                    }).ToList()

                });
            }

            return new BaseResponse<IList<GroupedTablesResponseDto>>
            {
                Message = $"{existingTables.Count} tables(s) fetched.",
                Status = true,
                Data = responseList
            };
        }

       
        private void CacheData(string cacheKey, List<Table> data)
        {
            // Store data in the in-memory cache
            _memoryCache.Set(cacheKey, data, TimeSpan.FromMinutes(10)); // Cache for 10 minutes
        }

        private List<Table> RetrieveCachedData(string cacheKey)
        {
            // Retrieve data from the in-memory cache
            return _memoryCache.Get<List<Table>>(cacheKey);
        }

        private string GenerateAlphanumericTableId(int tableNumber)
        {
            char alphabet = (char)('A' + (tableNumber - 1) % 26);
            string numericPart = tableNumber.ToString("D3");

            return $"{alphabet}{numericPart}";
        }

        private async Task<string> GenerateQrCode(Table table, string companyName)
        {
            try
            {
                var menus = await _menuRepository.GetAllMenusByCompanyNameAsync(companyName);
                var tableData = new
                {
                    TableId = table.TableId,
                    TableNumber = table.TableNumber,
                    Menus = menus
                };

                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };

                var jsonData = JsonConvert.SerializeObject(tableData, settings);

                var qrCodeImageBytes = QRCodeImageGenerator(jsonData);

                // Convert the byte array to Base64 for simplicity
                return Convert.ToBase64String(qrCodeImageBytes);

                // Generate a unique file name
                //var fileName = $"{DateTime.Now:yyyyMMddHHmmssfff}_qrcode.png";
                //var filePath = Path.Combine("C:\\Users\\dell\\Desktop\\New folder\\troo-x-backend\\Persistence\\QrImages\\", fileName);

                //File.WriteAllBytes(filePath, qrCodeImage);

                // return filePath;
            }
            catch (Exception ex)
            {
                // Log the exception for further investigation
                _logger.LogError(ex, $"Error generating QR code: {ex.Message}");
                throw; // Rethrow the exception for the caller to handle
            }
        }

        private byte[] QRCodeImageGenerator(string qrCode)
        {
            var writer = new QRCodeWriter();
            var encOptions = new QrCodeEncodingOptions
            {
                Width = 200,
                Height = 200,
                Margin = 0,
                PureBarcode = false,
                ErrorCorrection = ErrorCorrectionLevel.H,
                CharacterSet = "UTF-8", 
            };

            encOptions.Hints.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H);
            var matrix = writer.encode(qrCode, BarcodeFormat.QR_CODE, 200, 200, encOptions.Hints);
            int scale = 2;
            Bitmap result = new Bitmap(matrix.Width * scale, matrix.Height * scale);

            // Rest of your logic for creating the QR code image

            var ms = new MemoryStream();
            result.Save(ms, ImageFormat.Png);
            byte[] byteImage = ms.ToArray();
            return byteImage;
        }

        
    }
}
