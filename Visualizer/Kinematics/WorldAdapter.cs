using UtilityLibraries;
using Visualizer.Kinematics;
using System;
using System.Collections.Generic;
using ProjectileN;

namespace Visualizer.Kinematics
{
    class WorldAdapter : IEngine
    {
        World world;
        public WorldAdapter(World world)
        {
            this.world = world;
        }

        public bool Tick(double newTime)
        {
            try
            {
                world.Tick(newTime);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<IProjectile> Projectiles
        {
            get
            {
                List<IProjectile> output = new List<IProjectile>();
                foreach (ProjectileParent p in world.projectiles)
                {
                    output.Add(new ProjectileAdapter(p));
                }
                return output;
            }
        }
        public double Time
        {
            get
            {
                return world.time;
            }
        }
    }
}