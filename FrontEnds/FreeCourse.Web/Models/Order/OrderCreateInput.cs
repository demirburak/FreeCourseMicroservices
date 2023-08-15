namespace FreeCourse.Web.Models.Order
{
    public class OrderCreateInput
    {
        public string BuyerId { get; set; }

        public List<OrderItemCreateInput> OrderItems { get; set; }

        public AddressCreateInput Address { get; set; }

        public OrderCreateInput()
        {
            OrderItems = new List<OrderItemCreateInput>();
        }
    }
}
