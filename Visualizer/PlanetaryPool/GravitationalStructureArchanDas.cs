using DongUtility;
using Geometry.Geometry3D;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using static WPFUtility.UtilityFunctions;

namespace Visualizer.PlanetaryPool
{
    /// <summary>
    /// This is the only file you should turn in
    /// Please make sure to replace "YOURNAME" with your actual name, in both the file name and the class name
    /// </summary>
    public class GravitationalStructureArchanDas : GravitationalStructure
    {
        double mass, mu, e, v_init, a, r_p;
        /// <summary>
        /// The constructor cannot take any arguments.  Please hard-code any parameters you wish to add.
        /// </summary>
        public GravitationalStructureArchanDas()
        {
            //GOAL: generate square orbit using 4 hyperbolic trajectories

            mass = 1e24;
            mu = mass * 6.6743e-11;
            v_init = 100000;
            a = (-mu) / (v_init * v_init);
            //The projectile should start at the midpoint of one side of the square
            //with some given high velocity specified in v_init
            //
            // Put your code here to create tetrahedra
            //sorry for not using these tetrahedra right dr dong

            double x, y;
            x = 1000e6 + a;
            y = 1000e6 + a;


            AddTetrahedron(new Tetrahedron(
                new Point(-10+x, -10+y, -10),
                new Point(-10 + x, 10 + y, -10),
                new Point(10 + x, -10 + y, -10),
                new Point(-10 + x, -10 + y, 10),
                mass, ConvertColor(Colors.CadetBlue), .03, .5));

            AddTetrahedron(new Tetrahedron(
                new Point(-10 - x, -10 + y, -10),
                new Point(-10 - x, 10 + y, -10),
                new Point(10 - x, -10 + y, -10),
                new Point(-10 - x, -10 + y, 10),
                mass, ConvertColor(Colors.CadetBlue), .03, .5));

            AddTetrahedron(new Tetrahedron(
                new Point(-10 - x, -10 - y, -10),
                new Point(-10 - x, 10 - y, -10),
                new Point(10 - x, -10 - y, -10),
                new Point(-10 - x, -10 - y, 10),
                mass, ConvertColor(Colors.CadetBlue), .03, .5));

            AddTetrahedron(new Tetrahedron(
                new Point(-10 + x, -10 - y, -10),
                new Point(-10 + x, 10 - y, -10),
                new Point(10 + x, -10 - y, -10),
                new Point(-10 + x, -10 - y, 10),
                mass, ConvertColor(Colors.CadetBlue), .03, .5));


        }

        public override Point StartingPoint => new Point(1000e6, 0, 0);

        public override Vector StartingVelocity => new Vector(0, v_init, 0);
    }
}
