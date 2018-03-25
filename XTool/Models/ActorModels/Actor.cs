using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using XTool.Models.EvaluationModels;
using XTool.Data.ModelInterfaces;
using XTool.Models.ModelInterfaces.DataAnnotations;
using XTool.Models.Roles;
using XTool.Models.Shared;

// ДОБАВИТЬ ПРИОРИТЕТНОСТЬ И ВРЕМЯ СОЗДАНИЯ

namespace XTool.Models.ActorModels 
{
    public class Actor : IStorageModel<int, Actor>
    {
        public int Id { get; set; }

        #region Key Info

        /// <summary>
        /// ФИО актора
        /// </summary>
        [SearchEngine]
        [Required, StringLength(200)]
        public string Name { get; set; }

        /// <summary>
        /// Пол актора
        /// </summary>
        public Sexes Sex { get; set; }

        /// <summary>
        /// Дата рождения актора
        /// </summary>
        [SearchEngine]
        public DateTime Birthday { get; set; }

        /// <summary>
        /// Перечисление текущих мест работы и соответсвующих должностей
        /// </summary>
        [StringLength(500)]
        [SearchEngine]
        public string Position { get; set; }

        /// <summary>
        /// Говорит о том, насколько оценка этого актора приоритетна для технолога
        /// </summary>
        public Priority Priority { get; set; }
        #endregion

        #region All Info

        /// <summary>
        /// Набор фотографий актора
        /// </summary>
        public virtual List<Photo> Photos { get; set; } = new List<Photo>();

        /// <summary>
        /// Список публикаций и выступлений актора
        /// </summary>
        [SearchEngine]
        public virtual List<Publication> Publications { get; set; } = new List<Publication>();

        /// <summary>
        /// Список видеозаписей
        /// </summary>
        public virtual List<Video> Videos { get; set; } = new List<Video>();

        /// <summary>
        /// Список событий в биографии актора
        /// </summary>
        [SearchEngine]
        public virtual List<BiographyEvent> BiograpphyEvents { get; set; } = new List<BiographyEvent>();

        /// <summary>
        /// Список периодов с событиями в карьере актора
        /// </summary>
        [SearchEngine]
        public virtual List<CareerPeriod> CareerPeriods { get; set; } = new List<CareerPeriod>();

        /// <summary>
        /// Список цитат актора
        /// </summary>
        [SearchEngine]
        public virtual List<Quotation> Quotations { get; set; } = new List<Quotation>();

        #endregion

        #region CustomSections

        /// <summary>
        /// Список пользовательских секций с дополнительной поясняющей информацией
        /// </summary>
        public virtual List<CustomSection> CustomSection { get; set; } = new List<CustomSection>();

        #endregion

        //public virtual XToolUser Technologist { get; set; }

        //public int TechnologistId { get; set; }

        public virtual List<Evaluation> Evaluations { get; set; } = new List<Evaluation>();

        [NotMapped]
        public int Age => DateTime.Now.Year - Birthday.Year;

        public void Update(Actor updateSource)
        {
            throw new NotImplementedException();
        }
    }
}
