﻿using Caliburn.Micro;
using PHDesktopUI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHDesktopUI.ViewModels
{
    public class LoginViewModel : Screen
    {
        public LoginViewModel(IAPIHelper aPIHelper)
        {
            _apiHelper = aPIHelper;
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
			}
		}

        private string? _password = "2897qlepS!";
        private readonly IAPIHelper _apiHelper;

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

		public async Task LogIn(string userName, string password)
		{
            try
            {
                var result = await _apiHelper.Authenticate(userName, password);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
        }
    }
}
