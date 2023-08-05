using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FreeCourse.Web.Models.Catalog
{
    public class FeatureViewModel
    {
        [Display(Name = "Kurs Süresi")]
        [Required]
        public int Duration { get; set; }
    }
}
