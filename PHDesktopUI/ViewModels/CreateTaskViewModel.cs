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
    public class CreateTaskViewModel : Screen
    {
        public ProjectModel ProjectModel { get; set; }

        private readonly IEventAggregator _events;
        private readonly IProjectEndpoint _projectEndpoint;
        private readonly ILoggedInUserModel _loggedInUserModel;
        private List<UserModel> _users;
        private UserModel _selectedUser;
        private string _taskName;
        private string _taskDescription;

        public CreateTaskViewModel(IEventAggregator events,
                                   IProjectEndpoint projectEndpoint,
                                   ILoggedInUserModel loggedInUserModel)
        {
            _events = events;
            _projectEndpoint = projectEndpoint;
            _loggedInUserModel = loggedInUserModel;
        }

        public List<UserModel> Users
        {
            get { return _users; }
            set 
            {
                _users = value; 
                NotifyOfPropertyChange(() => Users);
            }
        }

        public UserModel SelectedUser
        {
            get { return _selectedUser; }
            set 
            { 
                _selectedUser = value;
                NotifyOfPropertyChange(() => SelectedUser);
                NotifyOfPropertyChange(() => CanCreateTask);
            }
        }

        public string TaskName
        {
            get { return _taskName; }
            set 
            { 
                _taskName = value;
                NotifyOfPropertyChange(() => TaskName);
                NotifyOfPropertyChange(() => CanCreateTask);
            }
        }

        public string TaskDescription
        {
            get { return _taskDescription; }
            set 
            {
                _taskDescription = value;
                NotifyOfPropertyChange(() => TaskDescription);
                NotifyOfPropertyChange(() => CanCreateTask);
            }
        }


        public bool CanCreateTask
        {
            get
            {
                bool output = true;

                if (SelectedUser == null || string.IsNullOrWhiteSpace(TaskName) || string.IsNullOrWhiteSpace(TaskDescription))
                    output = false;

                return output;
            }
        }

        public async Task CreateTask()
        {
            await _projectEndpoint.CreateTask(TaskName, TaskDescription, _loggedInUserModel.Id, SelectedUser.Id, ProjectModel.Id, 3);

            ProjectModel.Tasks = await _projectEndpoint.GetProjectTasks(ProjectModel.Id);

            await OpenProjectView();
        }

        public async Task Back()
        {
            await OpenProjectView();
        }

        private async Task OpenProjectView()
        {
            var ope = new OpenProjectEvent
            {
                ProjectModel = this.ProjectModel,
                
            };

            await _events.PublishOnCurrentThreadAsync(ope);
        }
    }
}
