﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XTool.Models.DBModels;

namespace XTool.Models.Roles
{
    public class ExpertRole : XToolRole
    {
        public ExpertRole(string name = "expert") : base(name)
        {

        }

        /// <summary>
        /// Оценки, выставленная экспертом.
        /// </summary>
        public virtual ICollection<Conclusion> Conclusions { get; set; } = new List<Conclusion>();
    }
}