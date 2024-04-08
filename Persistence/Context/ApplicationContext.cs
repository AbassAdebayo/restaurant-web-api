using Domain.Domain.Modules.MenuSettings;
using Domain.Domain.Modules.Order;
using Domain.Domain.Modules.PaymentGateway;
using Domain.Domain.Modules.RolePermission.Entities;
using Domain.Domain.Modules.Tables;
using Domain.Domain.Modules.Users.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using sib_api_v3_sdk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            

            var superAdminRoleId = Guid.NewGuid();
            builder.ApplyAllConfigurations<ApplicationContext>();
            builder.ConfigureDeletableEntities();
            builder.Entity<Role>().HasData(
                new Role
                {
                    Id = superAdminRoleId,
                    RoleName = "SuperAdmin",
                    Description = "Owner",
                    CreatedBy = "Auto"
                   
                }

                );

            SeedPermissionsAndSubPermissionsForSuperAdmin(builder, superAdminRoleId);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }

        private void SeedPermissionsAndSubPermissionsForSuperAdmin(ModelBuilder builder, Guid superAdminRoleId)
        {
            var menuSettingsPermissionId = Guid.NewGuid();
            var cashRegisterPermissionId = Guid.NewGuid();
            var tillPermissionId = Guid.NewGuid();
            var ticketsPermissionId = Guid.NewGuid();
            var tableOrderingPermissionId = Guid.NewGuid();
            var KDSPermissionId = Guid.NewGuid();

            builder.Entity<Permission>().HasData(
            new Permission
            {
                Id = menuSettingsPermissionId,
                Name = "Menu Settings"
            },
            new Permission
            {
                Id = cashRegisterPermissionId,
                Name = "Cash Register"
            },
            new Permission
            {
                Id = tillPermissionId,
                Name = "Till",
                // Assign Till as a sub-permission of CardRegister

            },
            new Permission
            {
                Id = ticketsPermissionId,
                Name = "Tickets"
            },
            new Permission
            {
                Id = tableOrderingPermissionId,
                Name = "Table Ordering"
            },
            new Permission
            {
                Id = KDSPermissionId,
                Name = "Kitchen Display System"
            }
        );

            // Seed subpermissions for MenuSettings
            builder.Entity<SubPermission>().HasData(
                new SubPermission
                {
                    Id = Guid.NewGuid(),
                    Name = "Create Category",
                    PermissionId = menuSettingsPermissionId
                },
                new SubPermission
                {
                    Id = Guid.NewGuid(),
                    Name = "Create Menu",
                    PermissionId = menuSettingsPermissionId
                },
                new SubPermission
                {
                    Id = Guid.NewGuid(),
                    Name = "Add Item",
                    PermissionId = menuSettingsPermissionId
                }
                );

            // Seed subpermissions for CashRegister
            builder.Entity<SubPermission>().HasData(
                new SubPermission
                {
                    Id = Guid.NewGuid(),
                    Name = "Inventory Management",
                    PermissionId = cashRegisterPermissionId
                },
                new SubPermission
                {
                    Id = Guid.NewGuid(),
                    Name = "POS Integration",
                    PermissionId = cashRegisterPermissionId
                },
                new SubPermission
                {
                    Id = Guid.NewGuid(),
                    Name = "Hardware Integration",
                    PermissionId = cashRegisterPermissionId
                }
            );

            builder.Entity<SubPermission>().HasData(
               new SubPermission
               {
                   Id = Guid.NewGuid(),
                   Name = "Order Management",
                   PermissionId = tillPermissionId
               },
               new SubPermission
               {
                   Id = Guid.NewGuid(),
                   Name = "Ticket",
                   PermissionId = tillPermissionId
               },
               new SubPermission
               {
                   Id = Guid.NewGuid(),
                   Name = "Discount",
                   PermissionId = tillPermissionId
               },
               new SubPermission
               {
                   Id = Guid.NewGuid(),
                   Name = "Refunds",
                   PermissionId = tillPermissionId
               },
               new SubPermission
               {
                   Id = Guid.NewGuid(),
                   Name = "Cancel Or Void Order",
                   PermissionId = tillPermissionId
               },
               new SubPermission
               {
                   Id = Guid.NewGuid(),
                   Name = "Tips",
                   PermissionId = tillPermissionId
               },
               new SubPermission
               {
                   Id = Guid.NewGuid(),
                   Name = "EOD Balance Of Account",
                   PermissionId = tillPermissionId
               },
               new SubPermission
               {
                   Id = Guid.NewGuid(),
                   Name = "Sync To Cloud",
                   PermissionId = tillPermissionId
               },
               new SubPermission
               {
                   Id = Guid.NewGuid(),
                   Name = "Order Chat",
                   PermissionId = tillPermissionId
               }

           );

            // Seed subpermissions for Tickets
            builder.Entity<SubPermission>().HasData(
                new SubPermission
                {
                    Id = Guid.NewGuid(),
                    Name = "View All Tickets",
                    PermissionId = ticketsPermissionId
                },
                new SubPermission
                {
                    Id = Guid.NewGuid(),
                    Name = "View Ticket Status",
                    PermissionId = ticketsPermissionId
                },
                new SubPermission
                {
                    Id = Guid.NewGuid(),
                    Name = "Void Ticket Transactions",
                    PermissionId = ticketsPermissionId
                },
                new SubPermission
                {
                    Id = Guid.NewGuid(),
                    Name = "Refund Ticket",
                    PermissionId = ticketsPermissionId
                }
            );

            // Seed subpermissions for TableOrdering
            builder.Entity<SubPermission>().HasData(
                new SubPermission
                {
                    Id = Guid.NewGuid(),
                    Name = "Access Handheld Devices With Pin",
                    PermissionId = tableOrderingPermissionId
                },
                new SubPermission
                {
                    Id = Guid.NewGuid(),
                    Name = "Mirror Cash Register Privileges",
                    PermissionId = tableOrderingPermissionId
                }
            );

            // Seed subpermissions for KitchenDisplaySystem
            builder.Entity<SubPermission>().HasData(
                new SubPermission
                {
                    Id = Guid.NewGuid(),
                    Name = "View Order",
                    PermissionId = KDSPermissionId
                },
                new SubPermission
                {
                    Id = Guid.NewGuid(),
                    Name = "Fulfill Order",
                    PermissionId = KDSPermissionId
                },
                new SubPermission
                {
                    Id = Guid.NewGuid(),
                    Name = "View Order Status",
                    PermissionId = KDSPermissionId
                },
                new SubPermission
                {
                    Id = Guid.NewGuid(),
                    Name = "Edit Order Status",
                    PermissionId = KDSPermissionId
                },
                new SubPermission
                {
                    Id = Guid.NewGuid(),
                    Name = "Order Chat",
                    PermissionId = KDSPermissionId
                }
            );

            


            builder.Entity<RolePermission>().HasData(
        new RolePermission
        {
            Id = Guid.NewGuid(),
            RoleId = superAdminRoleId,
            PermissionId = menuSettingsPermissionId
        },
        new RolePermission
        {
            Id = Guid.NewGuid(),
            RoleId = superAdminRoleId,
            PermissionId = cashRegisterPermissionId
        },
        new RolePermission
        {
            Id = Guid.NewGuid(),
            RoleId = superAdminRoleId,
            PermissionId = tillPermissionId
        },
        new RolePermission
        {
            Id = Guid.NewGuid(),
            RoleId = superAdminRoleId,
            PermissionId = ticketsPermissionId
        },
        new RolePermission
        {
            Id = Guid.NewGuid(),
            RoleId = superAdminRoleId,
            PermissionId = tableOrderingPermissionId
        },
        new RolePermission
        {
            Id = Guid.NewGuid(),
            RoleId = superAdminRoleId,
            PermissionId = KDSPermissionId
        }
    
        );
        }
     
        //public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //{
        //    UpdateSoftDeleteStatuses();
        //    this.AddAuditInfo();
        //    return base.SaveChangesAsync(cancellationToken);
        //}


        //public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        //    CancellationToken cancellationToken = default)
        //{
        //    UpdateSoftDeleteStatuses();
        //    this.AddAuditInfo();
        //    return base.SaveChangesAsync(acceptAllChangesOnSuccess,
        //        cancellationToken);
        //}

        //private const string IsDeletedProperty = "IsDeleted";
        //private void UpdateSoftDeleteStatuses()
        //{
        //    foreach (var entry in ChangeTracker.Entries())
        //    {
        //        switch (entry.State)
        //        {
        //            case EntityState.Added:
        //                entry.CurrentValues[IsDeletedProperty] = false;
        //                break;
        //            case EntityState.Deleted:
        //                entry.State = EntityState.Modified;
        //                entry.CurrentValues[IsDeletedProperty] = true;
        //                break;
        //        }
        //    }
        //}

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<KYC> KYCs { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<SubPermission> SubPermissions { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuGroup> MenuGroups { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<ModifierGroup> ModifierGroups { get; set; }
        public DbSet<ModifierItem> ModifierItems { get; set; }
        public DbSet<RangePriceOption> RangePriceOptions { get; set; }
        public DbSet<TimeSpecificPriceOption> TimeSpecificPriceOptions { get; set; }
        public DbSet<PriceListEntry> PriceListEntries { get; set; }
        public DbSet<PriceOption> PriceOptions { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Tab> Tabs { get; set; }
        public DbSet<Payment> Payments { get; set; }


    }
}
