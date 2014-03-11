using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Quest
{
    class Ghoul : Enemy
    {
        /* CONSTRUCTOR */
        public Ghoul(Game game, Point location, Rectangle boundaries)
            : base(game, location, boundaries, 10)
        { }

        /* METHODS */
        public override void Move(Random random)
        {

            if (random.Next(1, 12) <= 8)
                location = Move(FindPlayerDirection(game.PlayerLocation), game.Boundaries);
            if (NearPlayer())
                { game.HitPlayer(4, random); }
        }
    }
}
