namespace DJORGA.Application.Services
{
    public class ScoringWeights
    {
        public double KeyWeight { get; init; } = 0.30;
        public double BpmWeight { get; init; } = 0.30;
        public double DnaWeight { get; init; } = 0.40;
    }
}
