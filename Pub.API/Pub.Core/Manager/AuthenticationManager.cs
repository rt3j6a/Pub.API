using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pub.Core.Common;
using Pub.Core.Interface;
using Pub.Core.Model;

namespace Pub.Core.Manager {
    public class AuthenticationManager : CoreManager, IAuthenticationManager {
        public AuthenticationManager(IConfiguration configuration, IServiceScopeFactory scopeFactory) : base(configuration, scopeFactory) {
        }

        public async Task<(bool success, string message)> AddAccountAsync(string emailAddress, string loginName, string password) {
            var accountExists = await provider.Accounts.AnyAsync(x => x.EmailAddress.Equals(emailAddress) || x.AccountName.Equals(loginName));

            if (accountExists) {
                return (false, Messages.Authentication.AccountAlreadyExistsWithGivenCredentials);
            }

            Account account = new Account { 
                AccountName=loginName,
                AccountPasswd=password,
                EmailAddress=emailAddress
            };
             await provider.Accounts.AddAsync(account);
             await provider.SaveChangesAsync();

             return (true, Messages.Authentication.AccountCreatedSuccessfully);
        }

        public async Task<(bool success, string message)> LoginAsync(string loginName, string password) {
            var exists=await provider.Accounts.AnyAsync(x=>x.AccountName.Equals(loginName) && x.AccountPasswd.Equals(password));

            if (exists) {
                return (true, Messages.Authentication.LoggedInSuccessfully);
            }

            return (false, Messages.Authentication.AccountDoesntExistsWithGivenCredentials);
        }
    }
}
