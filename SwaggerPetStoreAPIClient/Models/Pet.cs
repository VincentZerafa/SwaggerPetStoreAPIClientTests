using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerPetStoreAPIClient.Models
{
    public class Pet : ApiResponse, IEqualityComparer<Pet>
    {
        public long id { get; set; }
        public Category category { get; set; }
        public string name { get; set; }
        public List<string> photoUrls { get; set; }
        public List<Tag> tags { get; set; }
        public string status { get; set; }

        public bool Equals(Pet x, Pet y)
        {
            if (x == null || y == null)
                return false;

            bool areTagsEqual = true;
            foreach (Tag tag in x.tags)
            {
                Tag otherTag = y.tags.SingleOrDefault(t => t.id == tag.id);
                areTagsEqual = tag.Equals(tag, otherTag);
            }

            return x.id == y.id
                && x.name == y.name
                && x.status == y.status
                && x.category.Equals(x.category, y.category)
                && x.photoUrls.SequenceEqual(y.photoUrls)
                && areTagsEqual;
        }

        public int GetHashCode([DisallowNull] Pet obj)
        {
            return obj.GetHashCode();
        }
    }
}
