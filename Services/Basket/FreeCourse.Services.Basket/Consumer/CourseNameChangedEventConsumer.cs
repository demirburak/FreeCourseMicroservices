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

            StackExchange.Redis.RedisResult hashes = _redisService.GetDb().Execute("KEYS", "*");

            List<BasketDto> updateBasketDtos = new();
            foreach (var hash in hashes.ToDictionary())
            {
                var tempValue = _redisService.GetDb().StringGet(hash.Key);
                var basketDto = JsonSerializer.Deserialize<BasketDto?>(tempValue);
                if (basketDto is not null)
                {
                    basketDto.BasketItems.ForEach(x =>
                    {
                        if (x.CourseId == context.Message.CourseId)
                        {
                            x.CourseName = context.Message.UpdatedName;
                            updateBasketDtos.Add(basketDto);
                        }
                    });
                }
            }

            foreach (var basketDto in updateBasketDtos)
            {
                await _basketService.SaveOrUpdate(basketDto);
            }

        }
    }
}
