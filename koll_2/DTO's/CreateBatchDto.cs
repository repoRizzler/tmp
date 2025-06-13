using System.ComponentModel.DataAnnotations;

namespace koll_2.DTOs
{
    public class CreateBatchDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Species name cannot exceed 100 characters")]
        public string Species { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Nursery name cannot exceed 100 characters")]
        public string Nursery { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "At least one responsible person is required")]
        public List<ResponsibleEmployeeDto> Responsible { get; set; } = new List<ResponsibleEmployeeDto>();
    }

    public class ResponsibleEmployeeDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Employee ID must be greater than 0")]
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Role cannot exceed 50 characters")]
        public string Role { get; set; }
    }

    public class BatchCreatedDto
    {
        public int BatchId { get; set; }
        public int Quantity { get; set; }
        public string SownDate { get; set; }
        public string ReadyDate { get; set; }
        public string Species { get; set; }
        public string Nursery { get; set; }
        public List<ResponsibleEmployeeDto> Responsible { get; set; } = new List<ResponsibleEmployeeDto>();
    }
}