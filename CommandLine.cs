/*
 * Copyright (c) 2018 ffxiun0@gmail.com
 * https://opensource.org/licenses/MIT
 */
using System.Collections.Generic;
using System.Linq;

namespace ffxigamma {
    public class CommandLine {
        public string Exe { get; set; }
        public string[] Args { get; set; }

        private CommandLine() {
            Exe = "";
            Args = new string[0];
        }

        private CommandLine(string exe, IEnumerable<string> args) {
            Exe = exe;
            Args = args.ToArray();
        }

        private CommandLine(IEnumerable<string> args) {
            var list = args.ToList();

            Exe = list[0];
            Args = list.GetRange(1, list.Count - 1).ToArray();
        }

        public static CommandLine Parse(string s) {
            var parser = new CommandLineParser();

            var args = parser.Parse(s);
            if (args == null) return null;
            if (args.Count() == 0) return new CommandLine();

            return new CommandLine(args);
        }

        public override string ToString() {
            return ToString(Exe, Args);
        }

        public static string ToString(string s) {
            return CommandLineEncoder.Encode(s);
        }

        public static string ToString(IEnumerable<string> args) {
            return CommandLineEncoder.Encode(args);
        }

        public static string ToString(string exe, IEnumerable<string> args) {
            var commandLine = Concat(exe, args);
            return ToString(commandLine);
        }

        private static IEnumerable<T> Concat<T>(T element, IEnumerable<T> list) {
            yield return element;

            foreach (var item in list)
                yield return item;
        }
    }
}
