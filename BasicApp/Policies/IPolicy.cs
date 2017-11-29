using System;
using Polly;

namespace BasicApp.Policies
{
    public interface IPolicy
    {
        Policy GetPolicy();
    }
}
