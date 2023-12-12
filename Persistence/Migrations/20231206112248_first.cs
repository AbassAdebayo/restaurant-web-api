using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KYCs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SuperAdminId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VatNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebPage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessAddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessAddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessFiscalYearFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BusinessFiscalYearTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisteredHomeAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisteredCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountHolderOrBusinessName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BVN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankCountry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KYCs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MenuName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MenuCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TagName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Channel = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModifierGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifierGroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifierGroupPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RuleDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModifierGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriceListEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceListEntries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PinCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    EmployeeType = table.Column<int>(type: "int", nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PincodeVerified = table.Column<bool>(type: "bit", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserType = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TableNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TableId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QrCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TableCapacity = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MenuId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tables_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ModifierItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifierGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifierItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModifierItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModifierItems_ModifierGroups_ModifierGroupId",
                        column: x => x.ModifierGroupId,
                        principalTable: "ModifierGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubPermissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubPermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MenuGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MenuGroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MenuGroupPricingOption = table.Column<bool>(type: "bit", nullable: false),
                    MenuGroupPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MenuGroupCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MenuId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MenuGroupImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Channel = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PriceListEntryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuGroups_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuGroups_PriceListEntries_PriceListEntryId",
                        column: x => x.PriceListEntryId,
                        principalTable: "PriceListEntries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PriceOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OptionPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceListEntryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceOptions_PriceListEntries_PriceListEntryId",
                        column: x => x.PriceListEntryId,
                        principalTable: "PriceListEntries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Guests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumberOfGuest = table.Column<int>(type: "int", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Guests_Tables_TableId",
                        column: x => x.TableId,
                        principalTable: "Tables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tabs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TabName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tabs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tabs_Tables_TableId",
                        column: x => x.TableId,
                        principalTable: "Tables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MenuItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MenuItemPricingOption = table.Column<int>(type: "int", nullable: false),
                    MenuItemCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MenuGroupCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MenuItemImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrentPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MiniPriceRange = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MaxPriceRange = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Channel = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    MenuGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PriceListEntryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuItems_MenuGroups_MenuGroupId",
                        column: x => x.MenuGroupId,
                        principalTable: "MenuGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuItems_PriceListEntries_PriceListEntryId",
                        column: x => x.PriceListEntryId,
                        principalTable: "PriceListEntries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TabId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WaiterName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Bill = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Channel = table.Column<int>(type: "int", nullable: true),
                    Tip = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Tabs_TabId",
                        column: x => x.TabId,
                        principalTable: "Tabs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RangePriceOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MenuItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RangePriceOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RangePriceOptions_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeSpecificPriceOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MenuItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountedPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSpecificPriceOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeSpecificPriceOptions_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MenuItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MenuItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0b513db6-6ed1-44c5-9615-32f2d74da1e5"), "Cash Register" },
                    { new Guid("1c349f74-1e47-4b6e-8b8f-e38fd7a029f7"), "Menu Settings" },
                    { new Guid("a03871eb-adf8-4dc3-9497-ac7d2d162d4a"), "Kitchen Display System" },
                    { new Guid("d2bf592c-5eb5-4d41-94c7-1134c70eef9e"), "Table Ordering" },
                    { new Guid("e1be84dc-e197-463e-a8f0-5915423eb13a"), "Till" },
                    { new Guid("fe5da079-9611-47b9-b06d-29222f6b6420"), "Tickets" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedBy", "Description", "RoleName" },
                values: new object[] { new Guid("708d384d-3f1e-493f-b083-6fa3a6eb3de9"), "Auto", "Owner", "SuperAdmin" });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("0895856f-54d8-4578-82c4-c76a13468fca"), new Guid("e1be84dc-e197-463e-a8f0-5915423eb13a"), new Guid("708d384d-3f1e-493f-b083-6fa3a6eb3de9") },
                    { new Guid("1e0d4aff-e109-4c6f-a36f-28c7fc6d50bc"), new Guid("fe5da079-9611-47b9-b06d-29222f6b6420"), new Guid("708d384d-3f1e-493f-b083-6fa3a6eb3de9") },
                    { new Guid("2359bbc4-b941-4841-8137-d2ed6fe4d8d3"), new Guid("1c349f74-1e47-4b6e-8b8f-e38fd7a029f7"), new Guid("708d384d-3f1e-493f-b083-6fa3a6eb3de9") },
                    { new Guid("66d20f75-cc4a-46c8-9243-becbd13d5ff7"), new Guid("0b513db6-6ed1-44c5-9615-32f2d74da1e5"), new Guid("708d384d-3f1e-493f-b083-6fa3a6eb3de9") },
                    { new Guid("d64f37a2-5638-49d8-98bd-0522cf2b331c"), new Guid("a03871eb-adf8-4dc3-9497-ac7d2d162d4a"), new Guid("708d384d-3f1e-493f-b083-6fa3a6eb3de9") },
                    { new Guid("de467fc3-7ede-442c-b066-54a7fb11270f"), new Guid("d2bf592c-5eb5-4d41-94c7-1134c70eef9e"), new Guid("708d384d-3f1e-493f-b083-6fa3a6eb3de9") }
                });

            migrationBuilder.InsertData(
                table: "SubPermissions",
                columns: new[] { "Id", "Name", "PermissionId" },
                values: new object[,]
                {
                    { new Guid("081320c9-5dd3-4055-b1f2-8efa99cce621"), "Ticket", new Guid("e1be84dc-e197-463e-a8f0-5915423eb13a") },
                    { new Guid("1238c73d-4220-4b02-b8dd-8521aea26d48"), "Refunds", new Guid("e1be84dc-e197-463e-a8f0-5915423eb13a") },
                    { new Guid("1a008bc7-e790-4e5c-bac9-35c61e877362"), "View Order", new Guid("a03871eb-adf8-4dc3-9497-ac7d2d162d4a") },
                    { new Guid("32fb0f3e-54e0-4b33-bf83-15933164a47c"), "Edit Order Status", new Guid("a03871eb-adf8-4dc3-9497-ac7d2d162d4a") },
                    { new Guid("38bb4e24-3a68-4842-8b84-da3552d6d780"), "Create Category", new Guid("1c349f74-1e47-4b6e-8b8f-e38fd7a029f7") },
                    { new Guid("3faeec15-af12-4b54-b3f3-ada58bdee3a7"), "View All Tickets", new Guid("fe5da079-9611-47b9-b06d-29222f6b6420") },
                    { new Guid("4514293e-b2f6-449f-997f-cc1cbd608d26"), "Discount", new Guid("e1be84dc-e197-463e-a8f0-5915423eb13a") },
                    { new Guid("45feadf7-1a22-46dc-a726-8fde87fbc8c1"), "Mirror Cash Register Privileges", new Guid("d2bf592c-5eb5-4d41-94c7-1134c70eef9e") },
                    { new Guid("468b229e-a308-450e-9516-d8c2e8fac1de"), "Hardware Integration", new Guid("0b513db6-6ed1-44c5-9615-32f2d74da1e5") },
                    { new Guid("47745725-12fb-4c42-968f-e20bb35849c8"), "View Order Status", new Guid("a03871eb-adf8-4dc3-9497-ac7d2d162d4a") },
                    { new Guid("4a886af6-06f7-4cba-b5e2-b48697b4ff24"), "Access Handheld Devices With Pin", new Guid("d2bf592c-5eb5-4d41-94c7-1134c70eef9e") },
                    { new Guid("53e81f34-1630-43bb-a976-b5b94703eeb8"), "Add Item", new Guid("1c349f74-1e47-4b6e-8b8f-e38fd7a029f7") },
                    { new Guid("629b1ae5-a3a8-41c6-ba8a-531ca9af9504"), "POS Integration", new Guid("0b513db6-6ed1-44c5-9615-32f2d74da1e5") },
                    { new Guid("6c2fc122-3f6e-4934-b1bb-cd19230315e1"), "Cancel Or Void Order", new Guid("e1be84dc-e197-463e-a8f0-5915423eb13a") },
                    { new Guid("788d02fc-b318-41db-9a48-9e09e3936226"), "EOD Balance Of Account", new Guid("e1be84dc-e197-463e-a8f0-5915423eb13a") },
                    { new Guid("7ad67d89-2377-44e4-898f-54599cc6edfa"), "Create Menu", new Guid("1c349f74-1e47-4b6e-8b8f-e38fd7a029f7") },
                    { new Guid("7f664624-c85e-4307-b8e1-33d710ea7951"), "Tips", new Guid("e1be84dc-e197-463e-a8f0-5915423eb13a") },
                    { new Guid("8e65b992-45c8-4aa7-acf9-78b89947f6f5"), "Refund Ticket", new Guid("fe5da079-9611-47b9-b06d-29222f6b6420") },
                    { new Guid("8fb175bf-6717-435c-89e6-824ab33bab7a"), "Order Chat", new Guid("a03871eb-adf8-4dc3-9497-ac7d2d162d4a") },
                    { new Guid("b96c2e9c-d2d9-4f33-9827-907e1f0fa8a1"), "View Ticket Status", new Guid("fe5da079-9611-47b9-b06d-29222f6b6420") },
                    { new Guid("cb858b45-8cdb-4bc8-b183-6f1c72dc447a"), "Inventory Management", new Guid("0b513db6-6ed1-44c5-9615-32f2d74da1e5") },
                    { new Guid("cc35524e-f68f-450c-aebb-32acbfcf85a8"), "Sync To Cloud", new Guid("e1be84dc-e197-463e-a8f0-5915423eb13a") },
                    { new Guid("d6cc6b7b-af56-48f6-90a3-2fd6b61b3335"), "Order Chat", new Guid("e1be84dc-e197-463e-a8f0-5915423eb13a") },
                    { new Guid("e172f63e-a92f-423e-831a-14b313cfbb24"), "Order Management", new Guid("e1be84dc-e197-463e-a8f0-5915423eb13a") },
                    { new Guid("f2eadb02-e660-404b-b595-51fe69466644"), "Fulfill Order", new Guid("a03871eb-adf8-4dc3-9497-ac7d2d162d4a") },
                    { new Guid("ff500aba-b4d4-453a-ab68-4785d84a7223"), "Void Ticket Transactions", new Guid("fe5da079-9611-47b9-b06d-29222f6b6420") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Guests_TableId",
                table: "Guests",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuGroups_MenuId",
                table: "MenuGroups",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuGroups_PriceListEntryId",
                table: "MenuGroups",
                column: "PriceListEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_MenuGroupId",
                table: "MenuItems",
                column: "MenuGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_PriceListEntryId",
                table: "MenuItems",
                column: "PriceListEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifierItems_ModifierGroupId",
                table: "ModifierItems",
                column: "ModifierGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_MenuItemId",
                table: "OrderItems",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TabId",
                table: "Orders",
                column: "TabId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceOptions_PriceListEntryId",
                table: "PriceOptions",
                column: "PriceListEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_RangePriceOptions_MenuItemId",
                table: "RangePriceOptions",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId",
                table: "RolePermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SubPermissions_PermissionId",
                table: "SubPermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_MenuId",
                table: "Tables",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Tabs_TableId",
                table: "Tabs",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSpecificPriceOptions_MenuItemId",
                table: "TimeSpecificPriceOptions",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Guests");

            migrationBuilder.DropTable(
                name: "KYCs");

            migrationBuilder.DropTable(
                name: "ModifierItems");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "PriceOptions");

            migrationBuilder.DropTable(
                name: "RangePriceOptions");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "SubPermissions");

            migrationBuilder.DropTable(
                name: "TimeSpecificPriceOptions");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "ModifierGroups");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Tabs");

            migrationBuilder.DropTable(
                name: "MenuGroups");

            migrationBuilder.DropTable(
                name: "Tables");

            migrationBuilder.DropTable(
                name: "PriceListEntries");

            migrationBuilder.DropTable(
                name: "Menus");
        }
    }
}
