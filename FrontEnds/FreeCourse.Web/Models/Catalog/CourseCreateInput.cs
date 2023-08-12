using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace FreeCourse.Web.Models.Catalog
{
    public class CourseCreateInput
    {
        [Display(Name = "Kurs İsmi")]
        public string Name { get; set; }
        
        [Display(Name = "Kurs Açıklama")]
        public string Description { get; set; }

        [Display(Name = "Kurs Fiyat")]
        public decimal Price { get; set; }

        public string UserId { get; set; } = string.Empty;

        public string Picture { get; set; } = string.Empty;

        public FeatureViewModel Feature { get; set; }

        [Display(Name = "Kurs Kategori")]
        public string CategoryId { get; set; }

        [Display(Name = "Kurs Resim")]
        public IFormFile PhotoFormFile { get; set; }
    }
}
