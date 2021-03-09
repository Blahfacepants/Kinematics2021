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
        List<Point> TetrahedronPoints = new List<Point>();
        /// <summary>
        /// The constructor cannot take any arguments.  Please hard-code any parameters you wish to add.
        /// </summary>
        public GravitationalStructureArchanDas()
        {
            //first one should point to .node file with vertices and ID's
            //second one to .ele file with tetrahedra constructed from 
            ReadPoints(10, new Vector(0, 0, 0), "C:\\Users\\creek\\Desktop\\CompuSci2020\\Kinematics2021\\Visualizer\\PlanetaryPool\\ArchanDasVertices.node");
            ReadTetrahedrons(1e10, Colors.CadetBlue, "C:\\Users\\creek\\Desktop\\CompuSci2020\\Kinematics2021\\Visualizer\\PlanetaryPool\\ArchanDasTetrahedrons.ele");
        }

        public void ReadTetrahedrons(double totalMass, System.Windows.Media.Color color, String tetpath)
        {
            //reads in text from file
            string text = System.IO.File.ReadAllText(tetpath);

            var lines = new List<String>(text.Split("\n"));

            //removes header and footer lines
            lines.RemoveAt(0);
            lines.RemoveAt(lines.Count - 1);
            lines.RemoveAt(lines.Count - 1);

            double mass = totalMass / lines.Count;

            foreach (var line in lines)
            {
                if (line.StartsWith("#") || line.Length == 0) continue;
                string[] split = line.Split();

                var tetstring = new List<String>();

                foreach (string vertex in split)
                {
                    if (vertex.Length >= 1)
                    {
                        tetstring.Add(vertex);
                    }
                }

                var id1 = Convert.ToInt32(tetstring[1]);
                var id2 = Convert.ToInt32(tetstring[2]);
                var id3 = Convert.ToInt32(tetstring[3]);
                var id4 = Convert.ToInt32(tetstring[4]);

                AddTetrahedron(new Tetrahedron(
                    TetrahedronPoints[id1 - 1],
                    TetrahedronPoints[id2 - 1],
                    TetrahedronPoints[id3 - 1],
                    TetrahedronPoints[id4 - 1],
                    mass, ConvertColor(color), .03, .5));
            }
        }

        public void ReadPoints(double itemScale, Vector itemOrigin, String node_path)
        {
            string nodeText = System.IO.File.ReadAllText(node_path);
            string[] linesArray = nodeText.Split("\n");
            var lines = new List<String>(linesArray);

            lines.RemoveAt(0);
            lines.RemoveAt(lines.Count - 1);
            lines.RemoveAt(lines.Count - 1);
            var points = new List<Point>();
            foreach (var line in lines)
            {
                if (line.StartsWith("#") || line.Length == 0) continue;
                string[] splitLineRaw = line.Split();

                var splitLine = new List<String>();

                foreach (string component in splitLineRaw)
                {
                    if (component.Length >= 1)
                    {
                        splitLine.Add(component);
                    }
                }

                var x = Convert.ToDouble(splitLine[1]) * itemScale + itemOrigin.X;
                var y = Convert.ToDouble(splitLine[2]) * itemScale + itemOrigin.Y;
                var z = Convert.ToDouble(splitLine[3]) * itemScale + itemOrigin.Z;

                points.Add(new Point(x, y, z));
            }

            TetrahedronPoints = points;
        }

        public override Point StartingPoint => new Point(-50, 220, 220);

        public override Vector StartingVelocity => new Vector(0, 0, 0);
    }
}
