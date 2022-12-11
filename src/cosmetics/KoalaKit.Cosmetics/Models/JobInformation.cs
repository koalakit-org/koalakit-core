using System.Diagnostics.CodeAnalysis;

namespace KoalaKit.Cosmetics
{
    [Serializable]
    public struct JobInformation : IEquatable<JobInformation>
    {
        public JobInformation(string information)
        {
            Number = information.Split(":")[0];
            Title= information.Split(":")[1];
        }

        public string Number { get; set; }
        public string Title { get; set; }

        public override string ToString() => $"{Number}:{Title}";
        public int GetHashCode([DisallowNull] JobInformation obj)
        {
            return base.GetHashCode();
        }

        public bool Equals(JobInformation other)
        {
            return Number.Equals(other.Number);
        }
    }
}
