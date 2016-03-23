﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KyntoLib.Interfaces.Instances.Rooms;
using KyntoLib.Interfaces.Instances.User;
using KyntoLib.Interfaces.Instances.Connections;
using KyntoLib.Interfaces.Instances.Rooms.Bots;
using KyntoLib.Interfaces.Instances.Rooms.Furni;
using KyntoLib.Interfaces.Instances.Rooms.Avatars;
using KyntoLib.Interfaces.Services;
using KyntoLib.Interfaces.Managers;

namespace KyntoServer.Instances.Rooms.Pathfinding
{
    public static class RotationalCalculator
    {
        /// <summary>
        /// Calculates the rotation of the avatar.
        /// </summary>
        /// <param name="NextX">The next x position.</param>
        /// <param name="NextY">The next y position.</param>
        /// <param name="CurrentX">The current x position.</param>
        /// <param name="CurrentY">The current y position.</param>
        /// <returns>The rotational number of the avatar.</returns>
        public static int CalculateRotation(int NextX, int NextY, int CurrentX, int CurrentY)
        {
            if (NextX > CurrentX)
            {
                return 2;
            }
            else if (NextX < CurrentX)
            {
                return 1;
            }
            else if (NextY < CurrentY)
            {
                return 3;
            }
            else if (NextY > CurrentY)
            {
                return 0;
            }
            else
            {
                return 0;
            }
        }
    }
}
