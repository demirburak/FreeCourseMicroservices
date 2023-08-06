using FreeCourse.Shared.Dtos;
using FreeCourse.Web.Models;
using FreeCourse.Web.Models.Catalog;
using FreeCourse.Web.Services.Interfaces;
using System.Net.Http.Json;

namespace FreeCourse.Web.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _httpClient;
        private readonly IPhotoStockService _photoStockService;

        public CatalogService(HttpClient httpClient, IPhotoStockService photoStockService)
        {
            _httpClient = httpClient;
            _photoStockService = photoStockService;
        }


        public async Task<bool> CreateCourse(CourseCreateInput courseCreateInput)
        {
            var resultPhotoService = await _photoStockService.UploadPhoto(courseCreateInput.PhotoFormFile);
            if(resultPhotoService is not null)
            {
                courseCreateInput.Picture = resultPhotoService.Url;
            }

            var response = await _httpClient.PostAsJsonAsync<CourseCreateInput>("courses", courseCreateInput);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCourse(string courseId)
        {
            var response = await _httpClient.DeleteAsync($"courses/{courseId}");

            return response.IsSuccessStatusCode;
        }

        public async Task<List<CategoryViewModel>> GetAllCategory()
        {
            var response = await _httpClient.GetAsync("categories");
            if (!response.IsSuccessStatusCode) { return null; }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CategoryViewModel>>>();

            return responseSuccess.Data;
        }

        public async Task<List<CourseViewModel>> GetAllCourse()
        {
            var response = await _httpClient.GetAsync("courses");
            if (!response.IsSuccessStatusCode) { return null; }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            return responseSuccess.Data;
        }

        public async Task<List<CourseViewModel>> GetAllCourseByUserId(string userID)
        {
            var response = await _httpClient.GetAsync($"courses/GetAllByUserId/{userID}");
            if (!response.IsSuccessStatusCode) { return null; }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            return responseSuccess.Data;
        }

        public async Task<CourseViewModel> GetByCourseId(string courseId)
        {
            var response = await _httpClient.GetAsync($"courses/{courseId}");
            if (!response.IsSuccessStatusCode) { return null; }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<CourseViewModel>>();

            return responseSuccess.Data;
        }

        public  async Task<bool> UpdateCourse(CourseUpdateInput courseUpdateInput)
        {
            var response = await _httpClient.PutAsJsonAsync<CourseUpdateInput>("courses", courseUpdateInput);

            return response.IsSuccessStatusCode;
        }
    }
}
