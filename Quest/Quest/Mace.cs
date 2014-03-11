using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Quest
{
    class Mace : Weapon
    {
        /* PROPERTIES */
        public override string Name { get { return "Mace"; } }

        /* CONSTRUCTOR */
        public Mace(Game game, Point location)
            : base(game, location) { }

        /* METHODS */
        public override void Attack(Direction direction, Random random)
        {
            DamageEnemy(Direction.Up, 30, 6, random);
            DamageEnemy(Direction.Left, 30, 6, random);
            DamageEnemy(Direction.Right, 30, 6, random);
            DamageEnemy(Direction.Down, 30, 6, random);
        }

        public override void Sound()
        {
            game.attackSnd.URL = @"sphere.wav";
            game.attackSnd.controls.play();
        }
    }
}
