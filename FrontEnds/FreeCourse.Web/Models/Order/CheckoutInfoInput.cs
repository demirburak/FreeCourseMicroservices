using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Web.Models.Order
{
    public class CheckoutInfoInput
    {
        [Display(Name = "İl")]
        public string Province { get; set; }

        [Display(Name = "İlçe")]
        public string District { get; set; }

        [Display(Name = "Cadde")]
        public string Street { get; set; }

        [Display(Name = "Posta Kodu")]
        public string ZipCode { get; set; }

        [Display(Name = "Adres")]
        public string Line { get; set; }

        [Display(Name = "Kart İsim Soyisim")]
        public string CardName { get; set; }

        [Display(Name = "Kart Numarası")]
        public string CardNumber { get; set; }

        [Display(Name = "Son Kullanım Tarih (aa/yy)")]
        public string Expiration { get; set; }

        [Display(Name = "CVV")]
        public string CVV { get; set; }

        
        public decimal TotalPrice { get; set; }
    }
}
