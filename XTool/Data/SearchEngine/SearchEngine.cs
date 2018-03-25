using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XTool.Data.Storage;
using XTool.Data.ModelInterfaces;
using System.Reflection;
using XTool.Models.ModelInterfaces.DataAnnotations;

namespace XTool.Data.SearchEngine
{
    public abstract class SearchEngine<TOut, TKey> : ISearchEngine<TOut, TKey> where TOut : class, IStorageModel<TKey, TOut>
    {
        public IStorage<TKey> Storage { get; set; }

        protected PropertyInfo[] SearchableSimpleTypes { get; set; }

        protected PropertyInfo[] SearchableAdvancedTypes { get; set; }

        public int ItemCount { get; }

        public SearchEngine(IStorage<TKey> storage)
        {
            Storage = storage;
            ItemCount = Storage.Count<TOut>();
        }

        protected void ProcessBaseType()
        {
            var props = typeof(TOut).GetProperties().Where(x => x.GetCustomAttribute(typeof(SearchEngineAttribute)) != null).ToArray();
            SearchableSimpleTypes = props.Where(x => x.PropertyType.IsPrimitive || (x.PropertyType == typeof(string))).ToArray();
            SearchableAdvancedTypes = props.Except(SearchableSimpleTypes).ToArray();
        }

        public abstract List<TOut> FindItems(ISearchFilter filter);
    }
}
