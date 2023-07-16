using FreeCourse.Services.Catalog.Models;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace FreeCourse.Services.Catalog.Dtos
{
    public class CategoryDto
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
