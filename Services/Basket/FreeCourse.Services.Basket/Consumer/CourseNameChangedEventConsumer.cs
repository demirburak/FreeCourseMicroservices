using FreeCourse.Services.Basket.Dtos;
using FreeCourse.Services.Basket.Services;
using FreeCourse.SharedCore7.Messages;
using MassTransit;
using System.Text.Json;

namespace FreeCourse.Services.Basket.Consumer
{
    public class CourseNameChangedEventConsumer : IConsumer<CourseNameChangedEvent>
    {
        private readonly RedisService _redisService;
        private readonly IBasketService _basketService;

        public CourseNameChangedEventConsumer(RedisService redisService, IBasketService basketService)
        {
            _redisService = redisService;
            _basketService = basketService;
        }

        public async Task Consume(ConsumeContext<CourseNameChangedEvent> context)
        {

            var allKeys = _redisService.GetAllKeys().ToList();


            foreach (var key in allKeys)
            {
                BasketDto basketDto = JsonSerializer.Deserialize<BasketDto>(_redisService.GetDb().StringGet(key));
                foreach (var item in basketDto.BasketItems)
                {
                    if (item.CourseId == context.Message.CourseId)
                    {
                        item.CourseName = context.Message.UpdatedName;
                        await _basketService.SaveOrUpdate(basketDto);
                    }
                }
            }

           

        }
    }
}
