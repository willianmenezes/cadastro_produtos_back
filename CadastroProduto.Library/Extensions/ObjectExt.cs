using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CadastroProduto.Library.Extensions
{
    public static class ObjectExt
    {
        public static void Validate<TEntity>(this TEntity obj) where TEntity : class
        {
            var context = new ValidationContext(obj, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(obj, context, results))
            {
                var modelState = results.ConvertToModelState();

                throw new Exception("Model validation error");
            }
        }
    }
}
