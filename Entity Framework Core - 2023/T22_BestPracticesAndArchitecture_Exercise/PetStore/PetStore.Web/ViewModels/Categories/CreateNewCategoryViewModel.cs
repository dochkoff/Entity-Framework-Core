using PetStore.Common;
using System.ComponentModel.DataAnnotations;

namespace PetStore.Web.ViewModels.Categories
{
    public class CreateNewCategoryViewModel
    {
        [Required(ErrorMessage =GlobalConstants.Validation.RequiredError)]
        [StringLength
            (GlobalConstants.Validation.CategoryNameMaxLength, 
            MinimumLength=GlobalConstants.Validation.CategoryNameMinLength, 
            ErrorMessage =GlobalConstants.Validation.LengthError)]
        public string Name { get; set; }
    }
}
