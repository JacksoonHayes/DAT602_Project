﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAT602_Project
{
    internal class Player
    {
        public static Player CurrentPlayer { get; set; }
        public string UserName { get; set; }
        public int Strength { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
