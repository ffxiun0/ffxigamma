using System;

namespace ffxigamma {
    public class RemoteControl : MarshalByRefObject {
        private App app;

        public RemoteControl(App app) {
            this.app = app;
        }

        delegate void InvokeDelegate();

        public void StartFFXI() {
            app.BeginInvoke(new InvokeDelegate(app.StartFFXI));
        }

        public void StartFFXIinAdmin() {
            app.BeginInvoke(new InvokeDelegate(app.StartFFXIinAdmin));
        }
    }
}
