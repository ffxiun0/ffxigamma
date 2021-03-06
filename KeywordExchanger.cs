﻿/*
 * Copyright (c) 2015 ffxiun0
 * https://opensource.org/licenses/MIT
 */
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ffxigamma {
    class KeywordExchanger {
        private Dictionary<string, string> keywordMap;

        public KeywordExchanger(DateTime time) {
            keywordMap = MakeKeywordMap(time);
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

        private static Dictionary<string, string> MakeKeywordMap(DateTime time) {
            var map = new Dictionary<string, string>();

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
