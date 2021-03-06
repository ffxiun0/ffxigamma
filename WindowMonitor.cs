﻿/*
 * Copyright (c) 2015 ffxiun0
 * https://opensource.org/licenses/MIT
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace ffxigamma {
    delegate void WindowMonitorEventHandler(object sender, WindowMonitorEventArgs e);

    class WindowMonitor : Component {
        private bool disposed;
        private Timer timer;
        private Dictionary<WindowInfo, WindowInfo> map;

        public event WindowMonitorEventHandler WindowOpened;
        public event WindowMonitorEventHandler WindowClosed;
        public event WindowMonitorEventHandler WindowUpdate;

        public WindowMonitor() {
            disposed = false;

            timer = new Timer();
            timer.Interval = 100;
            timer.Tick += Timer_Tick;

            map = new Dictionary<WindowInfo, WindowInfo>();
            Expire = 1000;
            Filter = null;
        }

        public WindowMonitor(IContainer container) : this() {
            container.Add(this);
        }

        protected override void Dispose(bool disposing) {
            if (disposed) return;
            if (disposing)
                timer.Dispose();
            disposed = true;
            base.Dispose(disposing);
        }

        [DefaultValue(1000)]
        public int Expire { get; set; }

        [DefaultValue(null), Browsable(false)]
        public Func<Window, bool> Filter { get; set; }

        [DefaultValue(100)]
        public int Interval {
            get {
                return timer.Interval;
            }
            set {
                timer.Interval = value;
            }
        }

        public void Start() {
            if (timer.Enabled) return;

            timer.Start();
        }

        public void Stop() {
            if (!timer.Enabled) return;

            timer.Stop();
        }

        [DefaultValue(false)]
        public bool Enabled {
            get {
                return timer.Enabled;
            }
            set {
                if (value)
                    Start();
                else
                    Stop();
            }
        }

        public void Reset() {
            map.Clear();
        }

        private void Update() {
            var targets = FindTargetWindows();
            var opened = UpdateWindows(targets);
            var closed = CleanupClosedWindows();

            NotifyWindowOpened(opened);
            NotifyWindowClosed(closed);
            NotifyWindowUpdate(targets);
        }

        private IEnumerable<WindowInfo> UpdateWindows(IEnumerable<WindowInfo> wndInfos) {
            var opened = new List<WindowInfo>();
            foreach (var wndInfo in wndInfos) {
                if (!map.ContainsKey(wndInfo))
                    opened.Add(wndInfo);
                map[wndInfo] = wndInfo;
            }
            return opened;
        }

        private IEnumerable<WindowInfo> FindTargetWindows() {
            var filter = Filter != null ? Filter : wnd => true;

            var result = from wnd in Window.EnumWindows()
                         where filter(wnd)
                         select new WindowInfo(wnd);
            return result.ToArray();
        }

        public IEnumerable<WindowInfo> CleanupClosedWindows() {
            var now = DateTime.Now;
            var closed = from wndInfo in map.Values
                         where (now - wndInfo.Time).TotalMilliseconds > Expire
                         select wndInfo;
            closed = closed.ToArray();

            foreach (var wndInfo in closed)
                map.Remove(wndInfo);

            return closed;
        }

        private void NotifyWindowOpened(IEnumerable<WindowInfo> opened) {
            if (WindowOpened == null) return;

            var args = new WindowMonitorEventArgs(opened);
            WindowOpened(this, args);
        }

        private void NotifyWindowClosed(IEnumerable<WindowInfo> closed) {
            if (WindowClosed == null) return;

            var args = new WindowMonitorEventArgs(closed);
            WindowClosed(this, args);
        }

        private void NotifyWindowUpdate(IEnumerable<WindowInfo> updates) {
            if (WindowUpdate == null) return;

            var args = new WindowMonitorEventArgs(updates);
            WindowUpdate(this, args);
        }

        private void Timer_Tick(object sender, EventArgs e) {
            Update();
        }
    }
}
