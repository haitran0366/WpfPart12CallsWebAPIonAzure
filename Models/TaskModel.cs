using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfPart12CallsWebAPIonAzure.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
