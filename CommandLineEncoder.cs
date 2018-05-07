/*
 * Copyright (c) 2018 ffxiun0
 * https://opensource.org/licenses/MIT
 */
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CLParser {
    class CommandLineEncoder {
        private CommandLineEncoder() { }

        public static string Encode(string s) {
            if (s.Length == 0) return "\"\"";

            var m = Regex.Match(s, @"[ ""]");
            if (!m.Success) return s;

            var qs = "\"" + EscapeQuote(s) + "\"";
            return EscapeLastQuote(qs);
        }

        public static string Encode(IEnumerable<string> args) {
            var quotedArgs = from arg in args select Encode(arg);

            return string.Join(" ", quotedArgs);
        }

        private static string EscapeQuote(string s) {
            return Regex.Replace(s, @"(\\+)""|""", (m) => {
                var g = m.Groups[1];
                if (!g.Success) return "\\\"";

                var bs = g.Value;
                return bs + bs + "\\\"";
            });
        }

        private static string EscapeLastQuote(string s) {
            return Regex.Replace(s, @"(\\+)""$", (m) => {
                var bs = m.Groups[1].Value;
                return bs + bs + "\"";
            });
        }
    }
}
