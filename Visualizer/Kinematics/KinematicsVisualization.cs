﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using VisualizerBaseClasses;
using VisualizerControl;
using VisualizerControl.Commands;

namespace Visualizer.Kinematics
{
    /// <summary>
    /// A visualization for a kinematics engine
    /// </summary>
    class KinematicsVisualization : IVisualization
    {
        private readonly IEngine engine;
        private readonly List<IProjectile> projectiles;

        /// <summary>
        /// Keeps track of projectile indices
        /// </summary>
        private Dictionary<int, IProjectile> projectileMap = new Dictionary<int, IProjectile>();
        private int counter = 0;

        /// <summary>
        /// Keeps track of connector indices
        /// </summary>
        private Dictionary<int, Connector> connectorMap = new Dictionary<int, Connector>();

        public KinematicsVisualization(IEngine engine)
        {
            this.engine = engine;
            projectiles = engine.Projectiles;
        }

        public bool Continue { get; private set; } = true;
        public double Time => engine.Time;

        /// <summary>
        /// Determines the radius of the connector relative to the projectiles it connects.
        /// It is a scale factor for visualizing connectors.
        /// </summary>
        public double ConnectorRadiusScale { get; set; } = .2;

        /// <summary>
        /// Add a connector that is anchored to a fixed point and another projectile
        /// </summary>
        public void AddAnchoredConnector(int projectileIndex, Vector3D anchorPoint, Color color)
        {
            var projectile = projectiles[projectileIndex];
            double scale = ConnectorRadiusScale * projectile.Size;
            var connector = new AnchoredConnector(scale, color, anchorPoint, projectile);
            connectorMap.Add(counter, connector);
            ++counter;
        }

        /// <summary>
        /// Add a connector that is anchored to two projectiles
        /// </summary>
        public void AddTwoParticleConnector(int projectileIndex1, int projectileIndex2, Color color)
        {
            var proj1 = projectiles[projectileIndex1];
            var proj2 = projectiles[projectileIndex2];
            double scale = (proj1.Size + proj2.Size) / 2 * ConnectorRadiusScale;
            var connector = new TwoProjectileConnector(scale, color, proj1, proj2);
            connectorMap.Add(counter, connector);
            ++counter;
        }

        public CommandSet<VisualizerControl.Visualizer> Initialization()
        {
            var set = new VisualizerCommandSet();

            // Add all the projectiles
            foreach (var projectile in projectiles)
            {
                // Start it off in the right place
                var obj = new ObjectPrototype(projectile.Shape, new BasicMaterial(projectile.Color, .05, .3),
                    projectile.Position, new Vector3D(projectile.Size, projectile.Size, projectile.Size));

                set.AddCommand(new AddObject(obj, counter));
                projectileMap.Add(counter, projectile);
                ++counter;
            }

            // Add all the connectors
            foreach (var connector in connectorMap)
            {
                // Update them first so they are in the right position
                connector.Value.Update();
                var obj = new ObjectPrototype(new VisualizerControl.Shapes.CaplessCylinder3D(), 
                    new BasicMaterial(connector.Value.Color, .05, .3));
                set.AddCommand(new AddObject(obj, connector.Key));
                set.AddCommand(connector.Value.GetTransformCommand(connector.Key));
            }

            return set;
        }

        public CommandSet<VisualizerControl.Visualizer> Tick(double newTime)
        {
            Continue = engine.Tick(newTime);

            var set = new VisualizerCommandSet();

            // Change all the projectiles
            foreach (var entry in projectileMap)
            {
                set.AddCommand(new MoveObject(entry.Key, entry.Value.Position));
            }

            // Change all the connectors
            foreach (var connector in connectorMap)
            {
                connector.Value.Update();
                set.AddCommand(connector.Value.GetTransformCommand(connector.Key));
            }

            return set;
        }
    }
}
