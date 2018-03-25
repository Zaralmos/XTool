using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XTool.Data.ModelInterfaces
{
    public interface IStorageModel<TKey, TSelf> : IUpdateble<TSelf>, IWithId<TKey>
    {
    }
}
