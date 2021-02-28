using UtilityLibraries;
using Visualizer.Kinematics;
using System;
using DongUtility;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using ProjectileN;

namespace Visualizer.Kinematics
{
    class ProjectileAdapter : IProjectile
    {
        Projectile projectile;

        //Passes the projectile object by reference (I hope)
        public ProjectileAdapter(Projectile projectile)
        {
            this.projectile = projectile;
        }

        public Vector3D Position
        {
            get
            {
                return new Vector3D(projectile.position.X, projectile.position.Y, projectile.position.Z);
            }
        }

        public Vector3D Velocity
        {
            get
            {
                return new Vector3D(projectile.velocity.X, projectile.velocity.Y, projectile.velocity.Z);
            }
        }



        public Color Color
        {
            get
            {
                return Color.FromRgb(255, 0, 0);
            }
        }


        public VisualizerControl.Shapes.Shape3D Shape
        {
            get
            {
                return new VisualizerControl.Shapes.Sphere3D();
            }
        }


        public double Size
        {
            get
            {
                return 1;
            }
        }

        public Vector3D Accleration
        {
            get
            {
                return new Vector3D(projectile.acceleration.X, projectile.acceleration.Y, projectile.acceleration.Z);
            }
        }
    }
}