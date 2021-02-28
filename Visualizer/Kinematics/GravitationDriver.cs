using GraphControl;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using static WPFUtility.UtilityFunctions;
using MotionVisualizer3D;
using ProjectileN;
using UtilityLibraries;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Visualizer.Kinematics
{
    static internal class GravitationDriver
    {
        static internal void RunKinematics()
        {
            RunGravitation3();
        }
        static internal void RunGravitation1()
        {
            Dictionary<int, int> conn = new Dictionary<int, int>();
            conn.Add(0, 1);
            // Create a World object
            World world = new World(conn, time_limit:2.628e6);

            // Create Projectiles and set up the World 
            ProjectileN.Projectile earth = new ProjectileN.Projectile(mass: 5.972e24, immovable:true);
            world.AddProjectile(earth);
            ProjectileN.Projectile moon = new ProjectileN.Projectile(mass: 7.347673e22, position: new Vector(384.4e6, 0, 0), velocity: new Vector(0, 1022, 0), acceleration: new Vector(0, 0, 0));
            world.AddProjectile(moon);
            //... and so forth

            // Create an EngineAdapter
            var engine = new WorldAdapter(world);

            // Once you have made your EngineAdapter, uncomment the rest of these lines.
            var visualization = new KinematicsVisualization(engine);

            Timeline.MaximumPoints = 10000;

            var fullViz = new MotionVisualizer3DControl(visualization);

            fullViz.Manager.Add3DGraph("Position", () => engine.Time, () => ConvertToDongUtilityVector(engine.Projectiles[1].Position), "Time (s)", "Position (m)");
            fullViz.Manager.Add3DGraph("Velocity", () => engine.Time, () => ConvertToDongUtilityVector(engine.Projectiles[1].Velocity), "Time (s)", "Velocity (m/s)");
            fullViz.Manager.Add3DGraph("Acceleration", () => engine.Time, () => ConvertToDongUtilityVector(engine.Projectiles[1].Accleration), "Time (s)", "Acceleration (m/s^2)");

            fullViz.Manager.AddSingleGraph("X vs. Y", ConvertColor(Colors.Teal), () => engine.Projectiles[1].Position.X, (() => engine.Projectiles[1].Position.Y),
               "Y (m)", "X (m)");

            fullViz.FastDraw = true;
            fullViz.Show();
        }

        static internal void RunGravitation2()
        {
            Dictionary<int, int> conn = new Dictionary<int, int>();
            conn.Add(0, 1);
            // Create a World object
            World world = new World(conn, time_limit: 20e7);

            // Create Projectiles and set up the World 
            ProjectileN.Projectile earth = new ProjectileN.Projectile(mass: 5.972e24, immovable: true, shape:Shape.Cube, radius: 6.36e6, ptcount:1000);
            world.AddProjectile(earth);
            ProjectileN.Projectile moon = new ProjectileN.Projectile(mass: 7.347673e22, position: new Vector(384.4e6, 0, 0), velocity: new Vector(0, 1022, 0), acceleration: new Vector(0, 0, 0));
            world.AddProjectile(moon);
            //... and so forth

            // Create an EngineAdapter
            var engine = new WorldAdapter(world);

            var visualization = new KinematicsVisualization(engine);

            Timeline.MaximumPoints = 10000;

            var fullViz = new MotionVisualizer3DControl(visualization);

            fullViz.Manager.Add3DGraph("Position", () => engine.Time, () => ConvertToDongUtilityVector(engine.Projectiles[1].Position), "Time (s)", "Position (m)");
            fullViz.Manager.Add3DGraph("Velocity", () => engine.Time, () => ConvertToDongUtilityVector(engine.Projectiles[1].Velocity), "Time (s)", "Velocity (m/s)");
            fullViz.Manager.Add3DGraph("Acceleration", () => engine.Time, () => ConvertToDongUtilityVector(engine.Projectiles[1].Accleration), "Time (s)", "Acceleration (m/s^2)");

            fullViz.Manager.AddSingleGraph("X vs. Y", ConvertColor(Colors.Teal), () => engine.Projectiles[1].Position.X, (() => engine.Projectiles[1].Position.Y),
               "Y (m)", "X (m)");
            Debug.WriteLine("working");
            fullViz.FastDraw = true;
            fullViz.Show();
        }

        static internal void RunGravitation3()
        {
            Dictionary<int, int> conn = new Dictionary<int, int>();
            conn.Add(0, 1);
            // Create a World object
            World world = new World(conn, time_limit: 20e7);

            // Create Projectiles and set up the World 
            ProjectileN.Projectile earth = new ProjectileN.Projectile(mass: 5.972e24, immovable: true, shape: Shape.Sphere, radius: 384.4e6, ptcount: 2);
            world.AddProjectile(earth);
            ProjectileN.Projectile moon = new ProjectileN.Projectile(mass: 7.347673e22, position: new Vector(10000, 500000, 0), velocity: new Vector(0, 0, 0), acceleration: new Vector(0, 0, 0));
            world.AddProjectile(moon);
            //... and so forth

            // Create an EngineAdapter
            var engine = new WorldAdapter(world);

            var visualization = new KinematicsVisualization(engine);

            Timeline.MaximumPoints = 10000;

            var fullViz = new MotionVisualizer3DControl(visualization);

            fullViz.Manager.Add3DGraph("Position", () => engine.Time, () => ConvertToDongUtilityVector(engine.Projectiles[1].Position), "Time (s)", "Position (m)");
            fullViz.Manager.Add3DGraph("Velocity", () => engine.Time, () => ConvertToDongUtilityVector(engine.Projectiles[1].Velocity), "Time (s)", "Velocity (m/s)");
            fullViz.Manager.Add3DGraph("Acceleration", () => engine.Time, () => ConvertToDongUtilityVector(engine.Projectiles[1].Accleration), "Time (s)", "Acceleration (m/s^2)");

            fullViz.Manager.AddSingleGraph("X vs. Y", ConvertColor(Colors.Teal), () => engine.Projectiles[1].Position.X, (() => engine.Projectiles[1].Position.Y),
               "Y (m)", "X (m)");

            fullViz.FastDraw = true;
            fullViz.Show();
        }

        static DongUtility.Vector ConvertToDongUtilityVector(Vector3D vector)
        {
            return new DongUtility.Vector(vector.X, vector.Y, vector.Z);
        }

        static ProjectileN.Projectile CenterOfMass(List<ProjectileN.Projectile> l)
        {
            double net_mass = 0;
            Vector net_vel = new Vector();
            Vector net_pos = new Vector();
            Vector net_acc = new Vector();
            foreach (ProjectileN.Projectile p in l)
            {
                net_mass += p.mass;
                net_vel += p.velocity * p.mass;
                net_pos += p.position * p.mass;
                net_acc += p.acceleration * p.mass;
            }
            return new ProjectileN.Projectile(mass: net_mass, position: net_pos / net_mass, velocity: net_vel / net_mass, acceleration: net_acc / net_mass);
        }
    }
}
