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
        /// <summary>
        /// The constructor cannot take any arguments.  Please hard-code any parameters you wish to add.
        /// </summary>
        public GravitationalStructureArchanDas()
        {
            // Put your code here to create tetrahedra

            AddTetrahedron(new Tetrahedron(
                new Point(-10, -10, -10),
                new Point(-10, 10, -10),
                new Point(10, -10, -10),
                new Point(-10, -10, 10),
                1e9, ConvertColor(Colors.CadetBlue), .03, .5));

            AddTetrahedron(new Tetrahedron(
                new Point(10, 10, 10),
                new Point(-10, 10, -10),
                new Point(10, -10, -10),
                new Point(-10, -10, 10),
    1e9, ConvertColor(Colors.CornflowerBlue), .3, .2));
        }

        public override Point StartingPoint => new Point(0, 0, 0);

        public override Vector StartingVelocity => new Vector(0, 1, 0);
    }
}
