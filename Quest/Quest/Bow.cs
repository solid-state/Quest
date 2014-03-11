using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Quest
{
    class Bow : Weapon
    {
        /* PROPERTIES */
        public override string Name { get { return "Bow"; } }

        /* CONSTRUCTOR */
        public Bow(Game game, Point location)
            : base(game, location) { }

        /* METHODS */
        public override void Attack(Direction direction, Random random)
        {
            int range = 100;
            int damage = 1;
            switch (direction)
            {
                case Direction.Up:
                    DamageEnemy(Direction.Up, range, damage, random);
                    break;
                case Direction.Left:
                    DamageEnemy(Direction.Left, range, damage, random);
                    break;
                case Direction.Right:
                    DamageEnemy(Direction.Right, range, damage, random);
                    break;
                case Direction.Down:
                    DamageEnemy(Direction.Down, range, damage, random);
                    break;
                default: break;
            }
        }
        public override void Sound()
        {
            game.attackSnd.URL = @"bolt.wav";
            game.attackSnd.controls.play();
        }
    }
}
