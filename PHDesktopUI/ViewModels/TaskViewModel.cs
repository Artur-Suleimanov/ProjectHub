using Caliburn.Micro;
using PHDesktopUI.Librery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHDesktopUI.ViewModels
{
    public class TaskViewModel : Screen
    {
        private TaskModel _taskModel;
		private List<UserModel> _users;
        private List<StateModel> _states;
        private string _taskDescription;
        private string _rtbEditor;
        private UserModel _selectedUser;
        private StateModel _selectedState;
        private string _taskName;
        private readonly IEventAggregator _events;
        public Model2 model;

        public TaskViewModel(IEventAggregator events)
        {
            _events = events;

            model = new Model2()
            {
                Description = "<FlowDocument xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"><Paragraph Foreground=\"Red\"><Bold>Hello, this is RichTextBox</Bold></Paragraph></FlowDocument>"
            };
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

                SelectedUser = Users.FirstOrDefault( u => u.Id == TaskModel.Executor.Id);
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

                SelectedState = States.FirstOrDefault( s => s.Id == TaskModel.StateId);
                NotifyOfPropertyChange(() => SelectedState);
            }
        }

        public string TaskDescription 
        { 
            get { return _taskDescription; }
            set 
            {
                _taskDescription = value;
                NotifyOfPropertyChange(() => TaskDescription);
            }
        }

        public string RtbEditor 
        { 
            get  { return _rtbEditor; } 
            set  
            { 
                _rtbEditor = value;
                NotifyOfPropertyChange(() => RtbEditor);
            } 
        }

        public UserModel SelectedUser 
        {
            get { return _selectedUser; }
            set 
            { 
                _selectedUser = value;
                NotifyOfPropertyChange(() => SelectedUser);
            } 
        }

        public StateModel SelectedState 
        {
            get { return _selectedState; } 
            set  
            {
                _selectedState = value;
                NotifyOfPropertyChange(() => SelectedState);
            } 
        }

        public string TaskName 
        { 
            get  { return _taskName; } 
            set  
            { 
                _taskName = value;
                NotifyOfPropertyChange(() => TaskName);
            } 
        }

        public void OnLayoutUpdated()
        {
            SelectedUser = TaskModel.Executor;
            NotifyOfPropertyChange(() => SelectedUser);
        }
    }
    public class Model2
    {
        public string Description { get; set; }
    }
}
