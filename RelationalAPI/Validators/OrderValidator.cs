using FluentValidation;
using RelationalAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelationalAPI.Validators
{
    public class OrderValidator:AbstractValidator<NewOrderModel>
    {
        public OrderValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty().NotNull();
            RuleFor(x => x.CustomerName).NotEmpty().NotNull();
            RuleForEach(x => x.Items).SetValidator(new OrderItemValidator());
        }
    }

    public class OrderItemValidator: AbstractValidator<OrderItemModel>
    {
        public OrderItemValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().NotNull();
            RuleFor(x => x.ProductName).NotEmpty().NotNull();
            RuleFor(x => x.Quantity).GreaterThan(0);           
        }
    }
    public class OrderAttachmentValidator: AbstractValidator<OrderNewAttachmentModel>
    {
        public OrderAttachmentValidator()
        {
            RuleFor(x => x.AttachmentBase64).NotEmpty().NotNull();
            RuleFor(x => x.AttachmentName).NotEmpty().NotNull();
            RuleFor(x => x.OrderId).GreaterThan(0);
        }
    }

   
}
