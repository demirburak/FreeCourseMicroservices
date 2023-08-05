using FreeCourse.Web.Models.Catalog;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<List<CourseViewModel>> GetAllCourse();

        Task<List<CategoryViewModel>> GetAllCategory();

        Task<List<CourseViewModel>> GetAllCourseByUserId(string userID);

        Task<CourseViewModel> GetByCourseId(string courseId);

        Task<bool> CreateCourse(CourseCreateInput courseCreateInput);

        Task<bool> UpdateCourse(CourseUpdateInput  courseUpdateInput);
        
        Task<bool> DeleteCourse(string courseId);

        
    }
}
