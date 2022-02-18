using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CustomModalBinder
{
    public class CustomModalBinderCountryDetails : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var modelName = bindingContext.ModelName;
            var value = bindingContext.ValueProvider.GetValue(modelName);
            var result = value.First();
        

            if(!int.TryParse(result, out var id))
            {
                return Task.CompletedTask;
            }


            //Here based on received id from http request, search actual country detail from DB or other source
            var model = new CountryModal
            {
                Id = id,
                Name = "Pakistan",
                Area = 295,
                Population = 25
            };

            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;

        }
    }
}
