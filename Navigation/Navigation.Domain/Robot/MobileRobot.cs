﻿using Navigation.Domain.Robot.Visions;
using Navigation.Domain.Robot.Visions.Sensors;
using Navigation.Infrastructure;
using System;

namespace Navigation.Domain.Robot
{
    public abstract class MobileRobot
    {
        public Point Position { get; protected set; }
        public Lazy<IRobotVision> RobotVision { get; }

        protected MobileRobot(Lazy<IRobotVision> robotVision, Point position)
        {
            Position = position;
            RobotVision = robotVision;
        }

        public abstract void Run();
    }
}
