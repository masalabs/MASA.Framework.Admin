global using FluentValidation;
global using FluentValidation.AspNetCore;
global using Masa.BuildingBlocks.Ddd.Domain.Entities.Auditing;
global using Masa.BuildingBlocks.Ddd.Domain.Repositories;
global using Masa.BuildingBlocks.Dispatcher.Events;
global using Masa.Contrib.Configuration;
global using Masa.Contrib.Data.UoW.EF;
global using Masa.Contrib.Ddd.Domain;
global using Masa.Contrib.Ddd.Domain.Repository.EF;
global using Masa.Contrib.Dispatcher.Events;
global using Masa.Contrib.Dispatcher.IntegrationEvents.Dapr;
global using Masa.Contrib.Dispatcher.IntegrationEvents.EventLogs.EF;
global using Masa.Contrib.ReadWriteSpliting.Cqrs.Commands;
global using Masa.Contrib.ReadWriteSpliting.Cqrs.Queries;
global using Masa.Contrib.Service.MinimalAPIs;
global using Masa.Framework.Admin.Configuration.Application.Menu.Commands;
global using Masa.Framework.Admin.Configuration.Application.Menu.Queries;
global using Masa.Framework.Admin.Configuration.Domain.Aggregate;
global using Masa.Framework.Admin.Configuration.Infrastructure;
global using Masa.Framework.Admin.Configuration.Infrastructure.EntityConfigurations;
global using Masa.Framework.Admin.Configuration.Infrastructure.Extensions;
global using Masa.Framework.Admin.Configuration.Infrastructure.Middleware;
global using Masa.Framework.Admin.Configuration.Response.Menu;
global using Masa.Framework.Admin.Configuration.Services;
global using Masa.Framework.Admin.Infrastructure.Configurations.Const;
global using Masa.Framework.Admin.Infrastructure.Configurations.Response;
global using Masa.Utils.Data.EntityFrameworkCore;
global using Masa.Utils.Development.Dapr.AspNetCore;
global using Masa.Utils.Exceptions.Extensions;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.OpenApi.Models;
global using System.Globalization;
global using System.Linq.Expressions;
global using MenuCommand = Masa.Framework.Admin.Configuration.Application.Menu.Commands;
global using MenuQuery = Masa.Framework.Admin.Configuration.Application.Menu.Queries;
