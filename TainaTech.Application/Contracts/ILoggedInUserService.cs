using System;
using System.Collections.Generic;
using System.Text;

namespace TainaTech.Application.Contracts
{
    public interface ILoggedInUserService
    {
        public string UserId { get; }
    }
}
