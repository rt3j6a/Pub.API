using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pub.Core.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pub.Core.Manager {
    public abstract class CoreManager : IDisposable {
        protected readonly IConfiguration configuration;

        private IServiceScope scope;

        protected readonly DBProvider provider;

        public CoreManager(IConfiguration configuration, IServiceScopeFactory scopeFactory) {
            this.configuration = configuration;
            scope = scopeFactory.CreateScope();
            provider = scope.ServiceProvider.GetRequiredService<DBProvider>();
        }

        public virtual void Dispose() {
            provider.Dispose();
        }
    }
}
