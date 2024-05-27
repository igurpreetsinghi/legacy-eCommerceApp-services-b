using System.ComponentModel.DataAnnotations;

namespace ECommerceWebAPI.DTO.Category
{
    public class CreateCategoryDTO
    {

        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
