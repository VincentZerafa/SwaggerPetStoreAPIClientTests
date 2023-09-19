using System.Diagnostics.CodeAnalysis;

namespace SwaggerPetStoreAPIClient.Models
{
    public class Category : IEqualityComparer<Category>
    {
        public long id { get; set; }
        public string name { get; set; }

        public bool Equals(Category? x, Category? y)
        {
            if (x == null || y == null)
                return false;

            return x.id == y.id && x.name.Equals(y.name);
        }

        public int GetHashCode([DisallowNull] Category obj)
        {
            return obj.GetHashCode();
        }
    }
}