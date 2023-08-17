using FreeCourse.Services.Order.Infrastructure;
using FreeCourse.SharedCore7.Messages;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Application.Consumer
{
    public class CreateOrderMessageConsumer : IConsumer<CreateOrderMessageCommand>
    {
        private readonly OrderDbContext _orderDbContext;

        public CreateOrderMessageConsumer(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task Consume(ConsumeContext<CreateOrderMessageCommand> context)
        {
            var newAddress = new Domain.OrderAggregate.Address(context.Message.Address.Province, context.Message.Address.District,
                context.Message.Address.Street, context.Message.Address.ZipCode, context.Message.Address.Line);

            Domain.OrderAggregate.Order order = new Domain.OrderAggregate.Order(newAddress, context.Message.BuyerId);

            foreach (var item in context.Message.OrderItems)
            {
                order.AddOrderItem(item.ProductId, item.ProductName, item.Price, item.PictureUrl);
            }

            await _orderDbContext.Orders.AddAsync(order);

            await _orderDbContext.SaveChangesAsync();

        }
    }
}
