using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportProblemWebApp.Domain.Entities
{
	public abstract class EntityBase
	{
        protected EntityBase() => DateAdded = DateTime.UtcNow;

        [Required]
        public Guid Id { get; set; }

        [Display(Name = "Name (title)")]
        public virtual string Title { get; set; }


        [Display(Name = "Small description")]
        public virtual string? Subtitle { get; set; }

        [Display(Name = "Big Description")]
        public virtual string Text { get; set; }

        [Display(Name = "Title image")]
        public virtual string? TitleImagePath { get; set; }

        [Display(Name = "SEO metatag Title")]
        public virtual string? MetaTitle { get; set; }

        [Display(Name = "SEO metatag Description")]
        public virtual string? MetaDescription { get; set; }
        [Display(Name = "SEO metatag KeyWords")]
        public virtual string? KeyWords { get; set; }

		[DataType(DataType.Time)]
        public virtual DateTime DateAdded { get; set; }
	}
}
