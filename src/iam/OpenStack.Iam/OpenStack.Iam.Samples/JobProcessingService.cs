using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using OpenStack.Iam.Authentication;

namespace OpenStack.Iam.Samples
{
    public class JobProcessingService : IHostedService, IDisposable
    {
        private CancellationTokenSource m_CancellationTokenSource;
        private Task m_CurrentTask;

        private readonly IAuthenticationAndTokenManagementClient m_Client;

        public JobProcessingService(IAuthenticationAndTokenManagementClient client)
        {
            m_Client = client;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            m_CancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            // In later version of this task we will get a list of
            //skills which we will be make subsequent calls
            //however for now I just provided an example
            m_CurrentTask = m_Client.GetTokenPasswordAuthenticationUnscopedAuthorizationAsync();

            return m_CurrentTask.IsCompleted ? m_CurrentTask : Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {

            if (m_CurrentTask == null)
            {
                return;
            }

            try
            {
                m_CancellationTokenSource.Cancel();
            }
            finally
            {
                await Task.WhenAny(m_CurrentTask, Task.Delay(Timeout.Infinite, cancellationToken));
            }
        }

        public void Dispose()
        {
            m_CancellationTokenSource.Cancel();
        }
    }
}
