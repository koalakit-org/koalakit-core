using System;

namespace KoalaKit.Persistence.EfCore.Test.Models
{
    public class TestModelChild : IKoalaEntity
    {
        public int Id { get; set; }
        public Guid ExternalId { get; set; }
        public DateTime CreatedUtc { get; set; }
        public DateTime? UpdatedUtc { get; set; }
        public bool? Deleted { get; set; }

        public string Name { get; set; } = "";
        public int ParentId { get; set; }
        public TestModelParent? Parent { get; set; } = new TestModelParent();
    }
}
