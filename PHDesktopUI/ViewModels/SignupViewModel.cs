using Caliburn.Micro;
using PHDesktopUI.EventModels;
using PHDesktopUI.Librery.Api;
using PHDesktopUI.Librery.Functions;
using PHDesktopUI.Librery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PHDesktopUI.ViewModels
{
    public class SignupViewModel : Screen
    {
        private const string PasswordNotConfirmedErrorMessage = "Пароли не совпадают";
        private const string IncorrentEmail = "Некорректнеый email";
        private const string IncorrectPassword = "Некорректный пароль";
        private readonly IEventAggregator _events;
        private readonly IUserEndpoint _userEndpoint;
        private readonly ILogin _login;
        private string? _emailAddress;
        private string? _password;
        private string? _firstName;
        private string? _lastName;
        private string _confirmPassword;
        private string _errorMessage;
        private string _emailErrorMassage;

        public SignupViewModel(IEventAggregator events,
                               IUserEndpoint userEndpoint,
                               ILogin login)
        {
            _events = events;
            _userEndpoint = userEndpoint;
            _login = login;
        }

        public string ErrorMassage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                NotifyOfPropertyChange(() => IsErrorVisible);
                NotifyOfPropertyChange(() => ErrorMassage);
            }
        }

        public bool IsErrorVisible => IsErrorLenghtMoreThanZero(ErrorMassage);

        private bool IsErrorLenghtMoreThanZero(string ErrorMassage)
        {
            bool output = false;

            if (ErrorMassage?.Length > 0)
            {
                output = true;
            }

            return output;
        }

        public string EmailErrorMassage
        {
            get { return _emailErrorMassage; }
            set 
            { 
                _emailErrorMassage = value;
                NotifyOfPropertyChange(() => IsEmailErrorVisible);
                NotifyOfPropertyChange(() => EmailErrorMassage);
            }
        }

        public bool IsEmailErrorVisible => IsErrorLenghtMoreThanZero(EmailErrorMassage);

        private string _passwordErrorMassage;

        public string PasswordErrorMassage
        {
            get { return _passwordErrorMassage; }
            set 
            { 
                _passwordErrorMassage = value;
                NotifyOfPropertyChange(() => IsPasswordErrorVisible);
                NotifyOfPropertyChange(() => PasswordErrorMassage);
            }
        }

        public bool IsPasswordErrorVisible => IsErrorLenghtMoreThanZero(PasswordErrorMassage);


        public string? EmailAddress
        {
            get { return _emailAddress; }
            set
            {
                _emailAddress = value;

                if (string.IsNullOrWhiteSpace(_emailAddress) == false)
                {
                    if (IsValidEmail(_emailAddress) == false)
                        EmailErrorMassage = IncorrentEmail;
                    else if (IsValidEmail(_emailAddress) == true && EmailErrorMassage == IncorrentEmail)
                        EmailErrorMassage = string.Empty;
                }
                else
                    EmailErrorMassage = string.Empty;

                NotifyOfPropertyChange(() => EmailAddress);
                NotifyOfPropertyChange(() => CanSignUp);
            }
        }

        bool IsValidEmail(string email)
        {

            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        public string? Password
        {
            get { return _password; }
            set
            {
                _password = value;


                PasswordConfirmCheck();
                //if (IsPasswordCorrect == false && string.IsNullOrWhiteSpace(_password) == false)
                //{
                //    PasswordErrorMassage = IncorrectPassword;
                //}
                //else
                //{
                //    if (PasswordErrorMassage == IncorrectPassword)
                //        PasswordErrorMassage = string.Empty;

                //    PasswordConfirmCheck();
                //}

                NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(() => CanSignUp);
            }
        }

        public bool IsPasswordCorrect
        {
            get
            {
                Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
                return regex.Match(_password).Success;
            }
        }

        private void PasswordConfirmCheck()
        {
            if (string.IsNullOrWhiteSpace(_password) == false && IsPasswordCorrect == false) 
            {
                PasswordErrorMassage = IncorrectPassword;
            }
            else if (IsPasswordConfirmed == false
                                && string.IsNullOrWhiteSpace(_password) == false
                                && string.IsNullOrWhiteSpace(_confirmPassword) == false)
            {
                PasswordErrorMassage = PasswordNotConfirmedErrorMessage;
            }
            else if (IsPasswordConfirmed == true && PasswordErrorMassage == PasswordNotConfirmedErrorMessage)
                PasswordErrorMassage = string.Empty;
            else
                PasswordErrorMassage = string.Empty;

        }

        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set 
            { 
                _confirmPassword = value;

                PasswordConfirmCheck();

                NotifyOfPropertyChange(() => ConfirmPassword);
                NotifyOfPropertyChange(() => CanSignUp);
            }
        }

        public bool IsPasswordConfirmed 
        {
            get
            {
                var result = false;

                if(_password == _confirmPassword)
                    result = true;

                return  result;
            }
        }

        public string? FirstName
        {
            get { return _firstName; }
            set 
            { 
                _firstName = value;
                NotifyOfPropertyChange(() => FirstName);
                NotifyOfPropertyChange(() => CanSignUp);
            }
        }

        public string? LastName
        {
            get { return _lastName; }
            set 
            {
                _lastName = value;
                NotifyOfPropertyChange(() => LastName);
                NotifyOfPropertyChange(() => CanSignUp);
            }
        }

        public bool CanSignUp
        {
            get
            {
                bool output = false;
                if (_emailAddress?.Length > 0 
                    && _password?.Length > 0
                    && _confirmPassword?.Length > 0
                    && _firstName?.Length > 0 
                    && _lastName?.Length > 0
                    //&& string.IsNullOrWhiteSpace(_errorMessage) == true
                    && IsValidEmail(_emailAddress) == true
                    && IsPasswordCorrect == true)
                {
                    output = true;
                }

                return output;
            }
        }

        public async Task SignUp()
        {
            try
            {
                ErrorMassage = string.Empty;

                var userModel = new CreateUserModel() 
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    EmailAddress = EmailAddress,
                    Password = Password
                };

                await _userEndpoint.CreateUser(userModel);
                await _login.Run(userModel.EmailAddress!, userModel.Password!);
                await _events.PublishOnUIThreadAsync(new LogOnEvent());
            }
            catch (Exception ex)
            {
                ErrorMassage = ex.Message; 
            }
        }

        public async Task LogIn()
        {
            await _events.PublishOnUIThreadAsync(new OpenLogInViewEvent());
        }
    }
}
