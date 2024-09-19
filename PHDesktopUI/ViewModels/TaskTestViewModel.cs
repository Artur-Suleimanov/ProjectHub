using Caliburn.Micro;
using PHDesktopUI.EventModels;
using PHDesktopUI.Librery.Api;
using PHDesktopUI.Librery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Action = System.Action;

namespace PHDesktopUI.ViewModels
{
    public class TaskTestViewModel : Screen
    {
        private TaskModel _taskModel;
        private List<UserModel> _users;
        private List<StateModel> _states;
        private string _taskDescription;
        private UserModel _selectedUser;
        private StateModel _selectedState;
        private string _taskName;
        private string _solutionText;
        private readonly IEventAggregator _events;
        private readonly ILoggedInUserModel _loggedInUserModel;
        private readonly IProjectEndpoint _projectEndpoint;
        private readonly ITaskEndpoint _taskEndpoint;
        private ProjectModel _projectModel;

        public string SolutionText
        {
            get => _solutionText;
            set
            {
                _solutionText = value;
                _taskModel.SolutionText = value;
                NotifyOfPropertyChange(() => SolutionText);

            }
        }

        public TaskTestViewModel(ITaskEndpoint taskEndpoint,
                                 IEventAggregator events,
                                 ILoggedInUserModel loggedInUserModel,
                                 IProjectEndpoint projectEndpoint)
        {
            _taskEndpoint = taskEndpoint;
            _events = events;
            _loggedInUserModel = loggedInUserModel;
            _projectEndpoint = projectEndpoint;
            //_solutionText = "<FlowDocument PagePadding=\"5,0,5,0\" AllowDrop=\"True\" xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"><Paragraph><Run xml:lang=\"ru-ru\">текст</Run></Paragraph></FlowDocument>";
        }

        public TaskTestViewModel()
        {
        }

        public ProjectModel ProjectModel
        {
            get { return _projectModel; }
            set { _projectModel = value; }
        }


        public TaskModel TaskModel
        {
            get => _taskModel;
            set
            {
                _taskModel = value;
            }
        }

        public List<UserModel> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                NotifyOfPropertyChange(() => Users);

                SelectedUser = Users.FirstOrDefault(u => u.Id == TaskModel.Executor.Id);
                NotifyOfPropertyChange(() => SelectedUser);
            }
        }

        public List<StateModel> States
        {
            get { return _states; }
            set
            {
                _states = value;
                NotifyOfPropertyChange(() => States);

                SelectedState = States.FirstOrDefault(s => s.Id == TaskModel.StateId);
                NotifyOfPropertyChange(() => SelectedState);
            }
        }

        private bool _isTextBoxEnabled;

        public bool IsTextBoxEnabled
        {
            get 
            {
                bool output = true;

                if (_loggedInUserModel != null && _taskModel != null)
                {
                    if (_loggedInUserModel.Id != _taskModel.InitiatorId)
                        output = false;
                }
                
                return output;
            }
            
        }

        public string TaskDescription
        {
            get { return _taskDescription; }
            set
            {
                _taskDescription = value;
                _taskModel.Description = _taskDescription;
                NotifyOfPropertyChange(() => TaskDescription);
            }
        }

        public UserModel SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                _taskModel.ExecutorId = _selectedUser.Id;
                NotifyOfPropertyChange(() => SelectedUser);
            }
        }

        public StateModel SelectedState
        {
            get { return _selectedState; }
            set
            {
                _selectedState = value;
                _taskModel.State = _selectedState.Name;
                _taskModel.StateId = _selectedState.Id;
;
                NotifyOfPropertyChange(() => SelectedState);
            }
        }

        public string TaskName
        {
            get { return _taskName; }
            set
            {
                _taskName = value;
                NotifyOfPropertyChange(() => TaskName);
            }
        }

        public void OnLayoutUpdated()
        {
            //SelectedUser = TaskModel.Executor;
            //NotifyOfPropertyChange(() => SelectedUser);
        }

        public async Task Save()
        {
            await _taskEndpoint.UpdateTask(_taskModel);
            NotifyOfPropertyChange(() => IsTextBoxEnabled);
        }


        public async Task Back()
        {

            var ope = new OpenProjectEvent()
            {
                ProjectModel = await _projectEndpoint.GetProjectById(_projectModel.Id)
            };

            await _events.PublishOnCurrentThreadAsync(ope);
        }
    }
}
