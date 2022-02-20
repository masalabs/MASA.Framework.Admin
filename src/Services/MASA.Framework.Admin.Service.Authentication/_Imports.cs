global using FluentValidation;
global using FluentValidation.AspNetCore;
global using MASA.BuildingBlocks.DDD.Domain.Entities;
global using MASA.BuildingBlocks.DDD.Domain.Entities.Auditing;
global using MASA.BuildingBlocks.DDD.Domain.Repositories;
global using MASA.BuildingBlocks.Dispatcher.Events;
global using MASA.Contrib.Data.UoW.EF;
global using MASA.Contrib.DDD.Domain;
global using MASA.Contrib.DDD.Domain.Repository.EF;
global using MASA.Contrib.Dispatcher.Events;
global using MASA.Contrib.Dispatcher.IntegrationEvents.Dapr;
global using MASA.Contrib.Dispatcher.IntegrationEvents.EventLogs.EF;
global using MASA.Contrib.Service.MinimalAPIs;
global using RoleQuery = MASA.Framework.Admin.Service.Authentication.Application.Roles.Queries;
global using MASA.Framework.Admin.Service.Authentication.Domain.Repositories;
global using MASA.Framework.Admin.Service.Authentication.Infrastructure;
global using MASA.Framework.Admin.Service.Authentication.Infrastructure.EntityConfigurations;
global using MASA.Framework.Admin.Service.Authentication.Infrastructure.Extensions;
global using MASA.Framework.Admin.Service.Authentication.Infrastructure.Middleware;
global using MASA.Utils.Data.EntityFrameworkCore;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.OpenApi.Models;
global using Microsoft.Extensions.Options;
global using System.Linq.Expressions;
global using MASA.BuildingBlocks.Data.UoW;
global using MASA.Contrib.DDD.Domain.Events;
global using MASA.Framework.Admin.Service.Authentication.Domain.Aggregates.RoleAggregate;
global using MASA.Framework.Admin.Service.Authentication.Domain.Aggregates.PermissionAggregate;
global using System.Text.Json;
global using MASA.Framework.Admin.Infrastructure.Configurations.Const;
global using MASA.Framework.Admin.Infrastructure.Configurations.Response;
global using MASA.Contrib.ReadWriteSpliting.CQRS.Queries;
global using MASA.Contrib.ReadWriteSpliting.CQRS.Commands;
global using MASA.Framework.Admin.Service.Authentication.Domain.Enum;
global using MASA.Framework.Admin.Service.Authentication.Application.Roles.Commands;
global using MASA.Framework.Admin.Service.Authentication.Response.Role;
global using MASA.BuildingBlocks.Configuration;
global using MASA.Contrib.Configuration;
global using MASA.Framework.Admin.Service.Authentication.Infrastructure.Options;
global using MASA.Framework.Admin.Service.Authentication.Application.Roles.Queries;
global using MASA.Framework.Admin.Service.Authentication.Application.Permissions.Commands;
global using MASA.Framework.Admin.Service.Authentication.Response.Permission;
global using MASA.Framework.Admin.Service.Authentication.Application.Permissions.Queries;
global using MASA.Framework.Admin.Contracts.Authentication;
global using Dapr;
global using MASA.BuildingBlocks.DDD.Domain.Events;
global using MASA.Framework.Admin.Service.Authentication.Domain.Events;
global using MASA.Framework.Admin.Service.Authentication.Domain.Services;
