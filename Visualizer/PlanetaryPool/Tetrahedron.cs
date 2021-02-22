using System;
using Geometry.Geometry3D;

namespace Visualizer.PlanetaryPool
{
    public class Tetrahedron
    {
        private Geometry.Geometry3D.Tetrahedron underlying;
        public Point[] Points => underlying.Points;

        public double Mass { get; }
        public System.Drawing.Color Color { get; }
        public double Fresnel { get; }
        public double Roughness { get; }

        public Tetrahedron(Point point1, Point point2, Point point3, Point point4, double mass, System.Drawing.Color color, double fresnel = .03, double roughness = .5)
        {
            if (mass <= 0 || double.IsNaN(mass) || double.IsInfinity(mass))
                throw new ArgumentException("Mass must be a positive number!");

            underlying = new Geometry.Geometry3D.Tetrahedron(point1, point2, point3, point4);        
            Mass = mass;
            Color = color;

            if (CheckCoPlanar())
                throw new ArgumentException("Tetrahedron must have noncoplanar points!");
        }

        private bool CheckCoPlanar()
        {
            var plane012 = new Plane(underlying.Points[0], underlying.Points[1], underlying.Points[2]);
            return plane012.IsInPlane(underlying.Points[3]);
        }

        public bool Overlap(Tetrahedron other)
        {
            return underlying.BulkOverlap(other.underlying);
        }
    }
}
