﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HazardMan
{
    class RenderShooter : RenderEntity
    {
        public RenderShooter()
        {
            fg = ConsoleColor.Black;
            renderIcon = '╦';
        }
    }
}
