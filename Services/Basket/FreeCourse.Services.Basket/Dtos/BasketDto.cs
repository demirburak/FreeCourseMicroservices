namespace FreeCourse.Services.Basket.Dtos
{
    public class BasketDto
    {
        public string UserId { get; set; } = string.Empty;

        public string DiscountCode { get; set; } = string.Empty;

        public List<BasketItemDto> BasketItems { get; set; }

        public decimal TotalPrice { get => BasketItems.Sum(x => x.Quantity * x.Price); }

    }
}
