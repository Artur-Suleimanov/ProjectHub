using PHDesktopUI.Librery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHDesktopUI.EventModels
{
    public class TaskTestViewEvent
    {
        public TaskModel TaskModel { get; set; }
        public ProjectModel Project { get; set; }
        public List<StateModel> States { get; set; }
    }
}
