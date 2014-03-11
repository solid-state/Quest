using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Quest
{
    public enum Direction
    {
        Up,
        Left,
        Right,
        Down,
    }

    abstract class Mover
    {
        /*  FIELD VARIABLES */
        private const int moveInternal = 10;
        protected Game game;

        /* PROPERTIES */
        protected Point location;
        public Point Location { get { return location; } }

        /* CONSTRUCTOR */
        public Mover(Game game, Point location)
        {
            this.game = game;
            this.location = location;
        }

        /* METHODS */
        public bool Nearby(Point locationToCheak, int distance)
        {
            if (Math.Abs(location.X - locationToCheak.X) < distance &&
                (Math.Abs(location.Y - locationToCheak.Y) < distance))
            { return true; }
            else
            { return false; }
        }

        public bool Nearby(Point locationToCheak, Point location, int distance)
        {
            if (Math.Abs(location.X - locationToCheak.X) < distance &&
                (Math.Abs(location.Y - locationToCheak.Y) < distance))
            { return true; }
            else
            { return false; }
        }



        public Point Move(Direction direction, Rectangle boundaries)
        {
            Point newLocation = location;
            switch (direction)
            {
                case Direction.Up:
                    if (newLocation.Y - moveInternal >= boundaries.Top)
                        newLocation.Y -= moveInternal;
                    break;
                case Direction.Down:
                    if (newLocation.Y + moveInternal <= boundaries.Bottom)
                        newLocation.Y += moveInternal;
                    break;
                case Direction.Left:
                    if (newLocation.X - moveInternal >= boundaries.Left)
                        newLocation.X -= moveInternal;
                    break;
                case Direction.Right:
                    if (newLocation.X + moveInternal <= boundaries.Right)
                        newLocation.X += moveInternal;
                    break;
                default: break;
            }
            return newLocation;
        }

    }
}
