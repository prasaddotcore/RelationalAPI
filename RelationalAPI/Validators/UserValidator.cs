using FluentValidation;
using RelationalAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelationalAPI.Validators
{
    public class NewUserValidator:AbstractValidator<NewUserModel>
    {
        public NewUserValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.Email).NotEmpty().NotNull();
            RuleFor(x => x.Password).NotEmpty().NotNull();
            RuleFor(x => x.RoleId).NotEmpty().NotNull();
        }
    }

    public class EditUserValidator : AbstractValidator<EditUserModel>
    {
        public EditUserValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
                       
        }
    }
}
