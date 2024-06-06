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
    class CreateProjectViewModel : Screen
    {
		private string? _projectName;

        public CreateProjectViewModel(IProjectEndpoint projectEndpoint,
                                      IEventAggregator events)
        {
            _projectEndpoint = projectEndpoint;
            _events = events;
        }

        public string ProjectName
        {
			get { return _projectName; }
			set 
			{
                _projectName = value;
                NotifyOfPropertyChange(() => ProjectName);
                NotifyOfPropertyChange(() => CanCreateProject);
            }
		}

		private string? _projectDescription;
        private readonly IProjectEndpoint _projectEndpoint;
        private readonly IEventAggregator _events;

        public string ProjectDescription
        {
			get { return _projectDescription; }
			set 
			{
                _projectDescription = value;
                NotifyOfPropertyChange(() => ProjectDescription);
                NotifyOfPropertyChange(() => CanCreateProject);
            }
		}

		public bool CanCreateProject
		{
			get
			{
				bool output = false;

				if(ProjectName?.Length > 0 && ProjectDescription?.Length > 0) 
					output = true;

				return output;
			}
		}

		public async Task CreateProject()
		{
            await _projectEndpoint.CreateProject(new CreateProjectModel(ProjectName, ProjectDescription));

            await _events.PublishOnUIThreadAsync(new ShowHomePageEvent());
        }

        public async Task Back()
        {
            await _events.PublishOnUIThreadAsync(new ShowHomePageEvent());
        }
    }
}
