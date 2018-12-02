namespace Entities.ViewModels
{
    public class AddressViewModel
    {
        public string Country { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string ZipCode { get; set; }

        public override string ToString()
        {
            return $"{Street}, {City}, {ZipCode}, {Country}";
        }
    }
}