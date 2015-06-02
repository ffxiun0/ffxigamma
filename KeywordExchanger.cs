using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ffxigamma {
    class KeywordExchanger {
        private Dictionary<string, string> keywordMap;

        public KeywordExchanger() {
            keywordMap = MakeKeywordMap();
        }

        public string Replace(string s) {
            return Regex.Replace(s, @"@\w+@", (match) => {
                string result;
                if (keywordMap.TryGetValue(match.Value, out result))
                    return result;
                else
                    return match.Value;
            });
        }

        private static Dictionary<string, string> MakeKeywordMap() {
            var map = new Dictionary<string, string>();

            var time = DateTime.Now;
            foreach (var format in dateTimeFormats) {
                var key = "@" + format + "@";
                var value = time.ToString(format + " ").Trim();
                map[key] = value;
            }

            return map;
        }

        private static string[] dateTimeFormats = new string[]{
            "y", "yy", "yyy", "yyyy",
            "M", "MM", "MMM", "MMMM",
            "d", "dd", "ddd", "dddd",
            "h", "hh",
            "H", "HH",
            "m", "mm",
            "s", "ss",
            "f", "ff", "fff", "ffff", "fffff", "ffffff",
            "F", "FF", "FFF", "FFFF", "FFFFF", "FFFFFF",
            "t", "tt",
            "z", "zz", "zzz",
        };
    }
}
