using KoalaKit.Persistence;

namespace Koalakit.Sample.SimpleApi.Entities
{
    public abstract class SampleForecastBaseEntity : IKoalaEntity
    {
        public int Id { get; set; }
        public Guid ExternalId { get; set; }
        public DateTime CreatedUtc { get; set; }
        public DateTime? UpdatedUtc { get; set; }
        public bool? Deleted { get; set; }
    }
}
