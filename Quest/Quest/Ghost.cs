﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Quest
{
    class Ghost : Enemy
    {
        /* CONSTRUCTOR */
        public Ghost(Game game, Point location, Rectangle boundaries)
            : base(game, location, boundaries, 8)
        { }

        /* METHODS */
        public override void Move(Random random)
        {
            if (random.Next(1, 12) <= 4)
                location = Move(FindPlayerDirection(game.PlayerLocation), game.Boundaries);
            if (NearPlayer())
                { game.HitPlayer(3, random); }
        }
    }
}
