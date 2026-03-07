using System;

namespace MyApp.Domain.ValueObjects
{
    /// <summary>
    /// Repräsentiert einen Bereich von Beats per Minute (BPM).
    /// </summary>
    public record BpmRange
    {
        public double Min { get; init; }
        public double Max { get; init; }

        public BpmRange(double min, double max)
        {
            if (min < 0 || max < min)
                throw new ArgumentException("Ungültiger BPM-Bereich.");

            Min = min;
            Max = max;
        }

        public bool IsWithinRange(double bpm) => bpm >= Min && bpm <= Max;
    }
}
