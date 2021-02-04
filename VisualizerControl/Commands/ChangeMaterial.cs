using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualizerControl.Commands
{
    public class ChangeMaterial : VisualizerCommand
    {
        private readonly int index;
        private readonly MaterialPrototype material;

        public ChangeMaterial(int index, MaterialPrototype materialPrototype)
        {
            this.index = index;
            material = materialPrototype;
        }
        public override void Do(Visualizer viz)
        {
            throw new NotImplementedException();
            //var obj = viz.RetrieveObject(index);
            //obj.GeoModel.Material = material.Material;
        }

        protected override void WriteContent(BinaryWriter bw)
        {
            material.WriteToFile(bw);
            bw.Write(index);
        }

        internal ChangeMaterial(BinaryReader br)
        {
            material = MaterialPrototype.ReadFromFile(br);
            index = br.ReadInt32();
        }
    }
}
