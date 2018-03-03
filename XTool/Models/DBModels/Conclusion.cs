﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using XTool.Models.Roles;

namespace XTool.Models.DBModels
{
    /// <summary>
    /// Представляет класс оценки экспертом персоны по системе упрощённой MMPI экспертом.
    /// </summary>
    public class Conclusion : Evaluation
    {
        public Conclusion()
        {

        }

        public Conclusion(string comment = null, int expertId = 0)
        {
            if (comment != null)
                Comment = comment;
            if (ExpertId != 0)
                ExpertId = expertId;
        }

        #region properties
        public int Id { get; set; }

        public string Comment { get; set; }

        /// <summary>
        /// Id персоны, в отношении которой сформирована оценка.пше
        /// </summary>
        public int PersonId { get; set; }
        /// <summary>
        /// Персона, в отношении которой сформирована оценка.
        /// /// </summary>
        public virtual Person Person { get; set; }

        /// <summary>
        /// Id эксперта, сформировавшего оценку.
        /// </summary>
        public int ExpertId { get; set; }
        /// Эксперт, сформировавший оценку.
        public virtual XToolUser Expert { get; set; }
        #endregion

        public override IModel Update(IModel conclusion)
        {
            Conclusion temp = conclusion as Conclusion;
            Comment = temp.Comment;
            for (int i =0; i < 10; i++)
                Values[i] = temp.Values[i];
            return this;
        }
    }
}