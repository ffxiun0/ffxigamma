using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ffxigamma {
    public class CommandLine {
        public string Exe { get; set; } = "";
        public string[] Args { get; set; } = new string[0];

        public override string ToString() {
            return ToString(Exe, Args);
        }

        public static string ToString(string s) {
            var m = Regex.Match(s, @"[ \t\r\n\f""]");
            if (m.Success)
                return "\"" + EscapeQuote(s) + "\"";
            else
                return s;
        }

        public static string ToString(IEnumerable<string> args) {
            var quotedArgs = from arg in args select ToString(arg);
            return string.Join(" ", quotedArgs);
        }

        public static string ToString(string exe, IEnumerable<string> args) {
            var commandLine = Concat(exe, args);
            return ToString(commandLine);
        }

        private static string EscapeQuote(string s) {
            return s.Replace("\"", "\"\"");
        }

        private static IEnumerable<T> Concat<T>(T element, IEnumerable<T> list) {
            yield return element;
            foreach (var item in list)
                yield return item;
        }

        public static CommandLine Parse(string s) {
            var cls = new CommandLineScanner();
            var args = cls.Scan(s);

            if (args == null)
                return null;

            if (args.Count == 0)
                return new CommandLine();

            return new CommandLine() {
                Exe = args[0],
                Args = args.GetRange(1, args.Count - 1).ToArray(),
            };
        }

        private class CommandLineScanner {
            private State state;
            private string token;
            private List<string> list;

            private enum State {
                Initial,
                Token,
                WhiteSpace,
                Quote,
                QuoteEnd,
            }

            public List<string> Scan(string s) {
                state = State.Initial;
                token = "";
                list = new List<string>();

                foreach (var c in s) {
                    switch (state) {
                        case State.Initial:
                            ScanInitial(c);
                            break;
                        case State.Token:
                            ScanToken(c);
                            break;
                        case State.WhiteSpace:
                            ScanWhiteSpace(c);
                            break;
                        case State.Quote:
                            ScanQuote(c);
                            break;
                        case State.QuoteEnd:
                            ScanQuoteEnd(c);
                            break;
                    }
                }

                switch (state) {
                    case State.Token:
                    case State.QuoteEnd:
                        list.Add(token);
                        break;
                    case State.Initial:
                    case State.WhiteSpace:
                        break;
                    case State.Quote:
                        return null;
                }

                return list;
            }

            private void ScanInitial(char c) {
                switch (c) {
                    case ' ':
                    case '\t':
                    case '\r':
                    case '\n':
                    case '\f':
                        state = State.WhiteSpace;
                        break;
                    case '"':
                        state = State.Quote;
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
                    case '\t':
                    case '\r':
                    case '\n':
                    case '\f':
                        list.Add(token);
                        token = "";
                        state = State.WhiteSpace;
                        break;
                    case '"':
                        state = State.Quote;
                        break;
                    default:
                        token += c;
                        break;
                }
            }

            private void ScanWhiteSpace(char c) {
                switch (c) {
                    case ' ':
                    case '\t':
                    case '\r':
                    case '\n':
                    case '\f':
                        break;
                    case '"':
                        state = State.Quote;
                        break;
                    default:
                        token += c;
                        state = State.Token;
                        break;
                }
            }

            private void ScanQuote(char c) {
                switch (c) {
                    case '"':
                        state = State.QuoteEnd;
                        break;
                    default:
                        token += c;
                        break;
                }
            }

            private void ScanQuoteEnd(char c) {
                switch (c) {
                    case '"':
                        token += c;
                        state = State.Quote;
                        break;
                    case ' ':
                    case '\t':
                    case '\r':
                    case '\n':
                    case '\f':
                        list.Add(token);
                        token = "";
                        state = State.WhiteSpace;
                        break;
                    default:
                        token += c;
                        state = State.Token;
                        break;
                }
            }
        }
    }
}
