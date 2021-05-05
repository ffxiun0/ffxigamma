/*
 * Copyright (c) 2015 ffxiun0
 * https://opensource.org/licenses/MIT
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Pipes;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Threading;
using System.Threading.Tasks;

namespace ffxigamma {
    public delegate void RemoteControlEventHandler(object sender, string command);

    public class RemoteControl : Component {
        private bool disposed;
        private List<string> commandBuffer;
        private CancellationTokenSource serverCancel;
        private Task serverTask;
        private System.Windows.Forms.Timer timer;

        public RemoteControl() {
            disposed = false;
            commandBuffer = new List<string>();
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
        public int EventInterval { get; set; } = 100;
        public event RemoteControlEventHandler CommandReceived;

        public bool SendCommand(string command) {
            try {
                using (var pipe = new NamedPipeClientStream(Name)) {
                    pipe.Connect(SendTimeout);
                    using (var sw = new StreamWriter(pipe))
                        sw.WriteLine(command);
                    return true;
                }
            }
            catch (InvalidOperationException) { return false; }
            catch (TimeoutException) { return false; }
        }

        public void ServerStart() {
            if (serverTask != null) return;

            serverCancel = new CancellationTokenSource();
            serverTask = Task.Run(ServerMain);

            timer = new System.Windows.Forms.Timer();
            timer.Interval = EventInterval;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        public void ServerStop() {
            if (serverTask == null) return;

            timer.Stop();
            timer.Dispose();

            serverCancel.Cancel();
            serverTask.Wait();
            serverTask = null;
        }

        private void ServerMain() {
            var ps = new PipeSecurity();
            var par = new PipeAccessRule("Everyone", PipeAccessRights.ReadWrite, AccessControlType.Allow);
            ps.AddAccessRule(par);

            try {
                while (true) {
                    using (var pipe = CreateNamedPipeServerStream(Name, ps)) {
                        var connection = pipe.WaitForConnectionAsync();
                        connection.Wait(serverCancel.Token);

                        var sr = new StreamReader(pipe);
                        var read = sr.ReadLineAsync();
                        read.Wait(serverCancel.Token);

                        var cmd = read.Result;
                        if (cmd != null)
                            PushCommand(cmd);
                    }
                }
            }
            catch (OperationCanceledException) { }
        }

        private void PushCommand(string command) {
            lock (commandBuffer) {
                commandBuffer.Add(command);
            }
        }

        private IEnumerable<string> FlushCommands() {
            lock (commandBuffer) {
                if (commandBuffer.Count == 0) return null; // Required to improve standby performance.

                var result = commandBuffer.ToArray();
                commandBuffer.Clear();
                return result;
            }
        }

        private void Timer_Tick(object sender, EventArgs e) {
            var cmds = FlushCommands();

            if (cmds != null && CommandReceived != null) {
                timer.Stop();
                foreach (var cmd in cmds)
                    CommandReceived(this, cmd);
                timer.Start();
            }
        }

        private static NamedPipeServerStream CreateNamedPipeServerStream(string name, PipeSecurity ps) {
            var sdbin = ps.GetSecurityDescriptorBinaryForm();
            var sdptr = Marshal.AllocHGlobal(sdbin.Length);
            try {
                Marshal.Copy(sdbin, 0, sdptr, sdbin.Length);

                var sa = new NativeMethods.SECURITY_ATTRIBUTES();
                sa.nLength = Marshal.SizeOf(sa);
                sa.lpSecurityDescriptor = sdptr;
                sa.bInheritHandle = false;

                var path = @"\\.\pipe\" + name;

                var handle = NativeMethods.CreateNamedPipe(path,
                    NativeMethods.PIPE_ACCESS_DUPLEX,
                    NativeMethods.PIPE_TYPE_BYTE | NativeMethods.PIPE_WAIT,
                    1, 0, 0, 0, ref sa);
                if (handle.IsInvalid)
                    throw new Win32Exception(NativeMethods.GetLastError());

                return new NamedPipeServerStream(PipeDirection.InOut, false, false, handle);
            }
            finally {
                Marshal.FreeHGlobal(sdptr);
            }
        }
    }
}
