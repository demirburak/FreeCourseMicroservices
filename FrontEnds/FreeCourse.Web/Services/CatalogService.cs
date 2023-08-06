using FreeCourse.Shared.Dtos;
using FreeCourse.Web.Helpers;
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
        private readonly PhotoHelper _photoHelper;

        public CatalogService(HttpClient httpClient, IPhotoStockService photoStockService, PhotoHelper photoHelper)
        {
            _httpClient = httpClient;
            _photoStockService = photoStockService;
            _photoHelper = photoHelper;
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
            //** Custom eklendi.
            var course = await GetByCourseId(courseId);
            await _photoStockService.DeletePhoto(course.Picture);
            //***

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

            responseSuccess.Data.ForEach(x => x.Picture = _photoHelper.GetPhotoStockUrl(x.Picture));

            return responseSuccess.Data;
        }

        public async Task<List<CourseViewModel>> GetAllCourseByUserId(string userID)
        {
            var response = await _httpClient.GetAsync($"courses/GetAllByUserId/{userID}");
            if (!response.IsSuccessStatusCode) { return null; }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            responseSuccess.Data.ForEach(x => x.Picture = _photoHelper.GetPhotoStockUrl(x.Picture));

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
            var resultPhotoService = await _photoStockService.UploadPhoto(courseUpdateInput.PhotoFormFile);
            if (resultPhotoService is not null)
            {
                await _photoStockService.DeletePhoto(courseUpdateInput.Picture);
                courseUpdateInput.Picture = resultPhotoService.Url;
            }
            var response = await _httpClient.PutAsJsonAsync<CourseUpdateInput>("courses", courseUpdateInput);

            return response.IsSuccessStatusCode;
        }
    }
}
