namespace FreeCourse.Web.Models.Discount
{
    public class DiscountViewModel
    {

        public string UserId { get; set; } = string.Empty;

        public int Rate { get; set; }

        public string Code { get; set; } = string.Empty;

    }

    public class DiscountApplyInput
    {
        public string Code { get; set; }
    }
}
