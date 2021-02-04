using DongUtility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using VisualizerControl.Shapes;
using WPFUtility;
using static WPFUtility.UtilityFunctions;

namespace VisualizerControl
{
    public class ObjectPrototype
    {
        internal Shape3D Shape { get; }
        internal MaterialPrototype MaterialPrototype { get; }

        internal Vector3D Position { get; }
        internal Vector3D Scale { get; }
        internal Matrix3D Rotation { get; }

        public void WriteToFile(BinaryWriter bw)
        {
            Shape.WriteToFile(bw);
            MaterialPrototype.WriteToFile(bw);
            bw.Write(Position);
            bw.Write(Scale);
            bw.Write(Rotation);
        }

        public static ObjectPrototype ReadFromFile(BinaryReader br)
        {
            var shape = Shape3D.ReadShapeFromFile(br);
            var material = MaterialPrototype.ReadFromFile(br);
            var position = br.ReadVector3D();
            var scale = br.ReadVector3D();
            var rotation = br.ReadMatrix3D();

            return new ObjectPrototype(shape, material, position, scale, rotation);
        }

        public ObjectPrototype(Shape3D shape, Color color, bool specular = false) :
            this(shape, new BasicMaterial(color, specular))
        { }

        public ObjectPrototype(Shape3D shape, MaterialPrototype material) :
            this(shape, material, new Vector3D(0, 0, 0), new Vector3D(1, 1, 1))
        {}

        public ObjectPrototype(Shape3D shape, MaterialPrototype material, Vector3D position, Vector3D scale) :
            this(shape, material, position, scale, Matrix3D.Identity)
        { }

        public ObjectPrototype(Shape3D shape, MaterialPrototype material, Vector3D position, Vector3D scale,
            Matrix3D rotation)
        {
            Shape = shape;
            MaterialPrototype = material;
            Position = position;
            Scale = scale;
            Rotation = rotation;
        }

        public ObjectPrototype(Shape3D shape, MaterialPrototype material, Vector position, Vector scale, Rotation rotation) :
            this(shape, material, ConvertVector(position), ConvertVector(scale), ConvertToMatrix3D(rotation.Matrix))
        { }

        public ObjectPrototype(Shape3D shape, MaterialPrototype material, Vector position, Vector scale) :
            this(shape, material, position, scale, DongUtility.Rotation.Identity)
        { }
    }

}
