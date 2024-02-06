using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("0eb45329-e6cc-4c0e-a72f-ca688e616f84"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("19f47311-e732-44a3-bad2-201ff0d18ee7"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("2d6f2536-f730-4b02-8844-50f2d1fa003e"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("34402c88-1b7b-4386-b965-cd3f154d5d37"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("66e4bad1-b700-4708-b13c-907a7c1d0ed7"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("9bedf81e-e63c-4da0-8d8d-eb7d560846d1"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("153c16fc-55cf-46e3-b124-0d7273ba9c9a"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("33b0a1d1-2da6-468c-8772-02e23286221f"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("340a9fc4-d96a-4abe-9dc2-f6dc11551b3b"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("3929d13d-03b7-4c7c-a547-b345e1dd92a9"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("4a300bc5-2fb7-4131-b019-e227d08849cd"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("4d133fe8-799c-4bc8-a0e9-c71cdf6fbc3c"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("535f0cf1-6110-4f94-9ed8-bfda8084d056"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("55f1c2dc-bea6-4558-b9e6-db7e026dea23"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("607fcff5-d2d7-4311-91f4-e062c53e9cf2"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("637721a8-8c36-4ec6-8575-58caa9bcf38c"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("63e41f19-f911-4d23-a95e-721442823fd1"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("64189302-5df1-415a-869c-8154df9d93cd"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("685d3591-e984-4fba-9b04-6f6975e74419"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("6d2d554a-cf6e-4eff-9fd4-beef65f6d921"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("7024b2fd-e5f0-4a64-9e69-0294a431d0ff"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("709314ed-159d-4b77-9abf-75be001df594"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("8d94b0c7-7060-4618-a481-cbb045ecd64b"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("9bda32bf-8c09-4c5d-ad53-fa45236c29a9"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("a2bd1b6a-edf2-45d0-959d-b2d17dd9f176"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("a9bb756a-d8f0-4f69-b319-9aaff6d8b9f1"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("adf254b6-16c2-432c-a4ee-31e296544f2d"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("cc97540d-df9a-4b4c-a734-885b844a2f5b"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("dba87ec5-4019-4adf-85b2-20ad5061f29f"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("ee7fa12a-0b0b-4c76-b6be-c3b6e63c0424"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("ef43c373-9d49-4fe9-85e4-b5fde2a5a8b5"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("ff2ee0f0-dd95-4e06-b8f5-a68cc506fcf2"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("65ec4455-05bb-4012-a89f-91203f6c8bd8"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("88bbf4a1-3a48-496d-ad41-43ab930c1c3e"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("901f8ab3-a552-415b-8844-c5f31e4285fe"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("abb4a95b-7b38-4920-b6cf-584975cb26c8"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d67b9497-dce0-48e5-8770-30b8e7517ac2"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("dc75d111-d3a0-433a-b815-860eead0b275"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4d0486f2-c7d0-4aff-941d-a9b34e6a1b70"));

            migrationBuilder.AddColumn<Guid>(
                name: "SubPermissionId",
                table: "RolePermissions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("088a7ed8-e301-42ef-a3b1-ad44c72b3e3e"), "Cash Register" },
                    { new Guid("1b415ce8-b355-412a-89eb-fa198e1a9345"), "Kitchen Display System" },
                    { new Guid("1f97b4c0-dc80-4c27-bb39-a7eb179c38a1"), "Till" },
                    { new Guid("3cde5c36-4722-48d2-a136-52cfa5447ef6"), "Table Ordering" },
                    { new Guid("4c07ccd7-da62-40be-af55-1df76c0e402a"), "Tickets" },
                    { new Guid("e8af0f98-cc43-46b2-99d3-5fe771867c2e"), "Menu Settings" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedBy", "Description", "RoleName" },
                values: new object[] { new Guid("73f9be25-1e41-4ed8-90c1-5c9a15bf0b21"), "Auto", "Owner", "SuperAdmin" });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "PermissionId", "RoleId", "SubPermissionId" },
                values: new object[,]
                {
                    { new Guid("0d635805-1298-435d-93eb-5bb0c8164a4d"), new Guid("1b415ce8-b355-412a-89eb-fa198e1a9345"), new Guid("73f9be25-1e41-4ed8-90c1-5c9a15bf0b21"), null },
                    { new Guid("3f8481d5-d1ff-4ade-a80b-29ce59a6023b"), new Guid("088a7ed8-e301-42ef-a3b1-ad44c72b3e3e"), new Guid("73f9be25-1e41-4ed8-90c1-5c9a15bf0b21"), null },
                    { new Guid("98e44d46-0ea8-493d-a2ff-606172c3b8be"), new Guid("3cde5c36-4722-48d2-a136-52cfa5447ef6"), new Guid("73f9be25-1e41-4ed8-90c1-5c9a15bf0b21"), null },
                    { new Guid("bbc62572-ed51-4087-89b4-409d84e4ca82"), new Guid("e8af0f98-cc43-46b2-99d3-5fe771867c2e"), new Guid("73f9be25-1e41-4ed8-90c1-5c9a15bf0b21"), null },
                    { new Guid("bc5bc554-96bc-4751-bd12-232af01c1f1f"), new Guid("4c07ccd7-da62-40be-af55-1df76c0e402a"), new Guid("73f9be25-1e41-4ed8-90c1-5c9a15bf0b21"), null },
                    { new Guid("eeedb051-a3ff-46f0-89d3-862b78ff90f3"), new Guid("1f97b4c0-dc80-4c27-bb39-a7eb179c38a1"), new Guid("73f9be25-1e41-4ed8-90c1-5c9a15bf0b21"), null }
                });

            migrationBuilder.InsertData(
                table: "SubPermissions",
                columns: new[] { "Id", "Name", "PermissionId" },
                values: new object[,]
                {
                    { new Guid("0f6ae32e-f61a-42c1-92db-5203599f7f1a"), "View Order Status", new Guid("1b415ce8-b355-412a-89eb-fa198e1a9345") },
                    { new Guid("11f411bf-b364-422f-8774-63255204974a"), "Hardware Integration", new Guid("088a7ed8-e301-42ef-a3b1-ad44c72b3e3e") },
                    { new Guid("16db0e75-de8e-4c06-a8f8-2e61ae459a9e"), "Ticket", new Guid("1f97b4c0-dc80-4c27-bb39-a7eb179c38a1") },
                    { new Guid("23ddd78b-34fa-4400-98a0-2390522c619e"), "View Order", new Guid("1b415ce8-b355-412a-89eb-fa198e1a9345") },
                    { new Guid("28a9e0e1-a6f5-4c9f-9edf-3c1914e4073a"), "EOD Balance Of Account", new Guid("1f97b4c0-dc80-4c27-bb39-a7eb179c38a1") },
                    { new Guid("36cd63a3-6125-46b6-8a2c-8e50ac435783"), "Discount", new Guid("1f97b4c0-dc80-4c27-bb39-a7eb179c38a1") },
                    { new Guid("4485f558-2427-467d-97ab-334acd7f316a"), "Create Menu", new Guid("e8af0f98-cc43-46b2-99d3-5fe771867c2e") },
                    { new Guid("499f0967-79c7-432b-badb-6e738bba91a3"), "Order Management", new Guid("1f97b4c0-dc80-4c27-bb39-a7eb179c38a1") },
                    { new Guid("49da926e-bb1d-4490-9055-9fe85a487362"), "POS Integration", new Guid("088a7ed8-e301-42ef-a3b1-ad44c72b3e3e") },
                    { new Guid("5453f0bc-48a2-47ad-b347-1f29bb75e291"), "Refund Ticket", new Guid("4c07ccd7-da62-40be-af55-1df76c0e402a") },
                    { new Guid("56362818-0726-4c80-b8fa-da1c2f869665"), "Mirror Cash Register Privileges", new Guid("3cde5c36-4722-48d2-a136-52cfa5447ef6") },
                    { new Guid("6ad57654-0dee-4a93-86df-43658016a03a"), "Create Category", new Guid("e8af0f98-cc43-46b2-99d3-5fe771867c2e") },
                    { new Guid("75334307-f5ae-4db1-8f3b-99b9ed26f217"), "Cancel Or Void Order", new Guid("1f97b4c0-dc80-4c27-bb39-a7eb179c38a1") },
                    { new Guid("8aaa80df-bc18-47c8-b644-c1e2671d07e5"), "Edit Order Status", new Guid("1b415ce8-b355-412a-89eb-fa198e1a9345") },
                    { new Guid("8f9f0e4e-ecf0-40f4-bd16-23c1f4445988"), "Void Ticket Transactions", new Guid("4c07ccd7-da62-40be-af55-1df76c0e402a") },
                    { new Guid("9b32c784-a4e5-413b-984e-524a9fa4dd6b"), "Sync To Cloud", new Guid("1f97b4c0-dc80-4c27-bb39-a7eb179c38a1") },
                    { new Guid("c12811c0-693c-417e-8e8f-8575f184a89f"), "Access Handheld Devices With Pin", new Guid("3cde5c36-4722-48d2-a136-52cfa5447ef6") },
                    { new Guid("c6edf066-9018-4eab-a1b9-f44a8f0cc0c4"), "Order Chat", new Guid("1b415ce8-b355-412a-89eb-fa198e1a9345") },
                    { new Guid("c7b1e46e-c1d0-4983-91e1-c0ff5fdb7356"), "View All Tickets", new Guid("4c07ccd7-da62-40be-af55-1df76c0e402a") },
                    { new Guid("d2d3a010-6c95-48e1-89db-50d5296ea235"), "Inventory Management", new Guid("088a7ed8-e301-42ef-a3b1-ad44c72b3e3e") },
                    { new Guid("d438832f-3095-4407-b49a-d68315177da7"), "Add Item", new Guid("e8af0f98-cc43-46b2-99d3-5fe771867c2e") },
                    { new Guid("e2e0e295-21f3-46e2-8a0e-1568cbaf8f6d"), "Tips", new Guid("1f97b4c0-dc80-4c27-bb39-a7eb179c38a1") },
                    { new Guid("eea378bb-765b-4373-8579-0a025421ea7d"), "Refunds", new Guid("1f97b4c0-dc80-4c27-bb39-a7eb179c38a1") },
                    { new Guid("eec9789c-c410-440b-813c-783775e4ce37"), "View Ticket Status", new Guid("4c07ccd7-da62-40be-af55-1df76c0e402a") },
                    { new Guid("f523e2c1-7d27-42b2-979f-9df6ee40fef9"), "Order Chat", new Guid("1f97b4c0-dc80-4c27-bb39-a7eb179c38a1") },
                    { new Guid("f8cc6d11-82a5-49b5-a9a6-65fd3d26b237"), "Fulfill Order", new Guid("1b415ce8-b355-412a-89eb-fa198e1a9345") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_SubPermissionId",
                table: "RolePermissions",
                column: "SubPermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_SubPermissions_SubPermissionId",
                table: "RolePermissions",
                column: "SubPermissionId",
                principalTable: "SubPermissions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_SubPermissions_SubPermissionId",
                table: "RolePermissions");

            migrationBuilder.DropIndex(
                name: "IX_RolePermissions_SubPermissionId",
                table: "RolePermissions");

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("0d635805-1298-435d-93eb-5bb0c8164a4d"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("3f8481d5-d1ff-4ade-a80b-29ce59a6023b"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("98e44d46-0ea8-493d-a2ff-606172c3b8be"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("bbc62572-ed51-4087-89b4-409d84e4ca82"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("bc5bc554-96bc-4751-bd12-232af01c1f1f"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("eeedb051-a3ff-46f0-89d3-862b78ff90f3"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("0f6ae32e-f61a-42c1-92db-5203599f7f1a"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("11f411bf-b364-422f-8774-63255204974a"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("16db0e75-de8e-4c06-a8f8-2e61ae459a9e"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("23ddd78b-34fa-4400-98a0-2390522c619e"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("28a9e0e1-a6f5-4c9f-9edf-3c1914e4073a"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("36cd63a3-6125-46b6-8a2c-8e50ac435783"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("4485f558-2427-467d-97ab-334acd7f316a"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("499f0967-79c7-432b-badb-6e738bba91a3"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("49da926e-bb1d-4490-9055-9fe85a487362"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("5453f0bc-48a2-47ad-b347-1f29bb75e291"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("56362818-0726-4c80-b8fa-da1c2f869665"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("6ad57654-0dee-4a93-86df-43658016a03a"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("75334307-f5ae-4db1-8f3b-99b9ed26f217"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("8aaa80df-bc18-47c8-b644-c1e2671d07e5"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("8f9f0e4e-ecf0-40f4-bd16-23c1f4445988"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("9b32c784-a4e5-413b-984e-524a9fa4dd6b"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("c12811c0-693c-417e-8e8f-8575f184a89f"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("c6edf066-9018-4eab-a1b9-f44a8f0cc0c4"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("c7b1e46e-c1d0-4983-91e1-c0ff5fdb7356"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("d2d3a010-6c95-48e1-89db-50d5296ea235"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("d438832f-3095-4407-b49a-d68315177da7"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("e2e0e295-21f3-46e2-8a0e-1568cbaf8f6d"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("eea378bb-765b-4373-8579-0a025421ea7d"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("eec9789c-c410-440b-813c-783775e4ce37"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("f523e2c1-7d27-42b2-979f-9df6ee40fef9"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("f8cc6d11-82a5-49b5-a9a6-65fd3d26b237"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("088a7ed8-e301-42ef-a3b1-ad44c72b3e3e"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("1b415ce8-b355-412a-89eb-fa198e1a9345"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("1f97b4c0-dc80-4c27-bb39-a7eb179c38a1"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("3cde5c36-4722-48d2-a136-52cfa5447ef6"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("4c07ccd7-da62-40be-af55-1df76c0e402a"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("e8af0f98-cc43-46b2-99d3-5fe771867c2e"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("73f9be25-1e41-4ed8-90c1-5c9a15bf0b21"));

            migrationBuilder.DropColumn(
                name: "SubPermissionId",
                table: "RolePermissions");

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
        }
    }
}
