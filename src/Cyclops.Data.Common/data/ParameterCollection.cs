using System.Collections.ObjectModel;

namespace Cyclops.Data.Common
{
    public class ParameterCollection : KeyedCollection<string, Parameter>
    {
        protected override string GetKeyForItem(Parameter item)
        {
            return item.Key;
        }
    }
}
