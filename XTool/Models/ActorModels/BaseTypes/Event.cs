using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using XTool.Data.ModelInterfaces;
using XTool.Models.ModelInterfaces.DataAnnotations;

namespace XTool.Models.ActorModels.BaseTypes
{
    public abstract class Event : IStorageModel<int, Event>
    {
        public int Id { get; set; }

        [SearchEngine]
        [Required, StringLength(2000)]
        public string Description { get; set; }

        [SearchEngine]
        [Required, StringLength(20)] // Добавить бы RegularExpression
        public string Period { get; set; }

        [SearchEngine]
        [StringLength(2000)]
        public string Comment { get; set; }

        [NotMapped]
        public string Start => string.Concat(Period.Where(c => "1234567890-".Contains(c))).Split('-').FirstOrDefault();
        [NotMapped]
        public string End => string.Concat(Period.Where(c => "1234567890-".Contains(c))).Split('-').ElementAtOrDefault(1);

        public abstract void Update(Event updateSource);
    }
}
