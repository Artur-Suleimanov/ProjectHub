using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using PHDesktopUI.EventModels;
using PHDesktopUI.Librery.Api;
using PHDesktopUI.Librery.Models;
using PHDesktopUI.Views;
using static PHDesktopUI.ViewModels.TaskTestViewModel;

namespace PHDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>, IHandle<CreateProjectEvent>, 
        IHandle<ShowHomePageEvent>, IHandle<OpenProjectEvent>, IHandle<AddUserInProjectEvent>,
        IHandle<CreateTaskEvent>, IHandle<OpenTaskEvent>, IHandle<TaskTestViewEvent>
    {
        private readonly IEventAggregator _events;
        private readonly ILoggedInUserModel _user;
        private readonly IProjectEndpoint _projectEndpoint;

        //private readonly LoginViewModel _loginVM;
        public ShellViewModel(IEventAggregator events,
                              ILoggedInUserModel user,
                              IProjectEndpoint projectEndpoint) 
                              //LoginViewModel loginVM)
        {
            _events = events;
            _user = user;
            _projectEndpoint = projectEndpoint;
            //_loginVM = loginVM;

            _events.SubscribeOnPublishedThread(this);
            ActivateItemAsync(IoC.Get<LoginViewModel>(), new CancellationToken());

        }

        public async Task HandleAsync(LogOnEvent message, CancellationToken cancellationToken)
        {
            await ActivateItemAsync(IoC.Get<HomeViewModel>(), cancellationToken);
            //NotifyOfPropertyChange(() => IsLoggedIn);
            //NotifyOfPropertyChange(() => IsLoggedOut);
        }

        public async Task HandleAsync(CreateProjectEvent message, CancellationToken cancellationToken)
        {
            await ActivateItemAsync(IoC.Get<CreateProjectViewModel>(), cancellationToken);
        }

        public async Task HandleAsync(ShowHomePageEvent message, CancellationToken cancellationToken)
        {
            await ActivateItemAsync(IoC.Get<HomeViewModel>(), cancellationToken);
        }

        public async Task HandleAsync(OpenProjectEvent message, CancellationToken cancellationToken)
        {
            var pvm = IoC.Get<ProjectViewModel>();
            pvm.ProjectModel = message.ProjectModel;
            pvm.ProjectModel.Users = await _projectEndpoint.GetProjectUsers(message.ProjectModel.Id);
            pvm.Tasks = message.ProjectModel.Tasks;
            pvm.Users = pvm.ProjectModel.Users;
            await ActivateItemAsync(pvm, cancellationToken);
        }

        public async Task HandleAsync(AddUserInProjectEvent message, CancellationToken cancellationToken)
        {
            var auipv = IoC.Get<AddUserInProjectViewModel>();
            auipv.Project = message.Project;
            await ActivateItemAsync(auipv, cancellationToken);
        }

        public async Task HandleAsync(CreateTaskEvent message, CancellationToken cancellationToken)
        {
            var ctvm = IoC.Get<CreateTaskViewModel>();
            ctvm.ProjectModel = message.ProjectModel;
            ctvm.Users = message.ProjectModel.Users;
            await ActivateItemAsync(ctvm, cancellationToken);
        }

        public async Task HandleAsync(OpenTaskEvent message, CancellationToken cancellationToken)
        {
            var tvm = IoC.Get<TaskViewModel>();
            tvm.TaskModel = message.TaskModel;
            tvm.Users = message.Project.Users;
            tvm.States = message.States;
            tvm.TaskDescription = message.TaskModel.Description;
            tvm.TaskName = message.TaskModel.Name;
            await ActivateItemAsync(tvm, cancellationToken);
        }

        public async Task HandleAsync(TaskTestViewEvent message, CancellationToken cancellationToken)
        {
            var tvm = IoC.Get<TaskTestViewModel>();
            tvm.TaskModel = message.TaskModel;
            tvm.ProjectModel = message.Project;
            tvm.Users = message.Project.Users;
            tvm.States = message.States;
            tvm.TaskDescription = message.TaskModel.Description;
            tvm.TaskName = message.TaskModel.Name;

            if(string.IsNullOrWhiteSpace(message.TaskModel.SolutionText) == false)
            {
                var solutionTestFromTask = message.TaskModel.SolutionText.Replace(@"\", "").TrimStart('"').TrimEnd('"');

                tvm.SolutionText = solutionTestFromTask;
            }

            await ActivateItemAsync(tvm, cancellationToken);
        }
    }
}
