using FreeCourse.SharedCore7.Services;
using FreeCourse.Web.Models.Catalog;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FreeCourse.Web.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public CoursesController(ICatalogService catalogService, ISharedIdentityService sharedIdentityService)
        {
            _catalogService = catalogService;
            _sharedIdentityService = sharedIdentityService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _catalogService.GetAllCourseByUserId(_sharedIdentityService.GetUserId));
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _catalogService.GetAllCategory();

            ViewBag.categoryList = new SelectList(categories, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateInput courseCreateInput)
        {
            var categories = await _catalogService.GetAllCategory();

            ViewBag.categoryList = new SelectList(categories, "Id", "Name");

            if (!ModelState.IsValid)
            {
                return View();
            }

            courseCreateInput.UserId = _sharedIdentityService.GetUserId;

            if (!await _catalogService.CreateCourse(courseCreateInput))
            {
                ViewBag.Message = "Kayıt yapılamadı.";
                return View();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(string id)
        {
            var course = await _catalogService.GetByCourseId(id);
            var categories = await _catalogService.GetAllCategory();

            if (course == null) { RedirectToAction(nameof(Index)); }

            ViewBag.categoryList = new SelectList(categories, "Id", "Name", course.CategoryId);

            CourseUpdateInput courseUpdateInput = new()
            {
                Id = course.Id,
                Name = course.Name,
                Feature = course.Feature,
                Price = course.Price,
                CategoryId = course.CategoryId,
                UserId = course.UserId,
                Description = course.Description,
            };

            return View(courseUpdateInput);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CourseUpdateInput courseUpdateInput)
        {
            var categories = await _catalogService.GetAllCategory();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name", courseUpdateInput.CategoryId);

            courseUpdateInput.Picture = (courseUpdateInput.Picture is null) ? string.Empty : courseUpdateInput.Picture;
            ModelState.Remove("Picture");

            if (!ModelState.IsValid) { return View(); }

            if(!await _catalogService.UpdateCourse(courseUpdateInput))
            {
                ViewBag.Message = "Güncelleme yapılamadı.";
                return View();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _catalogService.DeleteCourse(id);

            return RedirectToAction(nameof(Index));
        }
    }

}
