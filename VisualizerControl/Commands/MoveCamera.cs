using DongUtility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Media.Media3D;
using WPFUtility;
using static WPFUtility.UtilityFunctions;

namespace VisualizerControl.Commands
{
    public class MoveCamera : VisualizerCommand
    {
        private Vector position;
        public MoveCamera(Vector position)
        {
            this.position = position;
        }
        public override void Do(Visualizer viz)
        {
            viz.Camera.Position = ConvertToPoint3D(ConvertVector(position));
        }

        protected override void WriteContent(BinaryWriter bw)
        {
            bw.Write(position);
        }

        internal MoveCamera(BinaryReader br)
        {
            position = br.ReadVector();
        }

    }
}
