using PHDataManager.Library.DataAccess.Interfaces;
using PHDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHDataManager.Library.DataAccess
{
    public class UserData : IUserData
    {
        private readonly ISqlDataAccess _sql;

        public UserData(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        public List<UserModel> GetUserById(string id)
        {
            var output = _sql.LoadData<UserModel, dynamic>("spUserLookup", new { id }, "PHData");

            return output;
        }

        public List<UserModel> GetUserByEmail(string email)
        {
            var output = _sql.LoadData<UserModel, dynamic>("spGetUserByEmail", new { email }, "PHData");

            return output;
        }

        public List<int> CheckUserProjectMembership(string userId, int projectId)
        {
            var output = _sql.LoadData<int, dynamic>("spCheckUserProjectMembership", new { userId, projectId }, "PHData");

            return output;
        }
    }
}
