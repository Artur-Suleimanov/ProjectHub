using PHDesktopUI.Models;
using System.Net.Http;

namespace PHDesktopUI.Helpers
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> Authenticate(string username, string password);
    }
}