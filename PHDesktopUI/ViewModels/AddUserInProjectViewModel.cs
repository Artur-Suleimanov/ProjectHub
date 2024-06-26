using Caliburn.Micro;
using PHDesktopUI.EventModels;
using PHDesktopUI.Librery.Api;
using PHDesktopUI.Librery.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PHDesktopUI.ViewModels
{
    public class AddUserInProjectViewModel : Screen
    {
        public ProjectModel Project { get; set; }

        public AddUserInProjectViewModel(IUserEndpoint userEndpoint,
                                         IProjectEndpoint projectEndpoint,
                                         IEventAggregator events)
        {
            _userEndpoint = userEndpoint;
            _projectEndpoint = projectEndpoint;
            _events = events;
        }

        private string? _email;
        private readonly IUserEndpoint _userEndpoint;
        private readonly IProjectEndpoint _projectEndpoint;
        private readonly IEventAggregator _events;

        public string? Email
        {
            get { return _email; }
            set 
            {
                _email = value;
                NotifyOfPropertyChange(() => Email);
            }
        }

        public async Task Email_TextChanged()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Email))
                {
                    ErrorMessage = string.Empty;
                    Users = new BindingList<UserModel>();
                    return; 
                }

                // Обработка изменения текста
                var user = await _userEndpoint.GetUserByEmail(_email);

                // Проверка является ли пользльзователь участником этого проекта:
                if(await _userEndpoint.CheckUserProjectMembership(user.Id, Project.Id) == true)
                {
                    ErrorMessage = "Этот пользователь уже является участником данного проекта";
                    Users = new BindingList<UserModel>();
                    return;
                }

                ErrorMessage = string.Empty;

                Users.Add(user);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                Users = new BindingList<UserModel>();
            }
            
        }

        public bool IsErrorVisible
        {
            get
            {
                bool output = false;

                if (ErrorMessage?.Length > 0)
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

        private BindingList<UserModel> _users = new BindingList<UserModel>();

        public BindingList<UserModel> Users
        {
            get { return _users; }
            set 
            { 
                _users = value;
                NotifyOfPropertyChange(() => Users);
            }
        }

        private UserModel? _selectedUser;

        public UserModel? SelectedUser
        {
            get { return _selectedUser; }
            set 
            { 
                _selectedUser = value;
                NotifyOfPropertyChange(() => SelectedUser);
                NotifyOfPropertyChange(() => CanAddUser);
            }
        }

        public bool CanAddUser
        {
            get
            {
                bool output = false;

                if (SelectedUser != null)
                    output = true;

                return output;
            }
        }

        public async Task AddUser()
        {
            try
            {
                await _projectEndpoint.AddUserInProject(Project.Id, SelectedUser.Id, 1);
                await OpenProjectViewModel();
            }
            catch (Exception ex) 
            {
                ErrorMessage = ex.Message;
            }
            
        }

        public async Task Back()
        {
            await OpenProjectViewModel();
        }

        private async Task OpenProjectViewModel()
        {
            var ope = new OpenProjectEvent();
            ope.ProjectModel = Project;
            await _events.PublishOnUIThreadAsync(ope);
        }
    }
}
