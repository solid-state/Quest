using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Quest
{
    class RedPotion : Weapon, IPotion
    {
        /* PROPERTIES */
        public override string Name { get { return "RedPotion"; } }
        public bool Used { get; private set; }

        /* CONSTRUCTOR */
        public RedPotion(Game game, Point location)
            : base(game, location) { Used = false; }

        /* METHODS */
        public override void Attack(Direction direction, Random random)
        {
            game.IncreasePlayerHealth(10, random);
            Used = true;
        }
        public override void Sound()
        {
            game.sounds.URL = @"heal.wav";
            game.sounds.controls.play();
        }
    }
}
