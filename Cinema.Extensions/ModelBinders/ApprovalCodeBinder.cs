using Cinema.Data.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Extensions.ModelBinders
{
    public class ApprovalCodeBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            bool success = false;
            ApprovalStatus code = 0;
            if (valueResult != ValueProviderResult.None && string.IsNullOrEmpty(valueResult.FirstValue) == false)
            {
                try
                {
                    code = Enum.Parse<ApprovalStatus>(valueResult.FirstValue);
                    success = code >= ApprovalStatus.Approved && code <= ApprovalStatus.DeniedApproval;
                }
                catch (Exception e)
                {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, e, bindingContext.ModelMetadata);
                    throw;
                }
            }
            if (success)
            {
                bindingContext.Result = ModelBindingResult.Success(code);
                return Task.CompletedTask;
            }
            else
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, "The entered status code is not valid.");
            }
            return Task.CompletedTask;
        }
    }
}
