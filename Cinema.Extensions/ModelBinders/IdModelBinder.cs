using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Extensions.ModelBinders
{
    public class IdModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            bool success = false;
            int id = 0;
            if (valueResult != ValueProviderResult.None && string.IsNullOrEmpty(valueResult.FirstValue) == false)
            {
                try
                {
                    id = int.Parse(valueResult.FirstValue);
                    success = id > 0;
                }
                catch (Exception e)
                {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, e, bindingContext.ModelMetadata);
                    throw;
                }
            }
            if (success)
            {
                bindingContext.Result = ModelBindingResult.Success(id);
                return Task.CompletedTask;
            }
            else
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, "The entered ID is not valid.");
            }
            return Task.CompletedTask;
        }
    }
}
