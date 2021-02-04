using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using WPFUtility;

namespace VisualizerControl
{
    abstract public class MaterialPrototype
    {
        private enum MaterialType : byte { Basic, Texture };

        public void WriteToFile(BinaryWriter bw)
        {
            if (this is BasicMaterial)
                bw.Write((byte)MaterialType.Basic);
            else if (this is TextureMaterial)
                bw.Write((byte)MaterialType.Texture);
            else
                throw new NotImplementedException();

            WriteContent(bw);
        }

        static public MaterialPrototype ReadFromFile(BinaryReader br)
        {
            byte typeCode = br.ReadByte();
            MaterialType type = (MaterialType)typeCode;
            switch (type)
            {
                case MaterialType.Basic:
                    Color color = br.ReadColor();
                    bool specular = br.ReadBoolean();
                    return new BasicMaterial(color, specular);

                case MaterialType.Texture:
                    string filename = br.ReadString();
                    return new TextureMaterial(filename);

                default:
                    throw new NotImplementedException();
            }
        }

        abstract public Material Material { get; }

        abstract protected void WriteContent(BinaryWriter bw);
    }
}
