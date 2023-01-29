using System.ComponentModel.DataAnnotations;

namespace TransportProblemWebApp.Domain.Entities
{
    public class TextField : EntityBase
    {
        [Required]
        public string CodeWord { get; set; }

        [Display(Name = "Name of page (title)")]
        public override string Title { get; set; } = "Information about page";

        [Display(Name = "Page content")]
        public override string Text { get; set; } = "Admin fill content";

    }
}
