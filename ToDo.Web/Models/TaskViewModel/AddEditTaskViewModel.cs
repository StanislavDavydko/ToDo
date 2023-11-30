using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ToDo.Web.Models.TaskViewModel
{
    public class AddEditTaskViewModel
    {
        public int Id { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public Legal.Enums.Type TaskType { get; set; }

        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Required]
        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [DisplayName("Updated Date")]
        public DateTime UpdatedDate { get; set; }


        public AddEditTaskViewModel() { }

        public AddEditTaskViewModel(Task task)
        {
            if (task != null)
            {
                Id = task.Id;
                Active = task.Active;
                TaskType = task.Type;
                Name = task.Name;
                Description = task.Description;
                CreatedDate = task.CreatedDate;
                UpdatedDate = task.UpdatedDate;
            }
        }
    }
}
