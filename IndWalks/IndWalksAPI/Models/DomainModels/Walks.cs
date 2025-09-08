namespace IndWalksAPI.Models.DomainModels
{
    public class Walks
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Desp { get; set; }
        public string LenInKms { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficultId { get; set; }
        public Guid RegionId { get; set; }
        public Difficulty Difficulty { get; set; }
        public Region Region { get; set; }
    }
}
