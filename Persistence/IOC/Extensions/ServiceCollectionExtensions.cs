using Application.Implementations.Modules.Employee.Services;
using Application.Implementations.Modules.MenuSettings.Services;
using Application.Implementations.Modules.RolePermission.Services;
using Application.Implementations.Modules.TableManagement.Services;
using Application.Implementations.Modules.Ticket.Services;
using Application.Implementations.Modules.Users.Services;
using Application.Interfaces;
using Application.Interfaces.Modules.Employee.Repositories;
using Application.Interfaces.Modules.Employee.Services;
using Application.Interfaces.Modules.MenuSettings.Repositories;
using Application.Interfaces.Modules.MenuSettings.Services;
using Application.Interfaces.Modules.RolePermission.Repository;
using Application.Interfaces.Modules.RolesAndPermissions.Service;
using Application.Interfaces.Modules.Users.IService;
using Application.Interfaces.Repositories.Modules.MenuSettings.Repositories;
using Application.Interfaces.Repositories.Modules.RolePermission.Repository;
using Application.Interfaces.Repositories.Modules.Table.Repositories;
using Application.Interfaces.Repositories.Modules.Ticket.Repositories;
using Application.Interfaces.Repositories.Modules.Users.IRepository;
using Application.Interfaces.Services.Modules.TableManagement.Services;
using Application.Interfaces.Services.Modules.Ticket.Services;
using Domain.Domain.Modules.RolePermission.Entities;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.Identity;
using Persistence.MailService;
using Persistence.Messaging;
using Persistence.Repositories.MenuRepositories;
using Persistence.Repositories.MenuSettingsRepositories;
using Persistence.Repositories.Modules.Employees.Repository;
using Persistence.Repositories.Modules.RolePermission.Repository;
using Persistence.Repositories.Modules.RolePermissions.Repository;
using Persistence.Repositories.Modules.Tables.Repository;
using Persistence.Repositories.Modules.Ticket.Repository;
using Persistence.Repositories.Modules.Users.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.IOC.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                
                .AddScoped<IUserService, UserService>()
                .AddScoped<IRolePermissionService, RolePermissionService>()
                .AddScoped<IRoleService, RoleService>()
                .AddScoped<IPermissionService, PermissionService>()
                .AddScoped<ISubPermissionService, SubPermissionService>()
                .AddScoped<IEmployeeService, EmployeeService>()
                .AddScoped<IMenuService, MenuService>()
                .AddScoped<IMenuGroupService, MenuGroupService>()
                .AddScoped<IModifierGroupService, ModifierGroupService>()
                .AddScoped<IMenuItemService, MenuItemService>()
                .AddScoped<IModifierItemService, ModifierItemService>()
                .AddScoped<IPriceListService, PriceListService>()
                .AddScoped<ITableService, TableService>()
                .AddScoped<IOrderService, OrderService>()
                .AddScoped<IGuestService, GuestService>()
                .AddScoped<ITabService, TabService>()
                .AddScoped<IMailSender, MailSender>()
                .AddScoped<IMailService, MailingService>()
                .AddScoped<IRazorEngine, RazorEngine>();


        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services

            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IPermissionRepository, PermissionRepository>()
            .AddScoped<ISubPermissionRepository, SubPermissionRepository>()
            .AddScoped<IRolePermissionsRepository, RolePermissionsRepository>()
            .AddScoped<IEmployeeRepository, EmployeeRepository>()
            .AddScoped<IRoleRepository, RoleRepository>()
            .AddScoped<IMenuRepository, MenuRepository>()
            .AddScoped<IMenuGroupRepository, MenuGroupRepository>()
            .AddScoped<IModifierGroupRepository, ModifierGroupRepository>()
            .AddScoped<IModifierItemRepository, ModifierItemRepository>()
            .AddScoped<IMenuItemRepository, MenuItemRepository>()
            .AddScoped<ITableRepository, TableRepository>()
            .AddScoped<IOrderRepository, OrderRepository>()
            .AddScoped<IGuestRepository, GuestRepository>()
            .AddScoped<ITabRepository, TabRepository>();



        }

        public static IServiceCollection AddCustomIdentity(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IUserStore<User>, UserStore>();
            services.AddScoped<IRoleStore<Role>, RoleStore>();
            services.AddIdentity<User, Role>()
                .AddDefaultTokenProviders();
            return services;
        }



        public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connectionString));
            return services;
        }

        




    }
}
