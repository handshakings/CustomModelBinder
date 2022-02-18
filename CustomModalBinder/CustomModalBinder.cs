using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CustomModalBinder
{
    public class CustomModalBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var reqData = bindingContext.HttpContext.Request;
            var qsData = reqData.Query;

            //first parameter is the name/key of query string. must be same as incoming request query string
            //second parameter is the actual values of the first parameter 
            var result = qsData.TryGetValue("countries", out var country);

            //if received data is not null, then convert it into array
            if(result)
            {
                var array = country.ToString().Split('|');
                bindingContext.Result = ModelBindingResult.Success(array);
            }
            return Task.CompletedTask;
        }
    }
}
