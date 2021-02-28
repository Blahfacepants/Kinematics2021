using GraphControl;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using static WPFUtility.UtilityFunctions;
using MotionVisualizer3D;
using ProjectileN;

namespace Visualizer.Kinematics
{
    static internal class KinematicsDriver
    {
        //static internal void RunKinematics()
        //{
        //    // Create a World object
        //    World engine = new World();

        //    // Create Projectiles and set up the World 
        //    Projectile earth = new Projectile(mass:5.972e24, immovable:true);
        //    engine.AddProjectile(projectile);
        //    //... and so forth

        //    // Create an EngineAdapter
        //    var adapter = new WorldAdapter(engine);

        //    // Once you have made your EngineAdapter, uncomment the rest of these lines.
        //    var visualization = new KinematicsVisualization(adapter);

        //    Timeline.MaximumPoints = 1000;

        //    var fullViz = new MotionVisualizer3DControl(visualization);

        //    fullViz.Manager.Add3DGraph("Position", () => engine.Time, () => ConvertToVector3D(engine.Projectiles[0].Position), "Time (s)", "Position (m)");
        //    fullViz.Manager.Add3DGraph("Velocity", () => engine.Time, () => ConvertToVector3D(engine.Projectiles[0].Velocity), "Time (s)", "Velocity (m/s)");
        //    fullViz.Manager.Add3DGraph("Acceleration", () => engine.Time, () => ConvertToVector3D(engine.Projectiles[0].Acceleration), "Time (s)", "Acceleration (m/s^2)");

        //    fullViz.Manager.AddSingleGraph("Speed", ConvertColor(Colors.Teal), () => engine.Time, (() => engine.Projectiles[0].Velocity.Magnitude),
        //        "Time (s)", "Speed (m/s)");

        //    fullViz.Show();
        //}
    }
}
