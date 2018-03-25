using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XTool.Data.ModelInterfaces
{
    public interface IUpdateble<T>
    {
        //IUpdateble Update(IUpdateble model);

        void Update(T updateSource);
    }
}
