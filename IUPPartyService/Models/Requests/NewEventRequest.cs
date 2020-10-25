namespace IUPPartyService.Models.Requests
{
    public class NewEventRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaxPeople { get; set; }
        public string DateStart { get; set; }
        public string DateEnd { get; set; }
        public string Host { get; set; }
        public string HostName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Image { get; set; }
    }
}