﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navigation.Domain.Robot.Visions
{
    public interface IRobotVision
    {
        VisionResult LookAround();
    }
}