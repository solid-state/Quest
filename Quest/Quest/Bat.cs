using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Quest
{
    class Bat : Enemy
    {
        /* CONSTRUCTOR */
        public Bat(Game game, Point location, Rectangle boundaries)
            : base(game, location, boundaries, 6)
        { }

        /* METHODS */
        public override void Move(Random random)
        {
            if (random.Next(1, 10) <= 5)
            {
                location = Move(FindPlayerDirection(game.PlayerLocation), game.Boundaries);
                if (NearPlayer())
                { game.HitPlayer(2, random); }
            }
            else
            {
                location = Move((Direction)random.Next(1, 4), game.Boundaries);
                if (NearPlayer())
                { game.HitPlayer(2, random); }
            }
        }
    }
}
