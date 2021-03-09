using DongUtility;
using Geometry.Geometry3D;
using System;
using System.Collections.Generic;
using System.Text;

namespace Visualizer.PlanetaryPool
{
    /// <summary>
    /// A bunch of tetrahedrons
    /// </summary>
    abstract public class GravitationalStructure
    {
        private List<Tetrahedron> tetrahedrons = new List<Tetrahedron>();

        /// <summary>
        /// Add a tetrahedron. Will throw an exception if it overlaps with any existing tetrahedra
        /// </summary>
        /// <param name="tetrahedron"></param>
        public void AddTetrahedron(Tetrahedron tetrahedron)
        {
            foreach (var tet in tetrahedrons)
            {
                //if (tet.Overlap(tetrahedron))
                //{
                //    throw new ArgumentException("Tetrahedra are not allowed to overlap!");
                //}
            }

            tetrahedrons.Add(tetrahedron);
        }

        // Gets a list of all tetrahedra
        // Done deliberately by copy (which is slow) to prevent manipulation of the list
        public List<Tetrahedron> GetTetrahedra()
        {
            var list = new List<Tetrahedron>();
            foreach (var tetrahedron in tetrahedrons)
            {
                var newTetrahedron = new Tetrahedron(tetrahedron.Points[0], tetrahedron.Points[1], tetrahedron.Points[2], tetrahedron.Points[3],
                    tetrahedron.Mass, tetrahedron.Color, tetrahedron.Fresnel, tetrahedron.Roughness);
                list.Add(tetrahedron);
            }
            return list;
        }

        /// <summary>
        /// The position the object will start at
        /// </summary>
        abstract public Point StartingPoint { get; }

        /// <summary>
        /// The initial velocity of the object
        /// </summary>
        abstract public Vector StartingVelocity { get; }
    }
}
