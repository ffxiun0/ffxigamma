using System;

namespace ffxigamma {
    public class RemoteControl : MarshalByRefObject {
        private App app;

        public RemoteControl(App app) {
            this.app = app;
        }

        delegate void InvokeDelegate();

        public void StartProgram() {
            app.BeginInvoke(new InvokeDelegate(app.AutoStartProgram));
        }
    }
}
