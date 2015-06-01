﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ffxigamma {
    public partial class InputHotKey : Form {
        private GlobalKeyReader keyReader;
        private bool fix;
        private HotKey editingKey;

        public HotKey HotKey { get; set; }

        public InputHotKey() {
            InitializeComponent();

            keyReader = new GlobalKeyReader();
            keyReader.GlobalKeyDown += keyReader_GlobalKeyDown;
            keyReader.GlobalKeyUp += keyReader_GlobalKeyUp;
            keyReader.Start();

            fix = false;
            editingKey = new HotKey();
            HotKey = new HotKey();
        }

        private void UpdateKey(GlobalKeyEventArgs e) {
            if (fix) return;

            editingKey.Control = e.State.Contains(Keys.ControlKey);
            editingKey.Shift = e.State.Contains(Keys.ShiftKey);
            editingKey.Alt = e.State.Contains(Keys.Menu);
            editingKey.Key = GetFirstKey(e.Trigger);

            if (editingKey.Key != Keys.None) {
                fix = true;
                uiKeys.BackColor = SystemColors.Control;
            }

            uiKeys.Text = editingKey.ToString();
        }

        private Keys GetFirstKey(GlobalKeys keys) {
            for (int i = 0; i < 256; i++) {
                var key = (Keys)i;
                if (keys.Contains(key) && !IsIgnoreKey(key))
                    return key;
            }
            return Keys.None;
        }

        private static bool IsIgnoreKey(Keys key) {
            return ignoreKeys.Contains(key);
        }

        private static HashSet<Keys> ignoreKeys = new HashSet<Keys> {
            Keys.ControlKey,
            Keys.LControlKey,
            Keys.RControlKey,
            Keys.Control,
            Keys.ShiftKey,
            Keys.LShiftKey,
            Keys.RShiftKey,
            Keys.Shift,
            Keys.LMenu,
            Keys.RMenu,
            Keys.Menu,
            Keys.Alt,
            Keys.LButton,
            Keys.MButton,
            Keys.RButton,
            Keys.LWin,
            Keys.RWin,
            Keys.Return,
        };

        private void Reset() {
            fix = false;
            uiKeys.BackColor = SystemColors.Window;
        }

        private void InputKey_Load(object sender, EventArgs e) {
            Reset();
        }

        private void keyReader_GlobalKeyDown(object sender, GlobalKeyEventArgs e) {
            UpdateKey(e);
        }

        private void keyReader_GlobalKeyUp(object sender, GlobalKeyEventArgs e) {
            UpdateKey(e);
        }

        private void uiReset_Click(object sender, EventArgs e) {
            Reset();
        }

        private void uiOk_Click(object sender, EventArgs e) {
            if (!fix) return;

            HotKey = editingKey;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void uiCancel_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
