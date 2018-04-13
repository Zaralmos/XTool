using System;
using XTool.Models.EvaluationModels;
using XTool.Models.Roles;
using XTool.Models.TransferModels.GraphApi;

namespace XTool.Views.ViewCode
{
    public class GraphModel
    {
        public int GraphFormId { get; set; }

        public int ActorId { get; set; }

        public GraphType GraphType { get; set; }

        public string DataSource { get; set; }

        public Scales InitScales { get; set; }

        public int InitSelectionPercent { get; set; }

        public Algorithms InitAlgorithm { get; set; }

        public XToolUser[] Experts { get; set; }

        public GraphModel SetRandomId()
        {
            GraphFormId = new Random(DateTime.Now.Millisecond).Next();
            return this;
        }
    }
}
