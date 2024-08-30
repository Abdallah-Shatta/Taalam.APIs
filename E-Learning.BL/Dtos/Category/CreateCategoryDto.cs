using System.ComponentModel.DataAnnotations;

namespace E_Learning.BL.Dtos.Category
{
    public class CreateCategoryDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
