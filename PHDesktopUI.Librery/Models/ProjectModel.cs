using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHDesktopUI.Librery.Models
{
    public class ProjectModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public List<TaskModel> Tasks { get; set; } = new List<TaskModel>();
        public List<UserModel> Users { get; set; }
    }
}
