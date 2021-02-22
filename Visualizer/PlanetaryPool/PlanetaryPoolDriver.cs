using DongUtility;
using GraphControl;
using MotionVisualizer3D;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using Visualizer.Kinematics;
using static WPFUtility.UtilityFunctions;

namespace Visualizer.PlanetaryPool
{
    static internal class PlanetaryPoolDriver
    {
        static internal void RunPlanetaryPool()
        {
            var structure = new GravitationalStructureYOURNAME();

            //World world = new World();
            //Projectile projectile = new Projectile(structure.StartingPoint.PositionVector(), structure.StartingVelocity, 1);
            //world.AddProjectile(projectile);

            //var adapter = new EngineAdapter(world);

            //var visualization = new PlanetaryPoolVisualization(structure, adapter);

            //Timeline.MaximumPoints = 3000;

            //var fullViz = new MotionVisualizer3DControl(visualization);

            //fullViz.Manager.Add3DGraph("Position", () => engine.Time, () => engine.Projectiles[0].Position, "Time (s)", "Position (m)");
            //fullViz.Manager.Add3DGraph("Velocity", () => engine.Time, () => engine.Projectiles[0].Velocity, "Time (s)", "Velocity (m/s)");
            //fullViz.Manager.Add3DGraph("Acceleration", () => engine.Time, () => engine.Projectiles[0].Acceleration, "Time (s)", "Acceleration (m/s^2)");

            //fullViz.Show();
        }
    }
}
