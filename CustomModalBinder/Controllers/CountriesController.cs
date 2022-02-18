using Microsoft.AspNetCore.Mvc;

namespace CustomModalBinder.Controllers
{
    public class CountriesController : Controller
    {
        [HttpGet("api/{controller}/add")]
        public IActionResult add([ModelBinder(typeof(CustomModalBinder))] string[] countriesList)
        {
            return Ok(countriesList);
        }


        [HttpGet("api/{controller}/{id}")]
        public IActionResult GetCountryDetail([ModelBinder(Name = "id")] CountryModal country)
        {
            return Ok(country);
        }

    }
}
