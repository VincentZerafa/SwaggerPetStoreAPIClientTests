using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerPetStoreAPITests.APIModels.EqualityComparers
{
    class PetEqualityComparer : IEqualityComparer<Pet>
    {
        public bool Equals(Pet x, Pet y)
        {
            if (x == null || y == null)
                return false;

            bool isEqual = x.id == y.id;
            isEqual = x.name == y.name;
            isEqual = x.status == y.status;
            isEqual = x.category.id == y.category.id;
            isEqual = x.category.name == y.category.name;
            isEqual = x.photoUrls.SequenceEqual(y.photoUrls);

            foreach (Tag tag in x.tags)
                isEqual = x.tags.Equals(y.tags);

            return isEqual;
        }

        public int GetHashCode([DisallowNull] Pet obj)
        {
            return obj.GetHashCode();
        }
    }
}
