using Microsoft.AspNetCore.Mvc;
using PetStore.Data;
using PetStore.Services.Interfaces;
using PetStore.Web.ViewModels.Categories;


namespace PetStore.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        //Returns a view ->Views->Categories->Index.cshtml (All categories)
        public async Task<IActionResult> Index()
        {
            var categories =await _categoryService.GetAll();
            return View(categories);
        }

        //Returns a view ->Views->Categories->Create.cshtml (Create category)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //Returns a view ->Views->Categories->Create.cshtml (Create category)
        [HttpPost]
        public async Task<IActionResult> Create(CreateNewCategoryViewModel createNewCategoryViewModel)
        {

            if (!ModelState.IsValid)
            {
                return View(createNewCategoryViewModel);
            }

            await _categoryService.Create(createNewCategoryViewModel.Name);

            return RedirectToAction("Index");
        }

        //Update category
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetById(id);

            if (category == null)
            {
                //Error handling
            }

            var editCategoryViewModel = new EditCategoryViewModel
            {
                Id = category.Id,
                Name = category.Name
            };

            return View(editCategoryViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCategoryViewModel editCategoryViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(editCategoryViewModel);
            }

            await _categoryService.Update(editCategoryViewModel.Id, editCategoryViewModel.Name);

            return RedirectToAction("Index");
        }


        //Delete category
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
           var category = await _categoryService.GetById(id);
            var categoryViewModel = new DeleteCategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                CreatedOn = category.CreatedOn,
                ModifiedOn = category.ModifiedOn
            };

            return View(categoryViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteById(id);

            return RedirectToAction("Index");
        }
    }
}
