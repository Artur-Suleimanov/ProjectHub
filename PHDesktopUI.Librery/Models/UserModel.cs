using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHDesktopUI.Librery.Models
{
    public class UserModel
    {
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailAddress { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? FullName { get => $"{FirstName} {LastName}"; }
    }
}
