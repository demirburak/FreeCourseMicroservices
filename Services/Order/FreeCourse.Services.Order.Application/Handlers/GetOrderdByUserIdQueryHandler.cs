using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeCourse.Services.Order.Application.Queries;
using FreeCourse.Shared.Dtos;
using FreeCourse.Services.Order.Application.Dtos;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using FreeCourse.Services.Order.Infrastructure;
using Microsoft.EntityFrameworkCore;
using FreeCourse.Services.Order.Application.Mapping;

namespace FreeCourse.Services.Order.Application.Handlers
{
    public class GetOrderdByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, Response<List<OrderDto>>>
    {
        private readonly OrderDbContext _orderDbContext;

        public GetOrderdByUserIdQueryHandler(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task<Response<List<OrderDto>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
        {
            List<Domain.OrderAggregate.Order> orders = await _orderDbContext.Orders.Include(x => x.OrderItems).Where(x => x.BuyerId == request.UserId).ToListAsync();

            if (!orders.Any())
            {
                return Response<List<OrderDto>>.Success(new List<OrderDto>(), 200);
            }

            List<OrderDto> orderDtos = ObjectMapper.Mapper.Map<List<OrderDto>>(orders);

            return Response<List<OrderDto>>.Success(orderDtos, 200);
        }
    }
}
