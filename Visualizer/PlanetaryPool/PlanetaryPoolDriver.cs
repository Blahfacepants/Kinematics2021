using DongUtility;
using GraphControl;
using MotionVisualizer3D;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using Visualizer.Kinematics;
using static WPFUtility.UtilityFunctions;
using ProjectileN;
using UtilityLibraries;
using System.Windows.Media.Media3D;

namespace Visualizer.PlanetaryPool
{
    static internal class PlanetaryPoolDriver
    {
        static internal void RunPlanetaryPool()
        {
            var structure = new GravitationalStructureArchanDas();

            World world = new World();
            Projectile projectile = new Projectile(position: structure.StartingPoint.PositionVector(), velocity: structure.StartingVelocity, acceleration: new UtilityLibraries.Vector(0, 0, 0), mass: 1);
            world.AddProjectile(projectile);

            var engine = new WorldAdapter(world);

            var visualization = new PlanetaryPoolVisualization(structure, engine);

            Timeline.MaximumPoints = 3000;

            var fullViz = new MotionVisualizer3DControl(visualization);

            fullViz.Manager.Add3DGraph("Position", () => engine.Time, () => ConvertToDongUtilityVector(engine.Projectiles[0].Position), "Time (s)", "Position (m)");
            fullViz.Manager.Add3DGraph("Velocity", () => engine.Time, () => ConvertToDongUtilityVector(engine.Projectiles[0].Velocity), "Time (s)", "Velocity (m/s)");
            fullViz.Manager.Add3DGraph("Acceleration", () => engine.Time, () => ConvertToDongUtilityVector(engine.Projectiles[0].Acceleration), "Time (s)", "Acceleration (m/s^2)");

            fullViz.Show();
        }

        static DongUtility.Vector ConvertToDongUtilityVector(Vector3D vector)
        {
            return new DongUtility.Vector(vector.X, vector.Y, vector.Z);
        }

        static UtilityLibraries.Vector ConvertToUtilityVector(DongUtility.Vector vector)
        {
            return new UtilityLibraries.Vector(vector.X, vector.Y, vector.Z);
        }
    }


}
