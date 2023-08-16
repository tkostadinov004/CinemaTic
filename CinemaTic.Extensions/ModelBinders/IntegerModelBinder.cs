using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CinemaTic.Extensions.ModelBinders
{
    public class IntegerModelBinder : IModelBinder
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
