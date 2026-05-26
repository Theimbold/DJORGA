using System;
using System.Collections.Generic;

namespace MyApp.Domain.ValueObjects
{
    /// <summary>
    /// Repräsentiert einen Camelot-Key (z.B. 8A, 11B) und bietet Logik für harmonische Kompatibilität.
    /// </summary>
    public sealed record CamelotKey
    {
        public int Number { get; } // 1-12
        public char Mode { get; }   // 'A' (Minor) oder 'B' (Major)

        public CamelotKey(int number, char mode)
        {
            if (number < 1 || number > 12)
                throw new ArgumentOutOfRangeException(nameof(number), "Camelot Number muss zwischen 1 und 12 liegen.");

            mode = char.ToUpper(mode);
            if (mode != 'A' && mode != 'B')
                throw new ArgumentException("Camelot Mode muss 'A' (Minor) oder 'B' (Major) sein.", nameof(mode));

            Number = number;
            Mode = mode;
        }

        public static CamelotKey Parse(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key darf nicht leer sein.", nameof(key));

            key = key.Trim().ToUpper();
            char mode = key[^1];
            if (!int.TryParse(key[..^1], out int number))
                throw new FormatException($"Ungültiges Camelot-Format: {key}");

            return new CamelotKey(number, mode);
        }

        public override string ToString() => $"{Number}{Mode}";

        /// <summary>
        /// Prüft, ob der andere Key harmonisch kompatibel ist (Nachbar oder Relativ).
        /// </summary>
        public bool IsCompatibleWith(CamelotKey other)
        {
            if (this == other) return true;

            // Gleicher Mode: Nachbarn auf dem Rad (z.B. 8A -> 7A oder 9A)
            if (Mode == other.Mode)
            {
                int diff = Math.Abs(Number - other.Number);
                return diff == 1 || diff == 11; // 11 deckt den Sprung von 12 auf 1 ab
            }

            // Unterschiedlicher Mode: Nur wenn die Nummer gleich ist (Relativ Major/Minor, z.B. 8A -> 8B)
            return Number == other.Number;
        }

        /// <summary>
        /// Gibt alle harmonisch perfekt passenden Keys zurück.
        /// </summary>
        public IEnumerable<CamelotKey> GetNeighbors()
        {
            // Gleicher Key
            yield return this;

            // Relativ Major/Minor
            yield return new CamelotKey(Number, Mode == 'A' ? 'B' : 'A');

            // Nachbarn (Links/Rechts auf dem Rad)
            yield return new CamelotKey(Number == 1 ? 12 : Number - 1, Mode);
            yield return new CamelotKey(Number == 12 ? 1 : Number + 1, Mode);
        }
    }
}
