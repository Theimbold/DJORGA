namespace DJORGA.Application.DTOs
{
    /// <summary>
    /// Repräsentiert die Peak-Amplituden für verschiedene Frequenzbänder eines Zeitsegments.
    /// </summary>
    public struct FrequencyPeak
    {
        public float Low { get; init; }   // Bass (Rot)
        public float Mid { get; init; }   // Mitten (Grün)
        public float High { get; init; }  // Höhen (Blau)

        /// <summary>
        /// Gesamte Amplitude für die Darstellung der äußeren Form.
        /// </summary>
        public float Total => Low + Mid + High;

        public FrequencyPeak(float low, float mid, float high)
        {
            Low = low;
            Mid = mid;
            High = high;
        }
    }
}
