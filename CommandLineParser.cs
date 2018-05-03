/*
 * Copyright (c) 2018 ffxiun0@gmail.com
 * https://opensource.org/licenses/MIT
 */
using System.Collections.Generic;
using System.Text;

namespace CLParser {
    class CommandLineParser {
        private State state;
        private StringBuilder token;
        private int backslash;
        private List<string> args;

        private enum State {
            Whitespace,
            Token,
            Quoted,
            QuotedEnd,
        }

        public CommandLineParser() {
            token = new StringBuilder(128);
        }

        public IEnumerable<string> Parse(string commandLine) {
            state = State.Whitespace;
            token.Clear();
            backslash = 0;
            args = new List<string>();

            Scan(commandLine);

            if (Finalize())
                return args;

            return null;
        }

        private void Scan(string commandLine) {
            foreach (var c in commandLine) {
                switch (state) {
                    case State.Whitespace:
                        ScanWhitespace(c);
                        break;
                    case State.Token:
                        ScanToken(c);
                        break;
                    case State.Quoted:
                        ScanQuoted(c);
                        break;
                    case State.QuotedEnd:
                        ScanQuotedEnd(c);
                        break;
                }
            }
        }

        private bool Finalize() {
            switch (state) {
                case State.Token:
                case State.QuotedEnd:
                    AppendBackslash();
                    AddToArgs();
                    break;
                case State.Whitespace:
                    break;
                case State.Quoted:
                    return false;
            }

            return true;
        }

        private void ScanWhitespace(char c) {
            switch (c) {
                case ' ':
                    break;
                case '"':
                    state = State.Quoted;
                    break;
                case '\\':
                    backslash++;
                    state = State.Token;
                    break;
                default:
                    AppendChar(c);
                    state = State.Token;
                    break;
            }
        }

        private void ScanToken(char c) {
            switch (c) {
                case ' ':
                    AppendBackslash();
                    AddToArgs();
                    state = State.Whitespace;
                    break;
                case '"':
                    if (AppendEscapedBackslash() == 0)
                        state = State.Quoted;
                    else
                        AppendChar(c);
                    break;
                case '\\':
                    backslash++;
                    break;
                default:
                    AppendBackslash();
                    AppendChar(c);
                    break;
            }
        }

        private void ScanQuoted(char c) {
            switch (c) {
                case '"':
                    if (AppendEscapedBackslash() == 0)
                        state = State.QuotedEnd;
                    else
                        AppendChar(c);
                    break;
                case '\\':
                    backslash++;
                    break;
                default:
                    AppendBackslash();
                    AppendChar(c);
                    break;
            }
        }

        private void ScanQuotedEnd(char c) {
            switch (c) {
                case ' ':
                    AddToArgs();
                    state = State.Whitespace;
                    break;
                case '"':
                    AppendChar(c);
                    state = State.Quoted;
                    break;
                case '\\':
                    backslash++;
                    state = State.Token;
                    break;
                default:
                    AppendChar(c);
                    state = State.Token;
                    break;
            }
        }

        private void AddToArgs() {
            args.Add(token.ToString());
            token.Clear();
        }

        private void AppendChar(char c) {
            token.Append(c);
        }

        private void AppendBackslash() {
            if (backslash == 0) return;

            token.Append('\\', backslash);
            backslash = 0;
        }

        private int AppendEscapedBackslash() {
            if (backslash == 0) return 0;

            token.Append('\\', backslash / 2);

            var remaining = backslash % 2;
            backslash = 0;

            return remaining;
        }
    }
}
