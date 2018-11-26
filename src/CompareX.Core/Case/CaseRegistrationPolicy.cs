using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CompareX.Authorization.Users;

namespace CompareX.Case
{
    public class CaseRegistrationPolicy : ICaseRegistrationPolicy
    {

        public Task CheckRegistrationAttemptAsync(Case checkCase, User user)
        {
            throw new NotImplementedException();
        }
    }
}
