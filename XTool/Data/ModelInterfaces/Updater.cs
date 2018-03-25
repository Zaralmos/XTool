using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XTool.Data.ModelInterfaces
{
    public static class Updater
    {
        //public static object Update(object o1, object o2)
        //{
        //    object result;
        //    if(o1 is IUpdateble && o2 is IUpdateble)
        //    {
        //        result = (o1 as IUpdateble).Update(o2 as IUpdateble); 
        //    }
        //    else
        //    {
        //        result = o1;
        //    }
        //    return result;
        //}

        public static T UpdateFast<T> (this T updateTarget, T updateSource) where T : IUpdateble<T>
        {
            updateTarget.Update(updateSource);
            return updateTarget;
        }
    }
}
