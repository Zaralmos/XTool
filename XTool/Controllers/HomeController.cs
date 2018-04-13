using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using XTool.Models;
using XTool.Data;
using XTool.Models.ActorModels;
using XTool.Data.Storage;
using XTool.Models.Roles;
using XTool.UserManager;
using XTool.Models.Shared;
using XTool.Models.UserManager;
using XTool.Models.EvaluationModels;
using XTool.Statistics;
using XTool.Data.DB;

namespace XTool.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IStorage<int> _storage;
        private XToolDbContext Context => _storage?.Context as XToolDbContext;
        private readonly UserManager<XToolUser> _userManager;

        public HomeController(IStorage<int> storage, UserManager<XToolUser> userManager) // тут поставить Сторадж
        {
            _storage = storage;
            _userManager = userManager;
        }

        public IActionResult Actors()
        {
            var actors = _storage.GetAll<Actor>();
            var actualActors = actors.OrderBy(a => a.Priority).Take(3); // Вот эту троечку вынести в конфиг // тут отсортировать по релевантности перед Take
            foreach (Actor actor in actualActors)
                actor.LoadFrom(Context);
            ViewBag.ActualActors = actualActors;
            ViewBag.Actors = actors.Take(20);
            return View();
        }

        public IActionResult Actor(int id)
        {
            IActionResult result = View();
            var actor = Context.Actors.Find(id);
            if (actor != null)
            {
                actor.LoadFrom(Context);
                ViewBag.Actor = actor;
                if (!User.IsInRole("expert"))
                {
                    int[] ids = actor.Evaluations.Select(e => e.Id).ToArray();
                    Scales scales = new Scales();
                    if (ids.Length > 0)
                    {
                        Context.Scales.Where(s => ids.Contains(s.EvaluationId))?.RootMeanSquare();
                    }
                    ViewBag.Scales = scales;
                    ViewBag.Comments = actor.Evaluations.Select(e => e.Comment) ?? new List<string>();
                    int[] evaluationIds = Context.Scales.Select(s => s.EvaluationId).ToArray();
                    int[] expertsIds = actor.Evaluations.Where(e => evaluationIds.Contains(e.Id)).Select(e => e.ExpertId).ToArray();

                    ViewBag.Experts = Context.Users.Where(u => expertsIds.Contains(u.Id)).ToArray();
                    result = View("TechnologistActor");
                }
                else
                {
                    Evaluation evaluation = actor.Evaluations.FirstOrDefault(e => e.ExpertId == CurrentUser.Id && e.ActorId == id);
                    if (evaluation != null)
                        ViewBag.Scales = Context.Scales.FirstOrDefault(s => s.EvaluationId == evaluation.Id) ?? new Scales();
                    else
                        ViewBag.Scales = new Scales();
                    ViewBag.Comment = evaluation?.Comment;
                }
            }
            else
                result = Error();
            return result;
        }

        public IActionResult CreateActor(Actor actor)
        {
            IActionResult result = null;
            int actorId = -1;
            if (_storage.GetAll<Actor>().FirstOrDefault(actr => actr.Name == actor.Name) != null)
            {
                result = Json(new OperationResult() { Status = Statuses.Error, Message = "В базе уже существует актор с таким именем!" });
            }
            else
            {
                actorId = _storage.Add(actor);
                if (actorId != -1)
                    result = Json(new OperationResult() { Status = Statuses.Ok, Data = actorId, Message = "Новый актор успешно добавлен." });
            }
            return result;
        }

        public IActionResult Error()
        {
            return View(/*new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }*/);
        }

        public XToolUser CurrentUser => _userManager.GetUser(User);

        #region AJAX actions

        //[HttpPost]
        public IActionResult Comment(int id, string comment)
        {
            OperationResult result = null;
            try
            {
                Evaluation evaluation = _storage.GetAll<Evaluation>().FirstOrDefault(e => e.ActorId == id && e.ExpertId == CurrentUser.Id);
                if (evaluation != null)
                {
                    evaluation.Comment = comment;
                    _storage.Update(evaluation.Id, evaluation);
                    result = new OperationResult() { Status = Statuses.Ok, Message = "Комментарий успешно изменён", Data = evaluation.Id };
                }
                else
                {
                    _storage.Add(new Evaluation() { ActorId = id, ExpertId = CurrentUser.Id, Comment = comment });
                    result = new OperationResult() { Status = Statuses.Ok, Message = "Комментарий успешно добавлен" };
                }
            }
            catch
            {
                result = new OperationResult() { Status = Statuses.Error, Message = "Произошла ошибка при сохранении комментария" };
            }
            return Json(result);
        }

        Evaluation ScalesEvaluation(Scales scales) => (_storage.Context as XToolDbContext).Evaluations.FirstOrDefault(e => e.Id == scales.EvaluationId);

        //[HttpPost]
        public IActionResult Scales(int id, Scales newScales)
        {
            newScales.Id = default(int);
            OperationResult result = null;
            try
            {
                Scales scales = (_storage.Context as XToolDbContext).Scales.FirstOrDefault(s => FindEval(s, id));
                if (scales != null)
                {
                    _storage.Update(scales.Id, newScales);
                    result = new OperationResult() { Status = Statuses.Ok, Message = "Экспертная оценка успешно обновлена" };
                }
                else
                {
                    Evaluation evaluation = Context.Evaluations.FirstOrDefault(e => e.ExpertId == CurrentUser.Id && e.ActorId == id);
                    if (evaluation != null)
                    {
                        newScales.EvaluationId = evaluation.Id;
                        _storage.Add(newScales);
                    }
                    else
                        _storage.Add(new Evaluation() { ExpertId = CurrentUser.Id, Scales = newScales, ActorId = id });
                    result = new OperationResult() { Status = Statuses.Ok, Message = "Экспертная оценка успешно добавлена" };
                }
            }
            catch
            {
                result = new OperationResult() { Status = Statuses.Error, Message = "Произошла ошибка при сохранении экспертной оценки!" };
            }
            return Json(result);
        }

        // refactor this!
        private bool FindEval(Scales scale, int id)
        {
            Evaluation evaluation = ScalesEvaluation(scale);
            if (evaluation == null)
                return false;
            if (evaluation.ActorId == id && evaluation.ExpertId == CurrentUser.Id)
                return true;
            else return false;
        }

        #endregion
    }
}
