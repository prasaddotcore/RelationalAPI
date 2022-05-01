using FluentValidation;
using RelationalAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelationalAPI.Validators
{
    public class UserClaimValidator:AbstractValidator<UserClaimDTO>
    {
        public UserClaimValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().NotNull();
            RuleFor(x => x.ClaimId).NotEmpty().NotNull();
        }
    }
}
