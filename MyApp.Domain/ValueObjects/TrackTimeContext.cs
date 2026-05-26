namespace MyApp.Domain.ValueObjects
{
    /// <summary>
    /// Der zeitliche Kontext (Tageszeit), in dem ein Track am besten funktioniert.
    /// </summary>
    public enum TrackTimeContext
    {
        None = 0,
        Sunrise = 1,
        Morning = 2,
        Afternoon = 3,
        Sunset = 4,
        Warmup = 5,
        PeakTime = 6,
        LateNight = 7,
        Afterhour = 8
    }
}
