/*
 * Copyright (c) 2018 ffxiun0@gmail.com
 * https://opensource.org/licenses/MIT
 */
using System.Collections.Generic;

namespace CLParser {
    class CommandLineParser {
        private State state;
        private string token;
        private int backslash;
        private List<string> args;

        private enum State {
            Initial,
            Token,
            Whitespace,
            Quoted,
            QuotedEnd,
            Backslash,
            QuotedBackslash,
        }

        public IEnumerable<string> Parse(string commandLine) {
            state = State.Initial;
            token = "";
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
                    case State.Initial:
                        ScanInitial(c);
                        break;
                    case State.Token:
                        ScanToken(c);
                        break;
                    case State.Whitespace:
                        ScanWhitespace(c);
                        break;
                    case State.Quoted:
                        ScanQuoted(c);
                        break;
                    case State.QuotedEnd:
                        ScanQuotedEnd(c);
                        break;
                    case State.Backslash:
                        ScanBackslash(c);
                        break;
                    case State.QuotedBackslash:
                        ScanQuotedBackslash(c);
                        break;
                }
            }
        }

        private bool Finalize() {
            switch (state) {
                case State.Token:
                case State.QuotedEnd:
                    args.Add(token);
                    break;
                case State.Initial:
                case State.Whitespace:
                    break;
                case State.Quoted:
                case State.QuotedBackslash:
                    return false;
                case State.Backslash:
                    token += new string('\\', backslash);
                    args.Add(token);
                    break;
            }

            return true;
        }

        private void ScanInitial(char c) {
            switch (c) {
                case ' ':
                    state = State.Whitespace;
                    break;
                case '"':
                    state = State.Quoted;
                    break;
                case '\\':
                    backslash++;
                    state = State.Backslash;
                    break;
                default:
                    token += c;
                    state = State.Token;
                    break;
            }
        }

        private void ScanToken(char c) {
            switch (c) {
                case ' ':
                    args.Add(token);
                    token = "";
                    state = State.Whitespace;
                    break;
                case '"':
                    state = State.Quoted;
                    break;
                case '\\':
                    backslash++;
                    state = State.Backslash;
                    break;
                default:
                    token += c;
                    break;
            }
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
                    state = State.Backslash;
                    break;
                default:
                    token += c;
                    state = State.Token;
                    break;
            }
        }

        private void ScanQuoted(char c) {
            switch (c) {
                case '"':
                    state = State.QuotedEnd;
                    break;
                case '\\':
                    backslash++;
                    state = State.QuotedBackslash;
                    break;
                default:
                    token += c;
                    break;
            }
        }

        private void ScanQuotedEnd(char c) {
            switch (c) {
                case ' ':
                    args.Add(token);
                    token = "";
                    state = State.Whitespace;
                    break;
                case '"':
                    token += c;
                    state = State.Quoted;
                    break;
                case '\\':
                    backslash++;
                    state = State.Backslash;
                    break;
                default:
                    token += c;
                    state = State.Token;
                    break;
            }
        }

        private void ScanBackslash(char c) {
            switch (c) {
                case ' ':
                    token += new string('\\', backslash);
                    args.Add(token);
                    token = "";
                    backslash = 0;
                    state = State.Whitespace;
                    break;
                case '"':
                    token += new string('\\', backslash / 2);
                    if ((backslash % 2) == 0) {
                        state = State.Quoted;
                    } else {
                        token += '"';
                        state = State.Token;
                    }
                    backslash = 0;
                    break;
                case '\\':
                    backslash++;
                    break;
                default:
                    token += new string('\\', backslash);
                    token += c;
                    backslash = 0;
                    state = State.Token;
                    break;
            }
        }

        private void ScanQuotedBackslash(char c) {
            switch (c) {
                case '"':
                    token += new string('\\', backslash / 2);
                    if ((backslash % 2) == 0) {
                        state = State.QuotedEnd;
                    } else {
                        token += '"';
                        state = State.Quoted;
                    }
                    backslash = 0;
                    break;
                case '\\':
                    backslash++;
                    break;
                default:
                    token += new string('\\', backslash);
                    token += c;
                    backslash = 0;
                    state = State.Quoted;
                    break;
            }
        }
    }
}
