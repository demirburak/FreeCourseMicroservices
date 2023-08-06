using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;

namespace FreeCourse.Web.Models.Catalog
{
    public class CourseUpdateInput
    {
        public string Id { get; set; }

        [Display(Name = "Kurs İsim")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Kurs Açıklama")]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Kurs Fiyat")]
        [Required]
        public decimal Price { get; set; }

        public string UserId { get; set; }

        public string Picture { get; set; }

        public DateTime CreatedTime { get; set; }

        public FeatureViewModel Feature { get; set; }

        [Display(Name = "Kurs Kategori")]
        [Required]
        public string CategoryId { get; set; }

        [Display(Name = "Kurs Resim")]
        public IFormFile PhotoFormFile { get; set; }
    }
}
