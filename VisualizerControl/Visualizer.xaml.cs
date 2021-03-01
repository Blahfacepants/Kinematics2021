using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VisualizerControl
{
    /// <summary>
    /// Interaction logic for ArenaVisualizerStandalone.xaml
    /// </summary>
    public partial class Visualizer : UserControl
    {
        public Visualizer()
        {
            InitializeComponent();

            Loaded += WhenLoaded;
            timer.Elapsed += Redraw;
            timer.AutoReset = true;
            timer.Start();
        }

        private void Redraw(object sender, ElapsedEventArgs e)
        {
            //if (Display != null && ShowVisual)
                //Display.Redraw();
        }

        private Application app;
        private IntPtr hwndListBox;
        private Window myWindow;
        internal Visualizer3DCoreInterface Display { get; set; } = null;

        private Timer timer = new Timer(33);

        private bool UIinitialized = false;

        private void OnUIReady(object sender, EventArgs e)
        {
            //var initial = TheArena.Initialization();
            if (!UIinitialized)
            {

                app = Application.Current;
                myWindow = app.MainWindow;
                //myWindow.SizeToContent = SizeToContent.WidthAndHeight;
                Display = new Visualizer3DCoreInterface(Visualizer3DCoreInterfaceHolder.ActualWidth,
                    Visualizer3DCoreInterfaceHolder.ActualHeight);
                Visualizer3DCoreInterfaceHolder.Child = Display;
                hwndListBox = Display.HwndListBox;

                // Add the particles that couldn't be added until initialization was done
                foreach (var pair in initialParticles)
                {
                    AddParticle(pair.Item1, pair.Item2);
                }
                foreach (var tuple in initialTransformation)
                {
                    TransformParticle(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4);
                }

                ShowVisual = true;
                UIinitialized = true;
            }
        }

        private void WhenLoaded(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            if (!initialized)
            {
                // Call it as a task, or else everything hangs here
                Task.Run(() => Visualizer3DCoreInterface.SetupDirectX());

                // Just for debugging
                //Visualizer3DCoreInterface.SetupDirectX();

                initialized = true;
            }
            InvalidateVisual();
        }
        private bool initialized = false;

        public bool ShowVisual { get; set; } = false;

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (Display != null && ShowVisual)
            {
                //Display.Redraw();
            }
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            //if (Display != null)
                //Display.ScaleDisplay((int)sizeInfo.NewSize.Width, (int)sizeInfo.NewSize.Height);
        }

        public class TempGetRidOfThis
        {
            public System.Windows.Media.Media3D.Point3D Position { get; set; }
        }

        public TempGetRidOfThis Camera { get; set; }

        private Dictionary<int, Object3D> particleDictionary = new Dictionary<int, Object3D>();

        private List<Tuple<Object3D, int>> initialParticles = new List<Tuple<Object3D, int>>();

        /// <summary>
        /// Adds a particle with a user-defined index for later manipulation
        /// </summary>
        public void AddParticle(Object3D part, int index)
        {
            // Store for later if Display isn't created yet
            if (Display == null)
            {
                initialParticles.Add(new Tuple<Object3D, int>(part, index));
            }
            else
            {
                Display.AddObject(part, index);
                particleDictionary.Add(index, part);
            }
        }

        /// <summary>
        /// Removes a particle with a given index
        /// </summary>
        public void RemoveParticle(int index)
        {

        }

        public void MoveParticle(int index, Vector3D newPosition)
        {
            Display.MoveObject(index, newPosition);
        }

        List<Tuple<int, Vector3D, Vector3D, Matrix3D>> initialTransformation = new List<Tuple<int, Vector3D, Vector3D, Matrix3D>>();
        public void TransformParticle(int index, Vector3D newPosition,
            Vector3D newScale, Matrix3D newRotation)
        {
            if (Display == null)
            {
                initialTransformation.Add(new Tuple<int, Vector3D, Vector3D, Matrix3D>(index, newPosition, newScale, newRotation));
            }
            else
            {
                Display.TransformObject(index, newScale, newRotation, newPosition);
            }
        }

        /// <summary>
        /// Clears all objects from the visualizer
        /// </summary>
        public void Clear()
        {

        }

        public bool AutoCamera
        {
            set
            {
                Display.SetAutoCamera(value);
            }
        }
    }
}
