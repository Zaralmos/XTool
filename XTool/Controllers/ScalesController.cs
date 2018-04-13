using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using XTool.Statistics;
using XTool.Data;
using XTool.Data.DB;
using XTool.Models.ActorModels;
using XTool.Models.EvaluationModels;
using XTool.Models.Roles;
using XTool.Models.Shared;
using XTool.Models.TransferModels.GraphApi;
using XTool.Models.UserManager;
using XTool.UserManager;

namespace XTool.Controllers
{
    [Authorize]
    public class ScalesController : XToolController
    {
        private readonly XToolDbContext _context;
        private readonly UserManager<XToolUser> _userManager;

        public ScalesController(XToolDbContext context, UserManager<XToolUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "superadmin, admin, technologist")]
        public IActionResult TotalScales(ScalesRequest request)
        {
            return WrapAjax(() =>
            {
                Actor actor = _context.Find<Actor>(request.ActorId)?.LoadFrom(_context)
                    ?? throw new OperationResultException(Statuses.Error, "В системе не существует актора с таким Id!");

                if (actor.Evaluations.Count == 0)
                    throw new OperationResultException(Statuses.Error, "В системе нет ни одной оценки для указанного актора!");

                int[] evaluationsIds = actor.Evaluations.Select(e => e.Id).ToArray();
                var allScales = _context.Scales.Where(s => evaluationsIds.Contains(s.EvaluationId))
                    ?? throw new OperationResultException(Statuses.Error, "У актора нет ни одной экспертной оценки!");

                Scales resultScales = null;
                switch (request.Algorithm)
                {
                    case Algorithms.Average:
                        resultScales = allScales.SimpleAverage();
                        break;
                    case Algorithms.RootMeanSquare:
                        resultScales = allScales.RootMeanSquare(request.SelectionPercent);
                        break;
                }

                return new OperationResult() { Status = Statuses.Ok, Message = "Результирующая экспертная оценка успешно сформирована", Data = resultScales };
            });
        }
        //OperationResult result = null;
        //return Json(result);

        public XToolUser CurrentUser => _userManager.GetUser(User);

        public async Task<IActionResult> IndividualScales(int actorId, int? expertId)
        {
            return await WrapAjaxAsync(async () =>
             {
                 bool isUserExpert = await _userManager.IsInRoleAsync(CurrentUser, "expert");
                 if (isUserExpert && expertId == null)
                     expertId = CurrentUser.Id;

                 if (isUserExpert && expertId != CurrentUser.Id)
                     throw new OperationResultException(Statuses.Forbidden, "У вас недостаточно прав доступа для просмотра этой оценки!");

                 XToolUser expert = _context.Users.Find(expertId)
                     ?? throw new OperationResultException(Statuses.Error, "В системе не существует эксперта с таким Id!");

                 Actor actor = _context.Find<Actor>(actorId)?.LoadFrom(_context)
                     ?? throw new OperationResultException(Statuses.Error, "В системе не существует актора с таким Id!");

                 Evaluation evaluation = actor.Evaluations.FirstOrDefault(ev => ev.ExpertId == expert.Id)?.LoadFrom(_context);

                 if (evaluation == null || evaluation.Scales == null)
                     throw new OperationResultException(Statuses.Error, "Этот эксперт ещё не поставил оценку данному актору!");

                 evaluation.Scales.Evaluation = null; // Выяснить, почему тут эта хрень!

                 return new OperationResult(Statuses.Ok, "Индивидуальная оценка успешно загружена.", evaluation.Scales);
             });
        }
    }
}