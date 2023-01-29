
using System.ComponentModel.DataAnnotations;

namespace TransportProblemWebApp.Domain.Entities
{
	public class InformationField :EntityBase
	{
        [Required(ErrorMessage = "Fill name of item")]
        [Display(Name = "Name of InformationField")]
        public override string Title { get; set; }

        [Display(Name = "Full description of InformationField")]
        public override string Text { get; set; }

        [Display(Name = "Small description of InformationField")]
        public override string Subtitle { get; set; }
	}
}
