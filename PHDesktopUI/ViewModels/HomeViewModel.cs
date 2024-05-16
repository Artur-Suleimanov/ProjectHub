using Caliburn.Micro;
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
        // Sales Page Creation - A TimCo Retail Manager Video

        private BindingList<string> _projects;

        public BindingList<string> Projects
        {
            get { return _projects; }
            set
            { 
                _projects = value; 
                NotifyOfPropertyChange(() => Projects);
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

        private BindingList<string> _tasks;

        public BindingList<string> Tasks
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
