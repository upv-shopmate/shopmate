using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace PopulateDb
{
    internal class CommandLineProcess : Process
    {
        public bool IsRunning { get; private set; } = false;

        public CommandLineProcess(string path, string arguments = "") : base()
        {
            StartInfo = new ProcessStartInfo(path, arguments)
            {
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            EnableRaisingEvents = true;

            Exited += (_, _args) => { IsRunning = false; };
        }

        public new bool Start()
        {
            var created = base.Start();
            IsRunning = true;

            BeginOutputReadLine();
            BeginErrorReadLine();

            return created;
        }

        public Task WaitForExitAsync(CancellationToken cancel = default)
        {
            if (HasExited)
            {
                return Task.CompletedTask;
            }

            var completion = new TaskCompletionSource<object>();
            Exited += (_, _args) => completion.TrySetResult(default);

            if (cancel != default)
            {
                cancel.Register(() => completion.SetCanceled());
            }

            return HasExited ? Task.CompletedTask : completion.Task;
        }
    }
}
