using System;

namespace KeyBoard
{
    public static class TranslationManager
    {
        private static readonly SinhalaConverter _sinhalaConverter = new SinhalaConverter();
        private static readonly LegacyConverter _legacyConverter = new LegacyConverter();

        public static string Translate(string input, string conversionMode)
        {
            if (conversionMode == "Singlish to Legacy")
            {
                string sinhala = _sinhalaConverter.Convert(input);
                return _legacyConverter.toisiwara(sinhala);
            }
            else
            {
                return _sinhalaConverter.Convert(input);
            }
        }
    }
}