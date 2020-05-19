using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CadastroProduto.Library.Extensions
{
    public static class ValidationResultExt
    {
        public static ModelStateDictionary ConvertToModelState(this ICollection<ValidationResult> result)
        {
            var modelState = new ModelStateDictionary();

            foreach (var error in result)
            {
                foreach (var field in error.MemberNames)
                {
                    if (modelState.ContainsKey(field))
                    {
                        modelState[field].Errors.Add(error.ErrorMessage);
                    }
                    else
                    {
                        modelState.AddModelError(field, error.ErrorMessage);
                    }
                }
            }

            return modelState;
        }
    }
}
