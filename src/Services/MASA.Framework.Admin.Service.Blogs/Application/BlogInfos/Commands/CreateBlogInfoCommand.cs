﻿using FluentValidation;
using MASA.Contrib.ReadWriteSpliting.CQRS.Commands;
using MASA.Framework.Admin.Contracts.Blogs.Model.BlogInfo.Model;

namespace MASA.Framework.Admin.Service.Blogs.Application.BlogInfos.Commands
{
    public class CreateBlogInfoCommandValidator : AbstractValidator<CreateBlogInfoCommand>
    {
        public CreateBlogInfoCommandValidator()
        {
            RuleFor(cmd => cmd.Id).NotEqual(default(Guid)).WithMessage("wrong id");
            RuleFor(cmd => cmd.CreationTime).GreaterThanOrEqualTo(DateTime.UtcNow.AddMinutes(-5)).WithMessage("abnormal payment time");
            RuleFor(cmd => cmd.CreationTime).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("2 abnormal payment time");


            RuleFor(cmd => cmd.Request).NotNull().WithMessage("Request is not null");
            RuleFor(cmd => cmd.Request.Title).NotEqual(default(string)).WithMessage("Title is not null");
            RuleFor(cmd => cmd.Request.Content).NotEqual(default(string)).WithMessage("Content is not null");
            RuleFor(cmd => cmd.Request.TypeId).NotEqual(default(Guid)).WithMessage("TypeId is not null");
        }
    }

    public record class CreateBlogInfoCommand : Command
    {
        public CreateBlogInfoCommand()
        {
        }

        public CreateBlogInfoCommand(CreateBlogInfoModel request)
        {
            this.Request = request;
        }

        public CreateBlogInfoModel Request { get; set; }
    }
}
