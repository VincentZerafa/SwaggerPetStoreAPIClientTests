using System.Diagnostics.CodeAnalysis;

namespace SwaggerPetStoreAPIClient.Models
{
    public class Tag : IEqualityComparer<Tag>
    {
        public long id { get; set; }
        public string name { get; set; }

        public bool Equals(Tag? x, Tag? y)
        {
            if (x == null || y == null)
                return false;

            return x.id == y.id && x.name == y.name;
        }

        public int GetHashCode([DisallowNull] Tag obj)
        {
            return obj.GetHashCode();
        }
    }
}