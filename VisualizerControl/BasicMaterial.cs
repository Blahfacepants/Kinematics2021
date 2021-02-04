using System.Collections.Generic;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using WPFUtility;

namespace VisualizerControl
{
    public class BasicMaterial : MaterialPrototype
    {
        public Color Color { get; }
        private readonly bool specular;

        public BasicMaterial(Color color, bool specular = false)
        {
            this.Color = color;
            this.specular = specular;
        }

        private const double specularCoefficient = 1;
        static private Dictionary<Color, Brush> brushes = new Dictionary<Color, Brush>();
        static private Dictionary<Color, DiffuseMaterial> diffMaterials = new Dictionary<Color, DiffuseMaterial>();
        static private Dictionary<Color, SpecularMaterial> specMaterials = new Dictionary<Color, SpecularMaterial>();

        /// <summary>
        /// Creates a new material if one does not exist, or otherwise returns it from a dictionary
        /// </summary>
        public override Material Material
        {
            get
            {
                if (!brushes.ContainsKey(Color))
                {
                    var newBrush = new SolidColorBrush(Color);
                    newBrush.Freeze();
                    brushes[Color] = newBrush;
                }
                var brush = brushes[Color];

                if (specular)
                {
                    if (!specMaterials.ContainsKey(Color))
                    {
                        var newMaterial = new SpecularMaterial(brush, specularCoefficient);
                        newMaterial.Freeze();
                        specMaterials[Color] = newMaterial;
                    }
                    return specMaterials[Color];
                }
                else
                {
                    if (!diffMaterials.ContainsKey(Color))
                    {
                        var newMaterial = new DiffuseMaterial(brush);
                        newMaterial.Freeze();
                        diffMaterials[Color] = newMaterial;
                    }
                    return diffMaterials[Color];
                }
            }
        }

        protected override void WriteContent(BinaryWriter bw)
        {
            bw.Write(Color);
            bw.Write(specular);
        }
    }
}
