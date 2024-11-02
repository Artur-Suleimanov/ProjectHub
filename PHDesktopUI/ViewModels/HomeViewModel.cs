using Caliburn.Micro;
using PHDesktopUI.EventModels;
using PHDesktopUI.Librery.Api;
using PHDesktopUI.Librery.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHDesktopUI.ViewModels
{
    public class HomeViewModel : Screen
    {
        private readonly IProjectEndpoint _projectEndpoint;
        private readonly IEventAggregator _events;
        private readonly ILoggedInUserModel _loggedInUserModel;

        // Sales Page Creation - A TimCo Retail Manager Video
        public HomeViewModel(IProjectEndpoint projectEndpoint,
                             IEventAggregator events,
                             ILoggedInUserModel loggedInUserModel)
        {
            _projectEndpoint = projectEndpoint;
            _events = events;
            _loggedInUserModel = loggedInUserModel;
        }

        protected override async void OnViewLoaded(object view)
        {
            await LoadProjects();
        }

        private async Task LoadProjects()
        {
            var projects = await _projectEndpoint.GetProjectsByUserId();
            Projects = new BindingList<ProjectModel>(projects);
        }

        private BindingList<ProjectModel> _projects;

        public BindingList<ProjectModel> Projects
        {
            get { return _projects; }
            set
            { 
                _projects = value; 
                NotifyOfPropertyChange(() => Projects);
            }
        }

        private ProjectModel _selectedProject;

        public ProjectModel SelectedProject
        {
            get { return _selectedProject; }
            set 
            { 
                _selectedProject = value;
                ProjectName = _selectedProject.Name;
                ProjectDescription = _selectedProject.Description;
                Tasks = new BindingList<TaskModel>(_selectedProject.Tasks);
                NotifyOfPropertyChange(() => SelectedProject);
                NotifyOfPropertyChange(() => CanOpenProject);
                NotifyOfPropertyChange(() => CanDeleteProject);
            }
        }


        private string _projectName;

        public string ProjectName
        {
            get { return _projectName; }
            set 
            { 
                _projectName = value;
                NotifyOfPropertyChange(() => ProjectName);
            }
        }

        private string _projectDescription;

        public string ProjectDescription
        {
            get { return _projectDescription; }
            set 
            {
                _projectDescription = value;
                NotifyOfPropertyChange(() => ProjectDescription);
            }
        }

        private BindingList<TaskModel> _tasks;
        

        public BindingList<TaskModel> Tasks
        {
            get { return _tasks; }
            set 
            { 
                _tasks = value; 
                NotifyOfPropertyChange(() => Tasks);
            }
        }

        public async Task CreateProject()
        {
            await _events.PublishOnUIThreadAsync(new CreateProjectEvent());
        }

        public bool CanOpenProject 
        {
            get
            {
                bool output = false;

                if (_selectedProject != null)
                {
                    output = true;
                }

                return output;
            }
        }

        public async Task OpenProject()
        {
            var ope = new OpenProjectEvent();
            ope.ProjectModel = _selectedProject;
            await _events.PublishOnUIThreadAsync(ope);
        }

        public bool CanDeleteProject
        {
            get
            {
                bool output = false;

                if (_selectedProject != null && _selectedProject.UserId == _loggedInUserModel.Id)
                {
                    output = true;
                }

                return output;
            }
        }

        public async Task DeleteProject()
        {
            await _projectEndpoint.DeleteProject(SelectedProject.Id);
            await LoadProjects();
            Tasks.Clear();
            ProjectDescription = string.Empty;
            ProjectName = string.Empty;
        }
    }
}
