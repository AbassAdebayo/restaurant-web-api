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
                    { new Guid("65ec4455-05bb-4012-a89f-91203f6c8bd8"), "Tickets" },
                    { new Guid("88bbf4a1-3a48-496d-ad41-43ab930c1c3e"), "Menu Settings" },
                    { new Guid("901f8ab3-a552-415b-8844-c5f31e4285fe"), "Cash Register" },
                    { new Guid("abb4a95b-7b38-4920-b6cf-584975cb26c8"), "Table Ordering" },
                    { new Guid("d67b9497-dce0-48e5-8770-30b8e7517ac2"), "Kitchen Display System" },
                    { new Guid("dc75d111-d3a0-433a-b815-860eead0b275"), "Till" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedBy", "Description", "RoleName" },
                values: new object[] { new Guid("4d0486f2-c7d0-4aff-941d-a9b34e6a1b70"), "Auto", "Owner", "SuperAdmin" });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("0eb45329-e6cc-4c0e-a72f-ca688e616f84"), new Guid("901f8ab3-a552-415b-8844-c5f31e4285fe"), new Guid("4d0486f2-c7d0-4aff-941d-a9b34e6a1b70") },
                    { new Guid("19f47311-e732-44a3-bad2-201ff0d18ee7"), new Guid("dc75d111-d3a0-433a-b815-860eead0b275"), new Guid("4d0486f2-c7d0-4aff-941d-a9b34e6a1b70") },
                    { new Guid("2d6f2536-f730-4b02-8844-50f2d1fa003e"), new Guid("abb4a95b-7b38-4920-b6cf-584975cb26c8"), new Guid("4d0486f2-c7d0-4aff-941d-a9b34e6a1b70") },
                    { new Guid("34402c88-1b7b-4386-b965-cd3f154d5d37"), new Guid("88bbf4a1-3a48-496d-ad41-43ab930c1c3e"), new Guid("4d0486f2-c7d0-4aff-941d-a9b34e6a1b70") },
                    { new Guid("66e4bad1-b700-4708-b13c-907a7c1d0ed7"), new Guid("d67b9497-dce0-48e5-8770-30b8e7517ac2"), new Guid("4d0486f2-c7d0-4aff-941d-a9b34e6a1b70") },
                    { new Guid("9bedf81e-e63c-4da0-8d8d-eb7d560846d1"), new Guid("65ec4455-05bb-4012-a89f-91203f6c8bd8"), new Guid("4d0486f2-c7d0-4aff-941d-a9b34e6a1b70") }
                });

            migrationBuilder.InsertData(
                table: "SubPermissions",
                columns: new[] { "Id", "Name", "PermissionId" },
                values: new object[,]
                {
                    { new Guid("153c16fc-55cf-46e3-b124-0d7273ba9c9a"), "Add Item", new Guid("88bbf4a1-3a48-496d-ad41-43ab930c1c3e") },
                    { new Guid("33b0a1d1-2da6-468c-8772-02e23286221f"), "Edit Order Status", new Guid("d67b9497-dce0-48e5-8770-30b8e7517ac2") },
                    { new Guid("340a9fc4-d96a-4abe-9dc2-f6dc11551b3b"), "Tips", new Guid("dc75d111-d3a0-433a-b815-860eead0b275") },
                    { new Guid("3929d13d-03b7-4c7c-a547-b345e1dd92a9"), "Access Handheld Devices With Pin", new Guid("abb4a95b-7b38-4920-b6cf-584975cb26c8") },
                    { new Guid("4a300bc5-2fb7-4131-b019-e227d08849cd"), "Create Menu", new Guid("88bbf4a1-3a48-496d-ad41-43ab930c1c3e") },
                    { new Guid("4d133fe8-799c-4bc8-a0e9-c71cdf6fbc3c"), "Discount", new Guid("dc75d111-d3a0-433a-b815-860eead0b275") },
                    { new Guid("535f0cf1-6110-4f94-9ed8-bfda8084d056"), "Cancel Or Void Order", new Guid("dc75d111-d3a0-433a-b815-860eead0b275") },
                    { new Guid("55f1c2dc-bea6-4558-b9e6-db7e026dea23"), "Order Chat", new Guid("dc75d111-d3a0-433a-b815-860eead0b275") },
                    { new Guid("607fcff5-d2d7-4311-91f4-e062c53e9cf2"), "Sync To Cloud", new Guid("dc75d111-d3a0-433a-b815-860eead0b275") },
                    { new Guid("637721a8-8c36-4ec6-8575-58caa9bcf38c"), "Refunds", new Guid("dc75d111-d3a0-433a-b815-860eead0b275") },
                    { new Guid("63e41f19-f911-4d23-a95e-721442823fd1"), "View Order", new Guid("d67b9497-dce0-48e5-8770-30b8e7517ac2") },
                    { new Guid("64189302-5df1-415a-869c-8154df9d93cd"), "EOD Balance Of Account", new Guid("dc75d111-d3a0-433a-b815-860eead0b275") },
                    { new Guid("685d3591-e984-4fba-9b04-6f6975e74419"), "Order Management", new Guid("dc75d111-d3a0-433a-b815-860eead0b275") },
                    { new Guid("6d2d554a-cf6e-4eff-9fd4-beef65f6d921"), "View All Tickets", new Guid("65ec4455-05bb-4012-a89f-91203f6c8bd8") },
                    { new Guid("7024b2fd-e5f0-4a64-9e69-0294a431d0ff"), "Inventory Management", new Guid("901f8ab3-a552-415b-8844-c5f31e4285fe") },
                    { new Guid("709314ed-159d-4b77-9abf-75be001df594"), "Mirror Cash Register Privileges", new Guid("abb4a95b-7b38-4920-b6cf-584975cb26c8") },
                    { new Guid("8d94b0c7-7060-4618-a481-cbb045ecd64b"), "Create Category", new Guid("88bbf4a1-3a48-496d-ad41-43ab930c1c3e") },
                    { new Guid("9bda32bf-8c09-4c5d-ad53-fa45236c29a9"), "Order Chat", new Guid("d67b9497-dce0-48e5-8770-30b8e7517ac2") },
                    { new Guid("a2bd1b6a-edf2-45d0-959d-b2d17dd9f176"), "Ticket", new Guid("dc75d111-d3a0-433a-b815-860eead0b275") },
                    { new Guid("a9bb756a-d8f0-4f69-b319-9aaff6d8b9f1"), "Fulfill Order", new Guid("d67b9497-dce0-48e5-8770-30b8e7517ac2") },
                    { new Guid("adf254b6-16c2-432c-a4ee-31e296544f2d"), "Void Ticket Transactions", new Guid("65ec4455-05bb-4012-a89f-91203f6c8bd8") },
                    { new Guid("cc97540d-df9a-4b4c-a734-885b844a2f5b"), "View Order Status", new Guid("d67b9497-dce0-48e5-8770-30b8e7517ac2") },
                    { new Guid("dba87ec5-4019-4adf-85b2-20ad5061f29f"), "POS Integration", new Guid("901f8ab3-a552-415b-8844-c5f31e4285fe") },
                    { new Guid("ee7fa12a-0b0b-4c76-b6be-c3b6e63c0424"), "Hardware Integration", new Guid("901f8ab3-a552-415b-8844-c5f31e4285fe") },
                    { new Guid("ef43c373-9d49-4fe9-85e4-b5fde2a5a8b5"), "View Ticket Status", new Guid("65ec4455-05bb-4012-a89f-91203f6c8bd8") },
                    { new Guid("ff2ee0f0-dd95-4e06-b8f5-a68cc506fcf2"), "Refund Ticket", new Guid("65ec4455-05bb-4012-a89f-91203f6c8bd8") }
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
