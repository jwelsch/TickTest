using Avalonia;
using System;

namespace TickTest
{
    public interface IPositionCalculator
    {
        Point NewPosition(Point startPosition, IVelocity velocity, Rect bounds);
    }

    public class PositionCalculator : IPositionCalculator
    {
        public Point NewPosition(Point startPosition, IVelocity velocity, Rect bounds)
        {
            var deltaX = 0.0;
            var deltaY = 0.0;

            if (velocity.Heading == 0.0)
            {
                deltaY = -velocity.DistancePerTick;
                //return new Point(startPosition.X, startPosition.Y - velocity.DistancePerTick);
            }
            else if (velocity.Heading == 90.0)
            {
                deltaX = velocity.DistancePerTick;
                //return new Point(startPosition.X + velocity.DistancePerTick, startPosition.Y);
            }
            else if (velocity.Heading == 180.0)
            {
                deltaX = -velocity.DistancePerTick;
                //return new Point(startPosition.X - velocity.DistancePerTick, startPosition.Y);
            }
            else if (velocity.Heading == 270.0)
            {
                deltaY = velocity.DistancePerTick;
                //return new Point(startPosition.X, startPosition.Y + velocity.DistancePerTick);
            }
            else if (velocity.Heading > 0.0 && velocity.Heading < 90.0)
            {
                deltaX = Math.Sin(velocity.Heading * (Math.PI / 180.0)) * velocity.DistancePerTick;
                deltaY = Math.Cos(velocity.Heading * (Math.PI / 180.0)) * -velocity.DistancePerTick;

                //return new Point(startPosition.X + deltaX, startPosition.Y - deltaY);
            }
            else if (velocity.Heading > 90.0 && velocity.Heading < 180.0)
            {
                deltaX = Math.Sin((180.0 - velocity.Heading) * (Math.PI / 180.0)) * velocity.DistancePerTick;
                deltaY = Math.Cos((180.0 - velocity.Heading) * (Math.PI / 180.0)) * velocity.DistancePerTick;

                //return new Point(startPosition.X + deltaX, startPosition.Y + deltaY);
            }
            else if (velocity.Heading > 180.0 && velocity.Heading < 270.0)
            {
                deltaX = Math.Sin((180.0 - velocity.Heading) * (Math.PI / 180.0)) * -velocity.DistancePerTick;
                deltaY = Math.Cos((180.0 - velocity.Heading) * (Math.PI / 180.0)) * velocity.DistancePerTick;

                //return new Point(startPosition.X - deltaX, startPosition.Y + deltaY);
            }
            else if (velocity.Heading > 270.0 && velocity.Heading < 360.0)
            {
                deltaX = Math.Sin(velocity.Heading * (Math.PI / 180.0)) * -velocity.DistancePerTick;
                deltaY = Math.Cos(velocity.Heading * (Math.PI / 180.0)) * -velocity.DistancePerTick;

                //return new Point(startPosition.X - deltaX, startPosition.Y - deltaY);
            }
            else
            {
                // Something went wrong.
                throw new InvalidOperationException($"Unexpected velocity heading: {velocity.Heading}");
            }

            return new Point(startPosition.X + deltaX, startPosition.Y + deltaY);
        }
    }
}
