global using FluentValidation;
global using FluentValidation.AspNetCore;
global using MASA.BuildingBlocks.Dispatcher.Events;
global using MASA.Contrib.Data.UoW.EF;
global using MASA.Contrib.DDD.Domain;
global using MASA.Contrib.DDD.Domain.Repository.EF;
global using MASA.Contrib.Dispatcher.Events;
global using MASA.Contrib.Dispatcher.IntegrationEvents.Dapr;
global using MASA.Contrib.Dispatcher.IntegrationEvents.EventLogs.EF;
global using MASA.Contrib.Service.MinimalAPIs;
global using MASA.Framework.Admin.Configuration.Infrastructure;
global using MASA.Framework.Admin.Configuration.Infrastructure.EntityConfigurations;
global using MASA.Framework.Admin.Configuration.Infrastructure.Extensions;
global using MASA.Framework.Admin.Configuration.Infrastructure.Middleware;
global using MASA.Utils.Data.EntityFrameworkCore;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.OpenApi.Models;
global using System.Linq.Expressions;
global using MASA.BuildingBlocks.DDD.Domain.Entities.Auditing;
global using MenuCommand = MASA.Framework.Admin.Configuration.Application.Menu.Commands;
global using MenuQuery = MASA.Framework.Admin.Configuration.Application.Menu.Queries;
global using MASA.Framework.Admin.Configuration.Domain.Aggregate;
global using MASA.BuildingBlocks.DDD.Domain.Repositories;
global using MASA.Contrib.ReadWriteSpliting.CQRS.Queries;
global using MASA.Contrib.Configuration;
global using MASA.Framework.Admin.Configuration.Services;
global using Microsoft.Extensions.Options;
global using MASA.Utils.Exceptions.Extensions;
global using MASA.Framework.Admin.Configuration.Response.Menu;
global using MASA.Framework.Admin.Configuration.Application.Menu.Commands;
global using MASA.Framework.Admin.Configuration.Application.Menu.Queries;
global using MASA.Contrib.ReadWriteSpliting.CQRS.Commands;
global using System.Globalization;
global using MASA.Framework.Admin.Infrastructure.Configurations.Response;
global using MASA.Framework.Admin.Infrastructure.Configurations.Const;
global using MASA.BuildingBlocks.Configuration;
global using MASA.Framework.Admin.Configuration.Infrastructure.Options;
global using Microsoft.EntityFrameworkCore;
global using MASA.Utils.Development.Dapr.AspNetCore;
