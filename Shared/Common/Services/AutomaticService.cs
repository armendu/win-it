using System.Threading;
using System.Threading.Tasks;

namespace Common.Services
{
    public abstract class AutomaticService
    {
        public abstract Task StartAsync(CancellationToken cancellationToken);

        public abstract Task StopAsync(CancellationToken cancellationToken);
    }
}