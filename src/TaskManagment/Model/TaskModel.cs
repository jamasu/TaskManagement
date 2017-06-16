using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagment.Model
{
    public class TaskModel
    {
        [Required]
        [Display(Name = "Task name")]
        public string TaskName { get; set; }
        public int Id { get; set; }
        [Required]
        [Display(Name = "Task message")]
        public string TaskMessage { get; set; }

        [Display(Name = "Created")]
        //TODO defaultvalue datetime.now
        public DateTime CreateDate { get; set; }
        [Required]
        [Display(Name = "Due date")]
        public DateTime DueDate { get; set; }
        [Required]
        public DateTime CompleteDate { get; set; }
        public bool Checked { get; set; }

    }
}
