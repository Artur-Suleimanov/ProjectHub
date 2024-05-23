using Caliburn.Micro;
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

        // Sales Page Creation - A TimCo Retail Manager Video
        public HomeViewModel(IProjectEndpoint projectEndpoint)
        {
            _projectEndpoint = projectEndpoint;
        }

        protected override async void OnViewLoaded(object view)
        {
            await LoadProjects();
        }

        private async Task LoadProjects()
        {
            var projects = await _projectEndpoint.GetAll();
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

    }
}
