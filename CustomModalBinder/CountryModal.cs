using Microsoft.AspNetCore.Mvc;

namespace CustomModalBinder
{
#nullable disable
    [ModelBinder(typeof(CustomModalBinderCountryDetails))]
    public class CountryModal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Population { get; set; }
        public int Area { get; set; }
    }
}
