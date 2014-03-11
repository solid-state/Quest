using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Quest
{
    abstract class Enemy : Mover
    {
        /*  FIELD VARIABLES */
        private const int nearPlayerDistance = 25;

        /* PROPERTIES */
        private int hitPoints;
        public int HitPoints { get { return hitPoints; } }
        private Size dimension = new Size(30, 30);
        public Size Dimension { get { return dimension; } }

        public bool Dead
        {   get {
                if (hitPoints <= 0) return true;
                else return false;
                }
        }

        /* CONSTRUCTOR */
        public Enemy(Game game, Point location, Rectangle boundaries, int hitPoints)
            : base(game, location) { this.hitPoints = hitPoints; }

        /* METHODS */
        public abstract void Move(Random random);

        public void Hit(int maxDamage, Random random)
        {
            hitPoints -= random.Next(1, maxDamage); 
        }

        protected bool NearPlayer()
            { return (Nearby(game.PlayerLocation, nearPlayerDistance)); }

        protected Direction FindPlayerDirection(Point playerLocation)
        {
            Direction directionToMove;
            if (playerLocation.X > location.X + 10)
                directionToMove = Direction.Right;
            else if (playerLocation.X < location.X - 10)
                directionToMove = Direction.Left;
            else if (playerLocation.Y < location.Y - 10)
                directionToMove = Direction.Up;
            else
                directionToMove = Direction.Down;
            return directionToMove;
        }
    }
}
