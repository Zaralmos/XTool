using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using XTool.Models.Shared;

namespace XTool.Controllers
{
    public abstract class XToolController : Controller
    {
        protected virtual async Task<IActionResult> WrapAjaxAsync(Func<Task<OperationResult>> action)
        {
            OperationResult result;
            try
            {
                result = await action();
            }
            catch (OperationResultException e)
            {
                result = e.OperationResult;
            }
            catch
            {
                result = OperationResult.UnknownError;
            }
            return Json(result);
        }

        protected virtual IActionResult WrapAjax(Func<OperationResult> action)
        {
            var wrappingTask = WrapAjaxAsync(async () => action());
            //wrappingTask.Start();
            wrappingTask.Wait();
            return wrappingTask.Result;
        }
    }
}