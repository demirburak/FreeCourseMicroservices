using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace FreeCourse.Web.Models.Catalog
{
    public class CourseCreateInput
    {
        [Display(Name = "Kurs İsmi")]
        [Required]
        public string Name { get; set; }
        
        [Display(Name = "Kurs Açıklama")]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Kurs Fiyat")]
        [Required]
        public decimal Price { get; set; }

        public string UserId { get; set; } = string.Empty;

        public string Picture { get; set; } = string.Empty;

        public FeatureViewModel Feature { get; set; }

        [Display(Name = "Kurs KAtegori")]
        [Required]
        public string CategoryId { get; set; }
    }
}
