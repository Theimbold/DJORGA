using System;
using System.Collections.Generic;

namespace DJORGA.Domain.ValueObjects
{
    /// <summary>
    /// Logik für die harmonische Kompatibilität (Camelot Wheel / Circle of Fifths).
    /// </summary>
    public static class KeyCompatibility
    {
        /// <summary>
        /// Prüft, ob zwei Camelot-Keys harmonisch kompatibel sind (Nachbarn auf dem Rad).
        /// Beispiel: 8A ist kompatibel mit 8A, 7A, 9A und 8B.
        /// </summary>
        public static bool AreCompatible(string key1, string key2)
        {
            if (string.IsNullOrEmpty(key1) || string.IsNullOrEmpty(key2)) return false;
            if (key1 == key2) return true;

            // Extrahiere Zahl und Buchstabe (z.B. 8A -> 8, A)
            if (!TryParseCamelot(key1, out int val1, out char type1) || 
                !TryParseCamelot(key2, out int val2, out char type2))
                return false;

            // Gleicher Typ (A/B), Nachbar-Zahl (+/- 1 oder 12/1 Übergang)
            if (type1 == type2)
            {
                int diff = Math.Abs(val1 - val2);
                return diff == 1 || diff == 11;
            }

            // Gleiche Zahl, Wechsel zwischen A und B (Relativ Dur/Moll)
            return val1 == val2;
        }

        private static bool TryParseCamelot(string key, out int value, out char type)
        {
            value = 0;
            type = ' ';
            if (key.Length < 2) return false;

            type = char.ToUpper(key[^1]);
            return int.TryParse(key[..^1], out value) && (type == 'A' || type == 'B');
        }
    }
}
