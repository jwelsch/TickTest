namespace TickTest
{
    public interface IVelocity
    {
        double Heading { get; }

        double DistancePerTick { get; }
    }

    public class Velocity(double heading, double distancePerTick) : IVelocity
    {
        public double Heading { get; } = heading;

        public double DistancePerTick { get; } = distancePerTick;
    }
}
