using Caliburn.Micro;
using PHDesktopUI.EventModels;
using PHDesktopUI.Helpers;
using PHDesktopUI.Librery.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHDesktopUI.ViewModels
{
    public class LoginViewModel : Screen
    {
        public LoginViewModel(IAPIHelper aPIHelper,
                              IEventAggregator events,
                              ILogin login)
        {
            _apiHelper = aPIHelper;
            _events = events;
            _login = login;
        }

        private string? _userName = "artur@mail.ru";
		public string UserName
        {
			get 
            { 
                if(_userName == null)
                    return string.Empty;
                return _userName; 
            }
			set 
			{
                if (value == null)
                    _userName = string.Empty;
                else _userName = value;

                NotifyOfPropertyChange(() => UserName);
                NotifyOfPropertyChange(() => CanLogIn);
            }
		}

        private string? _password = "2897qlepS!";
        private readonly IAPIHelper _apiHelper;
        private readonly IEventAggregator _events;
        private readonly ILogin _login;

        public string? Password
        {
            get 
            {
                return _password; 
            }
            set
            {
                if (value == null)
                    _password = string.Empty;
                else _password = value;

                NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }


        public bool IsErrorVisible
        {
            get 
            { 
                bool output = false;

                if(ErrorMessage?.Length > 0)
                {
                    output = true;
                }

                return output; 
            }
        }

        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set 
            {
                _errorMessage = value;
                NotifyOfPropertyChange(() => IsErrorVisible);
                NotifyOfPropertyChange(() => ErrorMessage);
            }
        }

        public bool CanLogIn
		{
            get
            {
                bool output = false;
                if (_userName?.Length > 0 && _password?.Length > 0)
                {
                    output = true;
                }

                return output;
            }
		}

		public async Task LogIn()
		{
            try
            {
                ErrorMessage = string.Empty;
                await _login.Run(_userName!, _password!);
                await _events.PublishOnUIThreadAsync(new LogOnEvent());
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        //private async Task NewMethod()
        //{
        //    var result = await _apiHelper.Authenticate(_userName!, _password!);

        //    // Capture more information about the user:
        //    await _apiHelper.GetLoggedInUserInfo(result.Access_Token);

        //    await _events.PublishOnUIThreadAsync(new LogOnEvent());
        //}

        public async Task SignUp()
        {
            await _events.PublishOnUIThreadAsync(new OpenSignupViewEvent());

        }
    }
}
