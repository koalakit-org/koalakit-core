using System;
using System.Collections.Generic;

namespace KoalaKit.Persistence.EfCore.Test.Models
{
    public class TestModelParent : IKoalaEntity
    {
        public int Id { get; set; }
        public Guid ExternalId { get; set; }
        public DateTime CreatedUtc { get; set; }
        public DateTime? UpdatedUtc { get; set; }
        public bool? Deleted { get; set; }
        public string Name { get; set; } = "";
        public List<TestModelChild>? Children { get; set; }
    }
}
