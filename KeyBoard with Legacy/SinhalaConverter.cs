using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace KeyBoard
{
    public class SinhalaConverter
    {
        private readonly List<string> _gn_c = new List<string>();
        private readonly List<string> _gn_cUni = new List<string>();
        private readonly List<string> _gn_v = new List<string>();
        private readonly List<string> _gn_vUni = new List<string>();
        private readonly List<string> _gn_vmUni = new List<string>();
        private readonly List<Regex> _specialgn_c = new List<Regex>();
        private readonly List<string> _specialgn_cUni = new List<string>();
        private readonly List<string> _gn_sc = new List<string>();
        private readonly List<string> _gn_scUni = new List<string>();

        public SinhalaConverter()
        {
            InitializeMappings();
        }

        private void InitializeMappings()
        {
            // Vowel mappings
            AddVowel("oo", "ඌ", "ූ");
            AddVowel("o\\)", "ඕ", "ෝ");
            AddVowel("oe", "ඕ", "ෝ");
            AddVowel("aa", "ආ", "ා");
            AddVowel("a\\)", "ආ", "ා");
            AddVowel("Aa", "ඈ", "ෑ");
            AddVowel("A\\)", "ඈ", "ෑ");
            AddVowel("ae", "ඈ", "ෑ");
            AddVowel("ii", "ඊ", "ී");
            AddVowel("i\\)", "ඊ", "ී");
            AddVowel("ie", "ඊ", "ී");
            AddVowel("ee", "ඊ", "ී");
            AddVowel("ea", "ඒ", "ේ");
            AddVowel("e\\)", "ඒ", "ේ");
            AddVowel("ei", "ඒ", "ේ");
            AddVowel("uu", "ඌ", "ූ");
            AddVowel("u\\)", "ඌ", "ූ");
            AddVowel("au", "ඖ", "ෞ");
            AddVowel("/a", "ඇ", "ැ");
            AddVowel("a", "අ", "");
            AddVowel("A", "ඇ", "ැ");
            AddVowel("i", "ඉ", "ි");
            AddVowel("e", "එ", "ෙ");
            AddVowel("u", "උ", "ු");
            AddVowel("o", "ඔ", "ො");
            AddVowel("I", "ඓ", "ෛ");

            // Special character mappings
            _specialgn_c.Add(new Regex(@"x", RegexOptions.Compiled));
            _specialgn_cUni.Add("ං");

            _specialgn_c.Add(new Regex(@"\\h", RegexOptions.Compiled));
            _specialgn_cUni.Add("ඃ");

            _specialgn_c.Add(new Regex(@"\\N", RegexOptions.Compiled));
            _specialgn_cUni.Add("ඞ");

            _specialgn_c.Add(new Regex(@"\\R", RegexOptions.Compiled));
            _specialgn_cUni.Add("ඍ");

            _specialgn_c.Add(new Regex(@"R", RegexOptions.Compiled));
            _specialgn_cUni.Add("ර්‍");

            _specialgn_c.Add(new Regex(@"\\r", RegexOptions.Compiled));
            _specialgn_cUni.Add("ර්‍");

            // Consonants
            AddConsonant("nnd", "ඬ");
            AddConsonant("nndh", "ඳ");
            AddConsonant("nng", "ඟ");
            AddConsonant("Th", "ථ");
            AddConsonant("Dh", "ධ");
            AddConsonant("gh", "ඝ");
            AddConsonant("Ch", "ඡ");
            AddConsonant("ph", "ඵ");
            AddConsonant("bh", "භ");
            AddConsonant("sh", "ශ");
            AddConsonant("Sh", "ෂ");
            AddConsonant("GN", "ඥ");
            AddConsonant("KN", "ඤ");
            AddConsonant("Lu", "ළු");
            AddConsonant("dh", "ද");
            AddConsonant("ch", "ච");
            AddConsonant("kh", "ඛ");
            AddConsonant("th", "ත");
            AddConsonant("t", "ට");
            AddConsonant("k", "ක");
            AddConsonant("d", "ඩ");
            AddConsonant("n", "න");
            AddConsonant("p", "ප");
            AddConsonant("b", "බ");
            AddConsonant("m", "ම");
            AddConsonant("\\\\u005Cy", "‍ය");
            AddConsonant("Y", "‍ය");
            AddConsonant("y", "ය");
            AddConsonant("j", "ජ");
            AddConsonant("l", "ල");
            AddConsonant("v", "ව");
            AddConsonant("w", "ව");
            AddConsonant("s", "ස");
            AddConsonant("h", "හ");
            AddConsonant("N", "ණ");
            AddConsonant("L", "ළ");
            AddConsonant("K", "ඛ");
            AddConsonant("G", "ඝ");
            AddConsonant("T", "ඨ");
            AddConsonant("D", "ඪ");
            AddConsonant("P", "ඵ");
            AddConsonant("B", "ඹ");
            AddConsonant("f", "ෆ");
            AddConsonant("q", "ඣ");
            AddConsonant("g", "ග");
            AddConsonant("r", "ර");

            // Special combinations
            AddSpecialCombination("ruu", "ෲ");
            AddSpecialCombination("ru", "ෘ");
        }

        private void AddVowel(string pattern, string uniChar, string modChar)
        {
            _gn_v.Add(pattern);
            _gn_vUni.Add(uniChar);
            _gn_vmUni.Add(modChar);
        }

        private void AddConsonant(string pattern, string uniChar)
        {
            _gn_c.Add(pattern);
            _gn_cUni.Add(uniChar);
        }

        private void AddSpecialCombination(string pattern, string uniChar)
        {
            _gn_sc.Add(pattern);
            _gn_scUni.Add(uniChar);
        }

        public string Convert(string input)
        {
            if (string.IsNullOrEmpty(input))
                return "";

            // Step 1: Replace special characters
            for (int i = 0; i < _specialgn_c.Count; i++)
            {
                input = _specialgn_c[i].Replace(input, _specialgn_cUni[i]);
            }

            // Step 2: Handle consonant + special symbol combinations
            foreach (var sc in _gn_sc)
            {
                int idx = _gn_sc.IndexOf(sc);
                string scUni = _gn_scUni[idx];

                for (int i = 0; i < _gn_c.Count; i++)
                {
                    string pattern = _gn_c[i] + sc;
                    string replacement = _gn_cUni[i] + scUni;
                    input = input.Replace(pattern, replacement);
                }
            }

            // Step 3: Handle Rakaransha (consonant + r + vowel)
            for (int i = 0; i < _gn_c.Count; i++)
            {
                string consonantPattern = _gn_c[i];
                string consonantUni = _gn_cUni[i];

                foreach (var v in _gn_v)
                {
                    int vowelIndex = _gn_v.IndexOf(v);
                    string vowelMod = _gn_vmUni[vowelIndex];

                    string pattern = consonantPattern + "r" + v;
                    string replacement = consonantUni + "්‍ර" + vowelMod;
                    input = input.Replace(pattern, replacement);
                }

                // Handle standalone 'r'
                input = input.Replace(consonantPattern + "r", consonantUni + "්‍ර");
            }

            // Step 4: Build all consonant + vowel combinations
            var combinedPatterns = new List<(string key, string value)>();

            foreach (var consonant in _gn_c)
            {
                int cIdx = _gn_c.IndexOf(consonant);
                string consonantUni = _gn_cUni[cIdx];

                foreach (var vowel in _gn_v)
                {
                    int vIdx = _gn_v.IndexOf(vowel);
                    string vowelMod = _gn_vmUni[vIdx];

                    string key = consonant + vowel;
                    string value = consonantUni + vowelMod;
                    combinedPatterns.Add((key, value));
                }
            }

            // Sort by length descending to prioritize longer matches
            combinedPatterns.Sort((x, y) => y.key.Length.CompareTo(x.key.Length));

            foreach (var item in combinedPatterns)
            {
                input = input.Replace(item.key, item.value);
            }

            // Step 5: Handle standalone consonants
            for (int i = 0; i < _gn_c.Count; i++)
            {
                input = input.Replace(_gn_c[i], _gn_cUni[i] + "්");
            }

            // Step 6: Handle standalone vowels
            for (int i = 0; i < _gn_v.Count; i++)
            {
                input = input.Replace(_gn_v[i], _gn_vUni[i]);
            }

            return input;
        }
    }
}