using FluentValidation;
using RelationalAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelationalAPI.Validators
{
    public class LoginDTOValidator:AbstractValidator<LoginRequestModel>
    {
        public LoginDTOValidator()
        {
            RuleFor(x => x.user).NotEmpty().NotNull();
            RuleFor(x => x.password).NotEmpty().NotNull();
        }
    }
}
