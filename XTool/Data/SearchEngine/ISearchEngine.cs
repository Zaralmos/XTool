using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XTool.Data.Storage;
using XTool.Data.ModelInterfaces;
using System.Reflection;

namespace XTool.Data.SearchEngine
{
    public interface ISearchEngine<TOut, TKey> where TOut : class, IStorageModel<TKey, TOut>
    {
        IStorage<TKey> Storage { get; }

        int ItemCount { get; }

        List<TOut> FindItems(ISearchFilter filter);
    }
}
