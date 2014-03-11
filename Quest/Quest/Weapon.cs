using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Quest
{
    abstract class Weapon : Mover
    {
        /* PROPERTIES */
        private bool pickedUp;
        public bool PickedUp { get { return pickedUp; } }
        public abstract string Name { get; }
        private const int nearPlayerDistance = 25; 

        /* CONSTRUCTOR */
        public Weapon(Game game, Point location)
            : base(game, location)
        { pickedUp = false; }

        /* METHODS */
        public void PickUpWeapon() { pickedUp = true; }

        public abstract void Attack(Direction direction, Random random);
        public abstract void Sound();

        protected bool DamageEnemy(Direction direction, int range, int damage, Random random)
        {
            Point target = game.PlayerLocation;
            for (int distance = 0; distance < range; distance++)
            {
                foreach (Enemy enemy in game.Enemies)
                {
                    if (enemy.HitPoints > 0 && Nearby(enemy.Location, target, range))//If in localaty, damage enemy
                    {
                        enemy.Hit(damage, random); // method that subtracts damage from enemy
                        return true;
                    }
                }
                target = Move(direction, game.Boundaries);
            }
            return false;
        }
    }
}
