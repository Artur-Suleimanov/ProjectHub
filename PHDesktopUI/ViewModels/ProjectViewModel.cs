using Caliburn.Micro;
using PHDesktopUI.EventModels;
using PHDesktopUI.Librery.Api;
using PHDesktopUI.Librery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PHDesktopUI.ViewModels
{
    public class ProjectViewModel : Screen
    {
        public ProjectViewModel(IEventAggregator events,
                                IProjectEndpoint projectEndpoint,
                                ILoggedInUserModel loggedInUserModel,
                                ITaskEndpoint taskEndpoint)
        {
            _events = events;
            _projectEndpoint = projectEndpoint;
            _loggedInUserModel = loggedInUserModel;
            _taskEndpoint = taskEndpoint;
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

		private TaskModel _selectedTask;

		public TaskModel SelectedTask
        {
			get { return _selectedTask; }
			set 
			{ 
				_selectedTask = value;
                NotifyOfPropertyChange(() => SelectedTask);
                NotifyOfPropertyChange(() => CanDeleteTask);
                NotifyOfPropertyChange(() => CanOpenTask);
            }
		}

		private List<UserModel> _users;
        private readonly IEventAggregator _events;
        private readonly IProjectEndpoint _projectEndpoint;
        private readonly ILoggedInUserModel _loggedInUserModel;
        private readonly ITaskEndpoint _taskEndpoint;

        public List<UserModel> Users
		{
			get { return _users; }
			set 
			{ 
				_users = value;
                NotifyOfPropertyChange(() => Users);
                NotifyOfPropertyChange(() => CanAddUser);
            }
		}

        public bool CanAddUser
        {
            get
            {
                if (_loggedInUserModel.Id == ProjectModel.UserId)
                    return true;

                return false;
            }
            
        }

        public async Task AddUser()
		{
            var auipe = new AddUserInProjectEvent
            {
                Project = ProjectModel
            };

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
                //bool output = true;

                //if (SelectedUser == null)
                //    output = false;
                //else if(SelectedUser.Id == ProjectModel.UserId)
                //    output = false;
                //else if(_loggedInUserModel.Id != ProjectModel.UserId
                //    && _loggedInUserModel.Id == SelectedUser.Id)
                //{

                //}


                if (SelectedUser == null)
                    return false;

                if (SelectedUser.Id == ProjectModel.UserId)
                    return false;

                if (_loggedInUserModel.Id != ProjectModel.UserId)
                {
                    if(_loggedInUserModel.Id == SelectedUser.Id)
                        return true;
                    else
                        return false;
                }

                return true;

                    //return output;
            }
        }

        public async Task RemoveUser()
        {
            if(_loggedInUserModel.Id != SelectedUser.Id)
                await _projectEndpoint.DeleteUserFromProject(ProjectModel.Id, SelectedUser.Id, _loggedInUserModel.Id);
            else
            {
                await _projectEndpoint.DeleteUserFromProject(ProjectModel.Id, SelectedUser.Id, ProjectModel.UserId);
                await Back();
            }
                

            Users = await _projectEndpoint.GetProjectUsers(ProjectModel.Id);
            Tasks = await _projectEndpoint.GetProjectTasks(ProjectModel.Id);
        }

        public async Task CreateTask()
        {
            var cte = new CreateTaskEvent
            {
                ProjectModel = ProjectModel
            };

            await _events.PublishOnCurrentThreadAsync(cte);
        }

        public bool CanDeleteTask
        {
            get
            {
                bool output = false;

                if(SelectedTask == null)
                    output = false;
                else
                {
                    if (_loggedInUserModel.Id == ProjectModel.UserId)
                        output = true;
                    else if(_loggedInUserModel.Id == SelectedTask.InitiatorId)
                    {
                        output = true;
                    }
                }

                return output;
            }
        }

        public async Task DeleteTask()
        {
            await _projectEndpoint.DeleteTask((int)SelectedTask.Id!);
            ProjectModel.Tasks = await _projectEndpoint.GetProjectTasks(ProjectModel.Id);
            Tasks = ProjectModel.Tasks;
        }

        public bool CanOpenTask
        {
            get
            {
                bool output = true;

                if (SelectedTask == null)
                    output = false;

                return output;
            }
        }

        public async Task OpenTask()
        {
            var states = await _taskEndpoint.GetAllStates();

            var solutionText = await _taskEndpoint.GetSolutionText(SelectedTask.Id);

            //XmlDocument xml = new XmlDocument();
            //xml.StartsWith();

            //string _byteOrderMarkUtf8 = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());
            //if (xml.StartsWith(_byteOrderMarkUtf8))
            //{
            //    xml = xml.Remove(0, _byteOrderMarkUtf8.Length);
            //}

            SelectedTask.SolutionText = solutionText;

            //await _events.PublishOnCurrentThreadAsync(new OpenTaskEvent() { 
            //    TaskModel = SelectedTask,
            //    Project = ProjectModel,
            //    States = states,
            //});
            await _events.PublishOnCurrentThreadAsync(new TaskTestViewEvent()
            {
                TaskModel = SelectedTask,
                Project = ProjectModel,
                States = states,
            });
        }

        public async Task Back()
        {
            await _events.PublishOnCurrentThreadAsync(new ShowHomePageEvent());
        }
    }
}
