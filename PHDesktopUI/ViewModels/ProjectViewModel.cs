using Caliburn.Micro;
using PHDesktopUI.Librery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHDesktopUI.ViewModels
{
    public class ProjectViewModel : Screen
    {
        public ProjectModel ProjectModel { get; set; }

		private List<TaskModel> _tasks;

		public List<TaskModel> Tasks
		{
			get { return _tasks; }
			set 
			{ 
				_tasks = value; 
				NotifyOfPropertyChange(() => Tasks);
			}
		}

		private Task _selectedTask;

		public Task SelectedTask
        {
			get { return _selectedTask; }
			set 
			{ 
				_selectedTask = value;
                NotifyOfPropertyChange(() => SelectedTask);
            }
		}

		private List<UserModel> _users;

		public List<UserModel> Users
		{
			get { return _users; }
			set 
			{ 
				_users = value;
                NotifyOfPropertyChange(() => Users);
            }
		}

	}
}
