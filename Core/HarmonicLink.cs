namespace Core
{
    public sealed class HarmonicLink
    {
        public string SourceTrackId { get; init; } = string.Empty;
        public string TargetTrackId { get; init; } = string.Empty;
        public double Score { get; init; }
        public string Reason { get; init; } = string.Empty;
    }
}