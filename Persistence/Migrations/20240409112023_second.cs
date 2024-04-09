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
                keyValue: new Guid("6ed2211c-0de6-4467-9a65-12b6c22ec035"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("97cd6f36-4903-4b97-89e6-63cb59bd3e6f"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("9ea0f660-043c-4fad-b00b-5f01d948810b"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("9f5c5309-f2ca-4943-8412-f44a31f53241"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("b8e7bb4e-02d7-43cc-8bfc-852731fe0f29"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("de11697e-31e2-4d59-aa05-9a13dab98577"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("156cb5f6-fdb0-4b29-a9f8-fe52aaf2b97a"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("1e2f2276-bdb3-49f3-91c6-2edd27ec4ace"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("20c31399-1427-4e0b-9900-6ab827185025"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("41ce2cf1-54fd-45b3-a2c0-d54a44ce3a64"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("4fece294-363f-46a8-99bb-44320baab8c1"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("58978779-860f-4644-a74c-f5a78d835c26"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("61e39a2a-0db1-43bb-adac-617e73504416"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("645291df-8563-4945-891f-4591bc572d59"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("6613bc9b-c152-4bac-9016-8b2870ec66b1"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("7f96d396-dc0b-446c-9030-83730c4cac14"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("899fdc2b-4948-44f5-b0e1-8940870b4c3f"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("98364e85-765d-45e9-9cfa-df3c273dae8a"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("9fffe096-8900-40b2-a6ca-3e5136d6947a"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("a1c9ed38-a9b6-4717-86c2-2019d8584d42"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("a451cfd0-787c-4817-80f1-28bf9d27e69f"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("a5524284-075e-4412-b146-d1a2fb9cb20a"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("c704ec3e-bc49-48e8-b94a-acf760398a9c"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("c88b91c3-80ba-4e41-ad87-fcfc34d4255f"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("cbbf6797-2911-4f99-b9e6-f785317f0ea4"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("cce7598a-19a0-4743-973d-4c1df69adde7"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("cdc36085-e200-4c2b-b807-27c569dbb1eb"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("d1192028-5033-40ec-b871-c96cef9252ed"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("d59a28d9-dbf8-42ae-a6b1-b7b0419edfcd"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("dc9b433e-c7b1-4023-bf73-b87c4d53a7b9"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("dffbf553-4722-48f0-9179-e6589bd0bd89"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("e4db0471-01a7-4a6f-82a4-a510ec589d74"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("683fac62-acd2-4652-8dcc-5a7090967e72"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("6f63571f-d12d-49ab-b312-cf418d9bcc31"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("a142b218-b5aa-43e3-a34b-4e70a85334a5"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("dd5b567a-2c29-4fb2-b77e-bf7bb792b5c7"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("f11e1aec-efe2-401d-9715-6bba98b1313b"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("f9f499aa-d4d4-49cd-b3e4-a60a778b93d1"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("cb116949-ba67-410e-b532-c4e49294b949"));

            migrationBuilder.AddColumn<string>(
                name: "BusinessName",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("10ed40b6-6d51-4fc0-bab4-80e1168344da"), "Till" },
                    { new Guid("52408be2-2c00-4902-962e-25f989eee7ac"), "Kitchen Display System" },
                    { new Guid("53b937b9-39f0-40be-b118-d211c56b6e4e"), "Menu Settings" },
                    { new Guid("86282af6-436b-4b95-ab13-16a0f21615b0"), "Cash Register" },
                    { new Guid("a4ab07ea-d693-4cc7-bc9f-b470a1a753c7"), "Table Ordering" },
                    { new Guid("acc372c0-6dbe-4ac9-8900-f7d19276ae9d"), "Tickets" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedBy", "Description", "RoleName" },
                values: new object[] { new Guid("3cc585b7-0380-4d76-98c4-237d26308743"), "Auto", "Owner", "SuperAdmin" });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "PermissionId", "RoleId", "SubPermissionId" },
                values: new object[,]
                {
                    { new Guid("208a6495-5e15-42f8-8149-831fd8ffa272"), new Guid("a4ab07ea-d693-4cc7-bc9f-b470a1a753c7"), new Guid("3cc585b7-0380-4d76-98c4-237d26308743"), null },
                    { new Guid("959c6871-a075-4ca0-a79d-c27f4626abea"), new Guid("53b937b9-39f0-40be-b118-d211c56b6e4e"), new Guid("3cc585b7-0380-4d76-98c4-237d26308743"), null },
                    { new Guid("9f4a154c-a178-4bf9-a9ba-d82a19599efc"), new Guid("52408be2-2c00-4902-962e-25f989eee7ac"), new Guid("3cc585b7-0380-4d76-98c4-237d26308743"), null },
                    { new Guid("b6a8c41c-c468-4d21-ab54-9d3475ef5af9"), new Guid("10ed40b6-6d51-4fc0-bab4-80e1168344da"), new Guid("3cc585b7-0380-4d76-98c4-237d26308743"), null },
                    { new Guid("cf58adbb-eb18-4d80-8040-295ec10f39ad"), new Guid("acc372c0-6dbe-4ac9-8900-f7d19276ae9d"), new Guid("3cc585b7-0380-4d76-98c4-237d26308743"), null },
                    { new Guid("ef6fd583-adc5-4b43-a9a0-a779ec4b9d8c"), new Guid("86282af6-436b-4b95-ab13-16a0f21615b0"), new Guid("3cc585b7-0380-4d76-98c4-237d26308743"), null }
                });

            migrationBuilder.InsertData(
                table: "SubPermissions",
                columns: new[] { "Id", "Name", "PermissionId" },
                values: new object[,]
                {
                    { new Guid("06b8bef8-7a57-42e6-854d-759c44876ce9"), "EOD Balance Of Account", new Guid("10ed40b6-6d51-4fc0-bab4-80e1168344da") },
                    { new Guid("06dd3190-a2c0-4bec-a924-d3f18bbec87a"), "Refunds", new Guid("10ed40b6-6d51-4fc0-bab4-80e1168344da") },
                    { new Guid("0b7e5497-596d-4fc7-876a-33c51e67b9ac"), "Tips", new Guid("10ed40b6-6d51-4fc0-bab4-80e1168344da") },
                    { new Guid("113cda9f-7895-4e19-9765-26787e63a546"), "POS Integration", new Guid("86282af6-436b-4b95-ab13-16a0f21615b0") },
                    { new Guid("1cb886a2-07f1-413b-baaa-57ce7e61847a"), "Create Category", new Guid("53b937b9-39f0-40be-b118-d211c56b6e4e") },
                    { new Guid("227c5310-3de4-4b02-b8a4-9cf9383e089e"), "View Order", new Guid("52408be2-2c00-4902-962e-25f989eee7ac") },
                    { new Guid("22e633a1-a35b-4e9b-8ec0-bd5ef95ca405"), "View Order Status", new Guid("52408be2-2c00-4902-962e-25f989eee7ac") },
                    { new Guid("42189a5c-90cd-4a57-8634-ef8fc6137b55"), "View Ticket Status", new Guid("acc372c0-6dbe-4ac9-8900-f7d19276ae9d") },
                    { new Guid("45f1dcdd-535d-4a84-b095-84aa82c7319b"), "Refund Ticket", new Guid("acc372c0-6dbe-4ac9-8900-f7d19276ae9d") },
                    { new Guid("5c921c02-0dc3-48bb-ae3c-3e6693f2af8b"), "Sync To Cloud", new Guid("10ed40b6-6d51-4fc0-bab4-80e1168344da") },
                    { new Guid("6923b19b-c5b9-4938-bc90-4405291242fe"), "Fulfill Order", new Guid("52408be2-2c00-4902-962e-25f989eee7ac") },
                    { new Guid("70098483-eab1-4762-b9ba-b377f4b65587"), "Void Ticket Transactions", new Guid("acc372c0-6dbe-4ac9-8900-f7d19276ae9d") },
                    { new Guid("7647468d-d476-4c8a-9838-57e2660442db"), "Edit Order Status", new Guid("52408be2-2c00-4902-962e-25f989eee7ac") },
                    { new Guid("85aced5f-a3ee-44ca-9686-89a4ed68172f"), "Add Item", new Guid("53b937b9-39f0-40be-b118-d211c56b6e4e") },
                    { new Guid("9783109c-eefd-4972-8c11-1126c47575be"), "Mirror Cash Register Privileges", new Guid("a4ab07ea-d693-4cc7-bc9f-b470a1a753c7") },
                    { new Guid("982cfa3d-4124-4929-af6b-854c0a7b165d"), "Cancel Or Void Order", new Guid("10ed40b6-6d51-4fc0-bab4-80e1168344da") },
                    { new Guid("9fc86b22-18ab-4d49-80cd-b42bb338b818"), "Inventory Management", new Guid("86282af6-436b-4b95-ab13-16a0f21615b0") },
                    { new Guid("a10f2a38-a328-49fa-a566-c4e19debb001"), "Create Menu", new Guid("53b937b9-39f0-40be-b118-d211c56b6e4e") },
                    { new Guid("a53d3791-cb61-44e0-ab4b-166200fbb35c"), "Access Handheld Devices With Pin", new Guid("a4ab07ea-d693-4cc7-bc9f-b470a1a753c7") },
                    { new Guid("b71beff5-7e5b-497b-ae65-1689a559aea6"), "View All Tickets", new Guid("acc372c0-6dbe-4ac9-8900-f7d19276ae9d") },
                    { new Guid("b999a3d5-667a-45ba-91fc-6fbde752c1c2"), "Ticket", new Guid("10ed40b6-6d51-4fc0-bab4-80e1168344da") },
                    { new Guid("de5f7658-de9b-4367-ab6b-91a2fd2d558a"), "Order Chat", new Guid("52408be2-2c00-4902-962e-25f989eee7ac") },
                    { new Guid("f4437b8a-13d2-4f47-aa9e-e0dd9e2d1d1e"), "Discount", new Guid("10ed40b6-6d51-4fc0-bab4-80e1168344da") },
                    { new Guid("f6c2de19-ded9-498c-995c-4f97bf61c251"), "Order Management", new Guid("10ed40b6-6d51-4fc0-bab4-80e1168344da") },
                    { new Guid("fe6f7a9f-52cf-4a94-bd12-bebea0510893"), "Order Chat", new Guid("10ed40b6-6d51-4fc0-bab4-80e1168344da") },
                    { new Guid("ffed8967-146e-4be2-863d-b9808464e33e"), "Hardware Integration", new Guid("86282af6-436b-4b95-ab13-16a0f21615b0") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("208a6495-5e15-42f8-8149-831fd8ffa272"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("959c6871-a075-4ca0-a79d-c27f4626abea"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("9f4a154c-a178-4bf9-a9ba-d82a19599efc"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("b6a8c41c-c468-4d21-ab54-9d3475ef5af9"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("cf58adbb-eb18-4d80-8040-295ec10f39ad"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("ef6fd583-adc5-4b43-a9a0-a779ec4b9d8c"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("06b8bef8-7a57-42e6-854d-759c44876ce9"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("06dd3190-a2c0-4bec-a924-d3f18bbec87a"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("0b7e5497-596d-4fc7-876a-33c51e67b9ac"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("113cda9f-7895-4e19-9765-26787e63a546"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("1cb886a2-07f1-413b-baaa-57ce7e61847a"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("227c5310-3de4-4b02-b8a4-9cf9383e089e"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("22e633a1-a35b-4e9b-8ec0-bd5ef95ca405"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("42189a5c-90cd-4a57-8634-ef8fc6137b55"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("45f1dcdd-535d-4a84-b095-84aa82c7319b"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("5c921c02-0dc3-48bb-ae3c-3e6693f2af8b"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("6923b19b-c5b9-4938-bc90-4405291242fe"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("70098483-eab1-4762-b9ba-b377f4b65587"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("7647468d-d476-4c8a-9838-57e2660442db"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("85aced5f-a3ee-44ca-9686-89a4ed68172f"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("9783109c-eefd-4972-8c11-1126c47575be"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("982cfa3d-4124-4929-af6b-854c0a7b165d"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("9fc86b22-18ab-4d49-80cd-b42bb338b818"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("a10f2a38-a328-49fa-a566-c4e19debb001"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("a53d3791-cb61-44e0-ab4b-166200fbb35c"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("b71beff5-7e5b-497b-ae65-1689a559aea6"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("b999a3d5-667a-45ba-91fc-6fbde752c1c2"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("de5f7658-de9b-4367-ab6b-91a2fd2d558a"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("f4437b8a-13d2-4f47-aa9e-e0dd9e2d1d1e"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("f6c2de19-ded9-498c-995c-4f97bf61c251"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("fe6f7a9f-52cf-4a94-bd12-bebea0510893"));

            migrationBuilder.DeleteData(
                table: "SubPermissions",
                keyColumn: "Id",
                keyValue: new Guid("ffed8967-146e-4be2-863d-b9808464e33e"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("10ed40b6-6d51-4fc0-bab4-80e1168344da"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("52408be2-2c00-4902-962e-25f989eee7ac"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("53b937b9-39f0-40be-b118-d211c56b6e4e"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("86282af6-436b-4b95-ab13-16a0f21615b0"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("a4ab07ea-d693-4cc7-bc9f-b470a1a753c7"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("acc372c0-6dbe-4ac9-8900-f7d19276ae9d"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("3cc585b7-0380-4d76-98c4-237d26308743"));

            migrationBuilder.DropColumn(
                name: "BusinessName",
                table: "Payments");

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("683fac62-acd2-4652-8dcc-5a7090967e72"), "Table Ordering" },
                    { new Guid("6f63571f-d12d-49ab-b312-cf418d9bcc31"), "Kitchen Display System" },
                    { new Guid("a142b218-b5aa-43e3-a34b-4e70a85334a5"), "Cash Register" },
                    { new Guid("dd5b567a-2c29-4fb2-b77e-bf7bb792b5c7"), "Tickets" },
                    { new Guid("f11e1aec-efe2-401d-9715-6bba98b1313b"), "Till" },
                    { new Guid("f9f499aa-d4d4-49cd-b3e4-a60a778b93d1"), "Menu Settings" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedBy", "Description", "RoleName" },
                values: new object[] { new Guid("cb116949-ba67-410e-b532-c4e49294b949"), "Auto", "Owner", "SuperAdmin" });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "PermissionId", "RoleId", "SubPermissionId" },
                values: new object[,]
                {
                    { new Guid("6ed2211c-0de6-4467-9a65-12b6c22ec035"), new Guid("f9f499aa-d4d4-49cd-b3e4-a60a778b93d1"), new Guid("cb116949-ba67-410e-b532-c4e49294b949"), null },
                    { new Guid("97cd6f36-4903-4b97-89e6-63cb59bd3e6f"), new Guid("6f63571f-d12d-49ab-b312-cf418d9bcc31"), new Guid("cb116949-ba67-410e-b532-c4e49294b949"), null },
                    { new Guid("9ea0f660-043c-4fad-b00b-5f01d948810b"), new Guid("683fac62-acd2-4652-8dcc-5a7090967e72"), new Guid("cb116949-ba67-410e-b532-c4e49294b949"), null },
                    { new Guid("9f5c5309-f2ca-4943-8412-f44a31f53241"), new Guid("dd5b567a-2c29-4fb2-b77e-bf7bb792b5c7"), new Guid("cb116949-ba67-410e-b532-c4e49294b949"), null },
                    { new Guid("b8e7bb4e-02d7-43cc-8bfc-852731fe0f29"), new Guid("f11e1aec-efe2-401d-9715-6bba98b1313b"), new Guid("cb116949-ba67-410e-b532-c4e49294b949"), null },
                    { new Guid("de11697e-31e2-4d59-aa05-9a13dab98577"), new Guid("a142b218-b5aa-43e3-a34b-4e70a85334a5"), new Guid("cb116949-ba67-410e-b532-c4e49294b949"), null }
                });

            migrationBuilder.InsertData(
                table: "SubPermissions",
                columns: new[] { "Id", "Name", "PermissionId" },
                values: new object[,]
                {
                    { new Guid("156cb5f6-fdb0-4b29-a9f8-fe52aaf2b97a"), "EOD Balance Of Account", new Guid("f11e1aec-efe2-401d-9715-6bba98b1313b") },
                    { new Guid("1e2f2276-bdb3-49f3-91c6-2edd27ec4ace"), "Add Item", new Guid("f9f499aa-d4d4-49cd-b3e4-a60a778b93d1") },
                    { new Guid("20c31399-1427-4e0b-9900-6ab827185025"), "Inventory Management", new Guid("a142b218-b5aa-43e3-a34b-4e70a85334a5") },
                    { new Guid("41ce2cf1-54fd-45b3-a2c0-d54a44ce3a64"), "Tips", new Guid("f11e1aec-efe2-401d-9715-6bba98b1313b") },
                    { new Guid("4fece294-363f-46a8-99bb-44320baab8c1"), "Edit Order Status", new Guid("6f63571f-d12d-49ab-b312-cf418d9bcc31") },
                    { new Guid("58978779-860f-4644-a74c-f5a78d835c26"), "Void Ticket Transactions", new Guid("dd5b567a-2c29-4fb2-b77e-bf7bb792b5c7") },
                    { new Guid("61e39a2a-0db1-43bb-adac-617e73504416"), "Ticket", new Guid("f11e1aec-efe2-401d-9715-6bba98b1313b") },
                    { new Guid("645291df-8563-4945-891f-4591bc572d59"), "Refunds", new Guid("f11e1aec-efe2-401d-9715-6bba98b1313b") },
                    { new Guid("6613bc9b-c152-4bac-9016-8b2870ec66b1"), "Create Menu", new Guid("f9f499aa-d4d4-49cd-b3e4-a60a778b93d1") },
                    { new Guid("7f96d396-dc0b-446c-9030-83730c4cac14"), "View Ticket Status", new Guid("dd5b567a-2c29-4fb2-b77e-bf7bb792b5c7") },
                    { new Guid("899fdc2b-4948-44f5-b0e1-8940870b4c3f"), "View All Tickets", new Guid("dd5b567a-2c29-4fb2-b77e-bf7bb792b5c7") },
                    { new Guid("98364e85-765d-45e9-9cfa-df3c273dae8a"), "Access Handheld Devices With Pin", new Guid("683fac62-acd2-4652-8dcc-5a7090967e72") },
                    { new Guid("9fffe096-8900-40b2-a6ca-3e5136d6947a"), "View Order", new Guid("6f63571f-d12d-49ab-b312-cf418d9bcc31") },
                    { new Guid("a1c9ed38-a9b6-4717-86c2-2019d8584d42"), "Order Chat", new Guid("6f63571f-d12d-49ab-b312-cf418d9bcc31") },
                    { new Guid("a451cfd0-787c-4817-80f1-28bf9d27e69f"), "View Order Status", new Guid("6f63571f-d12d-49ab-b312-cf418d9bcc31") },
                    { new Guid("a5524284-075e-4412-b146-d1a2fb9cb20a"), "Create Category", new Guid("f9f499aa-d4d4-49cd-b3e4-a60a778b93d1") },
                    { new Guid("c704ec3e-bc49-48e8-b94a-acf760398a9c"), "Mirror Cash Register Privileges", new Guid("683fac62-acd2-4652-8dcc-5a7090967e72") },
                    { new Guid("c88b91c3-80ba-4e41-ad87-fcfc34d4255f"), "Cancel Or Void Order", new Guid("f11e1aec-efe2-401d-9715-6bba98b1313b") },
                    { new Guid("cbbf6797-2911-4f99-b9e6-f785317f0ea4"), "Sync To Cloud", new Guid("f11e1aec-efe2-401d-9715-6bba98b1313b") },
                    { new Guid("cce7598a-19a0-4743-973d-4c1df69adde7"), "Order Chat", new Guid("f11e1aec-efe2-401d-9715-6bba98b1313b") },
                    { new Guid("cdc36085-e200-4c2b-b807-27c569dbb1eb"), "Discount", new Guid("f11e1aec-efe2-401d-9715-6bba98b1313b") },
                    { new Guid("d1192028-5033-40ec-b871-c96cef9252ed"), "POS Integration", new Guid("a142b218-b5aa-43e3-a34b-4e70a85334a5") },
                    { new Guid("d59a28d9-dbf8-42ae-a6b1-b7b0419edfcd"), "Fulfill Order", new Guid("6f63571f-d12d-49ab-b312-cf418d9bcc31") },
                    { new Guid("dc9b433e-c7b1-4023-bf73-b87c4d53a7b9"), "Order Management", new Guid("f11e1aec-efe2-401d-9715-6bba98b1313b") },
                    { new Guid("dffbf553-4722-48f0-9179-e6589bd0bd89"), "Hardware Integration", new Guid("a142b218-b5aa-43e3-a34b-4e70a85334a5") },
                    { new Guid("e4db0471-01a7-4a6f-82a4-a510ec589d74"), "Refund Ticket", new Guid("dd5b567a-2c29-4fb2-b77e-bf7bb792b5c7") }
                });
        }
    }
}
