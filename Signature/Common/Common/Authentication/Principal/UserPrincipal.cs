using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Common.Authentication.Principal.Interface;

namespace Common.Authentication.Principal
{
    public class UserPrincipal : IUserPrincipal
    {
        private string[] roles;
        public IIdentity Identity { get; private set; }

        protected string[] Roles
        {
            get { return roles; }
        }

        public UserPrincipal(string id)
        {
            this.Identity = new GenericIdentity(id);
        }

        public UserPrincipal(string id, string[] roles)
        {
            this.Identity = new GenericIdentity(id);
            this.roles = roles;
        }
        public bool IsInRole(string role)
        {
            foreach (var r in roles)
                if (string.Compare(r, role, true) == 0)
                    return true;
            return false;
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
