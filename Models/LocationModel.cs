namespace AAOAdmin.Models
{
    public class LocationModel
    {
        public int LocationId { get; set; }

        public string LocationAddress { get; set; }

        public string LocationPostalCode { get; set; }

        public int CityId { get; set; }
    }
}
