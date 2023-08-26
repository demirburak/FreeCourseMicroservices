using Dapper;
using FreeCourse.Shared.Dtos;
using Npgsql;
using System.Data;

namespace FreeCourse.Services.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<Response<NoContent>> Delete(int id)
        {

            int deleteStatus = await _dbConnection.ExecuteAsync("delete from discount where id=@Id", new { Id = id });
            return (deleteStatus > 0) ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("discount not found", 500);

        }

        public async Task<Response<List<Models.Discount>>> GetAll()
        {
            IEnumerable<Models.Discount> discounts = await _dbConnection.QueryAsync<Models.Discount>("Select * from discount");
            return Response<List<Models.Discount>>.Success(discounts.ToList(), 200);
        }

        public async Task<Response<Models.Discount>> GetByCodeAndUserId(string code, string userId)
        {
            var discount = (await _dbConnection.QueryAsync<Models.Discount>("Select * from discount where userid=@UserId and code=@Code",
                                    new { UserId = userId, Code = code })).FirstOrDefault();
            return discount is null
                ? Response<Models.Discount>.Fail("discount not found", 404)
                : Response<Models.Discount>.Success(discount, 200);

        }

        public async Task<Response<Models.Discount>> GetByID(int id)
        {
            var discount = (await _dbConnection.QueryAsync<Models.Discount>("Select * from discount where id=@Id", new { Id = id })).SingleOrDefault();
            if (discount is null)
            {
                return Response<Models.Discount>.Fail("Discount not found", 404);
            }

            return Response<Models.Discount>.Success(discount, 200);
        }

        public async Task<Response<NoContent>> Save(Models.Discount discount)
        {
            int saveStatus = await _dbConnection.ExecuteAsync("INSERT INTO discount (userId,rate,code) VALUES (@UserId,@Rate,@Code)", discount);
            if (saveStatus > 0)
            {
                return Response<NoContent>.Success(204);
            }

            return Response<NoContent>.Fail("An error occured while adding", 500);
        }

        public async Task<Response<NoContent>> Update(Models.Discount discount)
        {
            int updateStatus = await _dbConnection.ExecuteAsync("update discount set userid=@UserId,rate=@Rate,code=@Code where id=@Id",
                                        new { UserId = discount.UserId, Rate = discount.Rate, Code = discount.Code, Id = discount.Id });
            if (updateStatus > 0)
            {
                return Response<NoContent>.Success(204);
            }

            return Response<NoContent>.Fail("discount not found", 500);
        }
    }
}
