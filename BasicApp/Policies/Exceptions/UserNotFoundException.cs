using System;
namespace BasicApp.Policies.Exceptions
{
    public class UserNotFoundException : InvalidOperationException
    {
        public UserNotFoundException()
        {
        }
    }
}
