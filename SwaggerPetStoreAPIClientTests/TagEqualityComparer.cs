using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerPetStoreAPITests.APIModels.EqualityComparers
{
    public class TagEqualityComparer : IEqualityComparer<Tag>
    {
        public bool Equals(Tag x, Tag y)
        {
            if (x == null || y == null)
                return false;

            return x.id == y.id && x.name.Equals(y.name);
        }

        public int GetHashCode([DisallowNull] Tag obj)
        {
            return obj.GetHashCode();
        }
    }
}
