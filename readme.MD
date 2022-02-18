# .Net Modal Binding
Although .net  provide built in modal binding to bind your incoming data to the .net types but there may be a scenario where we need to interact and manipolate data before actually bind to .net types. In that case, we use custom modal binder.

## How Modal Binder works
Built in modal binder get incoming data from http request (body, form, query, routs, header) and finds the appropriate action method from controller. For example we have below action method

```c#
[HttpGet("{id}")]
public ActionResult<Pet> GetById(int id, bool dogsOnly)
```
and And the app receives a request with this URL:

https://contoso.com/api/pets/2?DogsOnly=true

Model binding goes through the following steps after the routing system selects the action method:

- Finds the first parameter of GetById, an integer named id.
Looks through the available sources in the HTTP request and finds id = "2" in route data.
- Converts the string "2" into integer 2.
- Finds the next parameter of GetById, a boolean named dogsOnly.
- Looks through the sources and finds "DogsOnly=true" in the query string. Name matching is not case-sensitive.
Converts the string "true" into boolean true.

The framework then calls the GetById method, passing in 2 for the id parameter, and true for the dogsOnly parameter.


## Custom Modal Binding

For example, you have to get an array of countries in action method by http get request. Then how can we do. We need to write custom modal binder to get all incoming data from query string/header/body/form and convert to an array of string. Then bind it to the action method.

You just to create a class (CustomModalBinder), inherit from IModalBinder and implement its method. In this method, receive incoming data from query string, convert it into array of string and forward to controller.

```c#
public class CustomModalBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var reqData = bindingContext.HttpContext.Request;
        var qsData = reqData.Query;

        //first parameter is the name/key of query string
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
```

Now go to controller action method and write attribute as
```c#
[HttpGet("api/{controller}/add")]
public IActionResult add([ModelBinder(typeof(CustomModalBinder))] string[] countries)
{
    return Ok(countries);
}
```

Nor run the app and go to below url in browser
https://localhost:7013/api/countries/add?countries=pakistan|chins|iran|iraq
