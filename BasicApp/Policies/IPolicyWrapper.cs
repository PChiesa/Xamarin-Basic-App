using System;
using Polly;
using Polly.Wrap;

namespace BasicApp.Policies
{
    public interface IPolicyWrapper<T> where T : class
    {
        PolicyWrap<T> GetPolicies();
    }
}
