using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTic.Extensions.ModelBinders
{
    public class DateModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            string dateString = valueResult.FirstValue;
            DateTime date;
            bool success = DateTime.TryParse(dateString, out date);
            if (success)
            {
                bindingContext.Result = ModelBindingResult.Success(date);
                return Task.CompletedTask;
            }
            else
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, "The entered date is not valid.");
            }
            return Task.CompletedTask;
        }
    }
}
