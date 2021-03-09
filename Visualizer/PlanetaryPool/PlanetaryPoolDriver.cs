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
using Geometry.Geometry3D;

namespace Visualizer.PlanetaryPool
{
    static internal class PlanetaryPoolDriver
    {
        static internal void RunPlanetaryPool()
        {
            var structure = new GravitationalStructureArchanDas();

            World world = new World();
            foreach(Tetrahedron tet in structure.GetTetrahedra())
            {
                world.AddProjectile(new ProjectileN.Tetrahedron(ConvertToUtilityVector(tet.Points[0]), ConvertToUtilityVector(tet.Points[1]), ConvertToUtilityVector(tet.Points[2]), ConvertToUtilityVector(tet.Points[3]), tet.Mass));
            }

            Projectile projectile = new Projectile(position: ConvertToUtilityVector(structure.StartingPoint.PositionVector()), velocity: ConvertToUtilityVector(structure.StartingVelocity), acceleration: new UtilityLibraries.Vector(0, 0, 0), mass: 100000);

            Dictionary<ProjectileParent, ProjectileParent> conn = new Dictionary<ProjectileParent, ProjectileParent>();
            foreach(ProjectileParent p in world.projectiles)
            {
                conn.Add(p, projectile);
            }

            world.connections = conn;

            world.AddProjectile(projectile);
            var engine = new WorldAdapter(world);

            var visualization = new PlanetaryPoolVisualization(structure, engine);

            Timeline.MaximumPoints = 3000;

            var fullViz = new MotionVisualizer3DControl(visualization);

            fullViz.Manager.Add3DGraph("Position", () => engine.Time, () => ConvertToDongUtilityVector(engine.Projectiles[engine.Projectiles.Count-1].Position), "Time (s)", "Position (m)");
            fullViz.Manager.Add3DGraph("Velocity", () => engine.Time, () => ConvertToDongUtilityVector(engine.Projectiles[engine.Projectiles.Count - 1].Velocity), "Time (s)", "Velocity (m/s)");
            fullViz.Manager.Add3DGraph("Acceleration", () => engine.Time, () => ConvertToDongUtilityVector(engine.Projectiles[engine.Projectiles.Count - 1].Acceleration), "Time (s)", "Acceleration (m/s^2)");

            fullViz.Manager.AddSingleGraph("X vs. Y", ConvertColor(Colors.Teal), () => engine.Projectiles[engine.Projectiles.Count - 1].Position.X, (() => engine.Projectiles[0].Position.X),
                           "Y (m)", "X (m)");
            fullViz.FastDraw = true;
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

        static UtilityLibraries.Vector ConvertToUtilityVector(Point vector)
        {
            return new UtilityLibraries.Vector(vector.X, vector.Y, vector.Z);
        }

        /// <summary>
        /// Runs a trajectory file already created (as a test)
        /// To use this, modify Driver.cs to point to this function instead of RunPlanetaryPool()
        /// And, of course, change the filename
        /// </summary>
        static internal void TestTrajectoryFile()
        {
            var fullViz = new MotionVisualizer3DControl(@"C:\Users\creek\Documents\Trajectory.dat", new VisualizerControl.VisualizerCommandFileReader());
            fullViz.Show();
        }
    }


}
