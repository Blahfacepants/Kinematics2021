using System;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Diagnostics;
using DongUtility;
using System.Collections.Generic;
using System.IO;
using VisualizerBaseClasses;
using System.Windows.Media.Media3D;
using System.Windows.Media;

namespace VisualizerControl
{
    public class Visualizer3DCoreInterface : HwndHost
    {
        internal const int
                    WsChild = 0x40000000,
                    WsVisible = 0x10000000,
                    LbsNotify = 0x00000001,
                    HostId = 0x00000002,
                    ListboxId = 0x00000001,
                    WsVscroll = 0x00200000,
                    WsBorder = 0x00800000;

        public int HostHeight { get; set; }
        public int HostWidth { get; set; }
        private IntPtr hwndHost;

        public Visualizer3DCoreInterface()
        { }

        public Visualizer3DCoreInterface(double windowWidth, double windowHeight)
        {
            SetWindowDimensions(windowWidth, windowHeight);
        }

        public IntPtr HwndListBox { get; private set; }

        protected override HandleRef BuildWindowCore(HandleRef hwndParent)
        {


            HwndListBox = IntPtr.Zero;
            hwndHost = IntPtr.Zero;

            string windowName = "internalWindow";
            //string curDir = Directory.GetCurrentDirectory();
            RegisterWindow(windowName);

            double fourKScaleFactor = 1;
            /*
             * On 4k screens you have to have UI scaling in order for things to not look super tiny.
             * Unfortunately it seems that the internal win32 apps don't get automatically scaled while the wpf does so we have to
             * manually scale here.
             */

            double debugInternalScale = 1;

            hwndHost = CreateWindowEx(0, "static", "",
                WsChild | WsVisible,
                0, 0,
                (int)(HostHeight * fourKScaleFactor), (int)(HostWidth * fourKScaleFactor),
                hwndParent.Handle,
                (IntPtr)HostId,
                IntPtr.Zero,
                0);

            HwndListBox = MakeWindow(windowName,
                WsChild | WsVisible | LbsNotify | WsBorder,
                (int)(HostHeight * fourKScaleFactor * debugInternalScale),
                (int)(HostWidth * fourKScaleFactor * debugInternalScale),
                hwndHost);

            return new HandleRef(this, hwndHost);
        }

        protected override IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            handled = false;
            return IntPtr.Zero;
        }

        protected override void DestroyWindowCore(HandleRef hwnd)
        {
            DestroyWindow(hwnd.Handle);
        }

        private Dictionary<string, int> shapeCodes = new Dictionary<string, int>();
        private Dictionary<Color, int> materialCodes = new Dictionary<Color, int>();
        private Dictionary<int, int> externalIndexToInternalIndex = new Dictionary<int, int>();

        internal void AddObject(Object3D obj, int externalIndex)
        {
            // Only add the shape if it is not currently in the dictionary
            var shape = obj.Shape;
            if (!shapeCodes.ContainsKey(shape.ShapeName))
            {
                AddShape(shape);
                // So each shape is added only once
                shapeCodes.Add(shape.ShapeName, shapeCodes.Count);
            }

            var material = obj.MaterialPrototype as BasicMaterial;
            if (material == null)
            {
                throw new NotImplementedException("Haven't gotten to non-basic materials yet");
            }

            if (!materialCodes.ContainsKey(material.Color))
            {
                AddMaterial(material);
                materialCodes.Add(material.Color, materialCodes.Count);
            }

            float[] position = ConvertVector(obj.Position);
            float[] scale = ConvertVector(obj.Scale);
            int internalIndex = AddObjectX(position, scale, shape.ShapeName, material.Color.ToString());
            externalIndexToInternalIndex.Add(externalIndex, internalIndex);
        }

        private float[] ConvertVector(Vector3D vec)
        {
            float[] response = new float[3];
            response[0] = (float)vec.X;
            response[1] = (float)vec.Y;
            response[2] = (float)vec.Z;
            return response;
        }

        private void AddMaterial(BasicMaterial material)
        {
            var color = material.Color;
            string name = color.ToString();
            float r = (float)color.R / 255;
            float g = (float)color.G / 255;
            float b = (float)color.B / 255;
            AddMaterialX(name, r, g, b, 0.05f, 0.3f);
        }

        private void AddShape(Shapes.Shape3D shape)
        {
            var mesh = shape.Mesh;
            int nVertices = mesh.Positions.Count;
            // Vectors are packed into flat arrays
            // So there are 3 * nVertices points in vertices[] and normals[]
            int size = nVertices * 3;
            float[] vertices = new float[size];
            float[] normals = new float[size];
            for (int i = 0; i < nVertices; ++i)
            {
                var vertex = mesh.Positions[i];
                int index = i * 3;
                vertices[index] = (float)(vertex.X);
                vertices[index + 1] = (float)(vertex.Y);
                vertices[index + 2] = (float)(vertex.Z);
                var normal = mesh.Normals[i];
                normals[index] = (float)(normal.X);
                normals[index + 1] = (float)(normal.Y);
                normals[index + 2] = (float)(normal.Z);
            }

            int nTriangleIndices = mesh.TriangleIndices.Count;
            UInt32[] triangles = new UInt32[nTriangleIndices];
            for (int i = 0; i < nTriangleIndices; ++i)
            {
                triangles[i] = (UInt32)(mesh.TriangleIndices[i]);
            }

            AddShapeX(shape.ShapeName, nVertices, vertices, normals, nTriangleIndices,
                triangles);
        }

        internal void MoveObject(int externalIndex, Vector3D newPosition)
        {
            int internalIndex = externalIndexToInternalIndex[externalIndex];
            MoveObjectX(internalIndex, ConvertVector(newPosition));
        }

        private const string dllName = @"..\..\..\..\VisualizerControl\Visualizer3DCore.dll";
        //private const string dllName = @"C:\Users\pdong\Documents\Visual Studio Repositories\Computational Science Spring 2021\VisualizerControl\Visualizer3DCore.dll";

        [DllImport(dllName, EntryPoint = "RegisterWindow", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool RegisterWindow(string ClassName);

        [DllImport(dllName, EntryPoint = "MakeWindow", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr MakeWindow(string ClassName, int style, int height, int width, IntPtr parent);

        [DllImport(dllName, EntryPoint = "SetupDirectX", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SetupDirectX();

        [DllImport(dllName, EntryPoint = "AddShape", CallingConvention = CallingConvention.Cdecl)]
        private static extern void AddShapeX(string name, int nVertices, float[] vertices,
        float[] normals, int nTriangleIndices, UInt32[] triangles);

        [DllImport(dllName, EntryPoint = "AddMaterial", CallingConvention = CallingConvention.Cdecl)]
        private static extern void AddMaterialX(string name, float colorR, float colorG,
            float colorB, float fresnel, float roughness);

        [DllImport(dllName, EntryPoint = "AddObject", CallingConvention = CallingConvention.Cdecl)]
        private static extern int AddObjectX(float[] position, float[] scale,
            string shape, string material);

        [DllImport(dllName, EntryPoint = "MoveObject", CallingConvention = CallingConvention.Cdecl)]
        private static extern int MoveObjectX(int index, float[] newPosition);


        [DllImport("user32.dll", EntryPoint = "CreateWindowEx", CharSet = CharSet.Unicode)]
        private static extern IntPtr CreateWindowEx(int dwExStyle,
            string lpszClassName,
            string lpszWindowName,
            int style,
            int x, int y,
            int width, int height,
            IntPtr hwndParent,
            IntPtr hMenu,
            IntPtr hInst,
            [MarshalAs(UnmanagedType.AsAny)] object pvParam);

        [DllImport("user32.dll", EntryPoint = "DestroyWindow", CharSet = CharSet.Unicode)]
        private static extern bool DestroyWindow(IntPtr hwnd);

        public void SetWindowDimensions(double windowWidth, double windowHeight)
        {
            HostHeight = (int)windowHeight;
            HostWidth = (int)windowWidth;
        }
    }
}
