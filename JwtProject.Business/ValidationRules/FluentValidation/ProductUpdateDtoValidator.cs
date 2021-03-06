using FluentValidation;
using jwtProject.Entities.Concrete;
using jwtProject.Entities.Dtos.ProductDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwtProject.Business.ValidationRules.FluentValidation
{
    public class ProductUpdateDtoValidator:AbstractValidator<ProductUpdateDto>
    {
        public ProductUpdateDtoValidator()
        {
            RuleFor(I => I.Id).InclusiveBetween(0, int.MaxValue);
            RuleFor(I => I.Name).NotEmpty().WithMessage("Ad alanı boş bırakılamaz");
        }
     
    }
}
