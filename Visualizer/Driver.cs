namespace Visualizer
{
    class Driver
    {
        static internal void Run()
        {
            Kinematics.KinematicsDriver.RunKinematics();
            PlanetaryPool.PlanetaryPoolDriver.RunPlanetaryPool();
        }

    }

}
