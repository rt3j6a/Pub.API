using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pub.Core.Interface {
    public interface IAuthenticationManager {
        Task<(bool success,string message)> LoginAsync(string loginName, string password);

        Task<(bool success,string message)> AddAccountAsync(string emailAddress, string loginName, string password);

    }
}
