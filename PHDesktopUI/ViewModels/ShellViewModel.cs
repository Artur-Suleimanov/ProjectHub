using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using PHDesktopUI.EventModels;
using PHDesktopUI.Librery.Models;

namespace PHDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>, IHandle<CreateProjectEvent>
    {
        private readonly IEventAggregator _events;
        private readonly ILoggedInUserModel _user;
        //private readonly LoginViewModel _loginVM;
        public ShellViewModel(IEventAggregator events,
                              ILoggedInUserModel user) 
                              //LoginViewModel loginVM)
        {
            _events = events;
            _user = user;
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
    }
}
