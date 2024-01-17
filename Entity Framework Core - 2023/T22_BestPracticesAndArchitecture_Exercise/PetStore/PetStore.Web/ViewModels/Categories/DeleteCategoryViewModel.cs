﻿namespace PetStore.Web.ViewModels.Categories
{
    public class DeleteCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set;}
    }
}
