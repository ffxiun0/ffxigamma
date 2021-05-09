/*
 * Copyright (c) 2015 ffxiun0
 * https://opensource.org/licenses/MIT
 */
using System;
using System.ComponentModel;
using System.IO;
using System.IO.Pipes;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace ffxigamma {
    public delegate void RemoteControlEventHandler(object sender, string command);

    public class RemoteControl : Component {
        private bool disposed;
        private bool serverIsRunning;
        private CancellationTokenSource serverCancellation;
        private Task serverTask;

        public RemoteControl() {
            disposed = false;
            serverIsRunning = false;
        }

        public RemoteControl(string name) : this() {
            Name = name;
        }

        public RemoteControl(IContainer container) : this() {
            container.Add(this);
        }

        protected override void Dispose(bool disposing) {
            if (disposed) return;
            if (disposing)
                ServerStop();
            disposed = true;
            base.Dispose(disposing);
        }

        public string Name { get; set; } = null;
        public int SendTimeout { get; set; } = 1000;
        public event RemoteControlEventHandler CommandReceived;

        public bool SendCommand(string command) {
            try {
                SendCommand(Name, command, SendTimeout);
                return true;
            }
            catch (InvalidOperationException) { return false; }
            catch (TimeoutException) { return false; }
        }

        private static void SendCommand(string name, string command, int timeout) {
            using (var pipe = new NamedPipeClientStream(name)) {
                pipe.Connect(timeout);
                using (var sw = new StreamWriter(pipe))
                    sw.WriteLine(command);
            }
        }

        private static string ReceiveCommand(string name, PipeSecurity ps, CancellationToken token) {
            using (var pipe = CreateNamedPipeServerStream(name, ps)) {
                var connection = pipe.WaitForConnectionAsync();
                connection.Wait(token);

                var sr = new StreamReader(pipe);
                var read = sr.ReadLineAsync();
                read.Wait(token);
                return read.Result;
            }
        }

        private static Task<string> ReceiveCommandAsync(string name, PipeSecurity ps, CancellationToken token) {
            return Task.Run(() => ReceiveCommand(name, ps, token));
        }

        public void ServerStart() {
            if (serverIsRunning) return;
            serverIsRunning = true;

            serverCancellation = new CancellationTokenSource();
            serverTask = ServerMainAsync(serverCancellation.Token);
        }

        public async void ServerStop() {
            if (!serverIsRunning) return;
            serverIsRunning = false;

            try {
                serverCancellation.Cancel();
                await serverTask;
            }
            catch (OperationCanceledException) { }
        }

        private async Task ServerMainAsync(CancellationToken token) {
            var user = WindowsIdentity.GetCurrent().User;
            var ps = new PipeSecurity();
            var par = new PipeAccessRule(user, PipeAccessRights.ReadWrite, AccessControlType.Allow);
            ps.AddAccessRule(par);

            while (true) {
                var cmd = await ReceiveCommandAsync(Name, ps, token);
                if (cmd != null && CommandReceived != null)
                    CommandReceived(this, cmd);
            }
        }

        private static NamedPipeServerStream CreateNamedPipeServerStream(string name, PipeSecurity ps) {
#if NET5_0_OR_GREATER
            return NamedPipeServerStreamAcl.Create(name, PipeDirection.InOut, 1,
                PipeTransmissionMode.Byte, PipeOptions.Asynchronous, 0, 0, ps);
#else
            return new NamedPipeServerStream(name, PipeDirection.InOut, 1,
                PipeTransmissionMode.Byte, PipeOptions.Asynchronous, 0, 0, ps);
#endif
        }
    }
}
