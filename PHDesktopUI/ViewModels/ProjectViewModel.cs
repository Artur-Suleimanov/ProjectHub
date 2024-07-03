using Caliburn.Micro;
using PHDesktopUI.EventModels;
using PHDesktopUI.Librery.Api;
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
        public ProjectViewModel(IEventAggregator events,
                                IProjectEndpoint projectEndpoint,
                                ILoggedInUserModel loggedInUserModel)
        {
            _events = events;
            _projectEndpoint = projectEndpoint;
            _loggedInUserModel = loggedInUserModel;
        }

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
        private readonly IEventAggregator _events;
        private readonly IProjectEndpoint _projectEndpoint;
        private readonly ILoggedInUserModel _loggedInUserModel;

        public List<UserModel> Users
		{
			get { return _users; }
			set 
			{ 
				_users = value;
                NotifyOfPropertyChange(() => Users);
            }
		}

		public async Task AddUser()
		{
			var auipe = new AddUserInProjectEvent();
			auipe.Project = ProjectModel;

            await _events.PublishOnUIThreadAsync(auipe);
        }


        private UserModel _selectedUser;

        public UserModel SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                NotifyOfPropertyChange(() => SelectedUser);
                NotifyOfPropertyChange(() => CanRemoveUser);
            }
        }

        public bool CanRemoveUser
        {
            get
            {
                bool output = true;

                if (SelectedUser == null)
                    output = false;
                else if(SelectedUser.Id == ProjectModel.UserId)
                    output = false;
                    
                return output;
            }
        }

        public async Task RemoveUser()
        {
            await _projectEndpoint.DeleteUserFromProject(ProjectModel.Id, SelectedUser.Id, _loggedInUserModel.Id);
            Users = await _projectEndpoint.GetProjectUsers(ProjectModel.Id);
            Tasks = await _projectEndpoint.GetProjectTasks(ProjectModel.Id);
        }
    }
}
