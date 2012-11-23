namespace Sxta.Rti1516.BoidSample
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Mogre;

    using Sxta.Rti1516.Ambassadors;

    class BoidsSample : Example
    {
        BoidsManager boidsManager;
        protected List<SceneNode> boidNodesList;
        public string color;

        public BoidsSample(BoidsManager manager, string boidColor)
        {
            boidsManager = manager;
            color = boidColor;
        }

        int count = 0;
        public void AddNewBoid(Boid boid)
        {
            //return;
            try
            {
                SceneNode boidNode = base.sceneMgr.RootSceneNode.CreateChildSceneNode("Node" + count);
                //Entity boidEntity = base.sceneMgr.CreateEntity("Boid"+count, "myBoid_" + boid.Color);
                //boidNode.AttachObject(boidEntity);
                ManualObject boidObj = BoidMesh.CreateBoidObject("Boid" + count, base.sceneMgr, boid.Color);
                boidNode.AttachObject(boidObj);
                boidNodesList.Add(boidNode);
                MoveBoid(boidNode, boid);
                count++;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public override void ChooseSceneManager()
        {
            // Get the SceneManager, in this case a generic one
            sceneMgr = root.CreateSceneManager(SceneType.ST_EXTERIOR_CLOSE, "SceneMgr");
        }

        public override void CreateScene()
        {
            ColourValue fadeColour = new ColourValue(0.9f, 0.9f, 0.9f);
            base.sceneMgr.SetFog(FogMode.FOG_LINEAR, fadeColour, 0, 100, 900);
            //base.sceneMgr.SetFog(FogMode.FOG_EXP, fadeColour, 0.009f);
            base.viewport.BackgroundColour = fadeColour;

            Plane plane;
            plane.d = 100;
            plane.normal = Vector3.NEGATIVE_UNIT_Y;

            base.sceneMgr.SetWorldGeometry("terrain.cfg");
            base.sceneMgr.SetSkyPlane(true, plane, "Examples/CloudySky", 500, 20, true, 0.5f, 150, 150);

            //FSLSoundObject backgroundSound = soundManager.CreateAmbientSound("../../media/sound/windy1.ogg", "Ambiente1", true); //Create Ambient sound
            //backgroundSound.Play();

            //BoidMesh.CreateBoidMesh("myBoid_green", "green");
            //BoidMesh.CreateBoidMesh("myBoid_red", "red");

            boidNodesList = new List<SceneNode>();
            for (int i = 0; i < boidsManager.KnownBoidsList.Count; i++)
            {
                AddNewBoid(boidsManager.KnownBoidsList[i]);
            }
            boidsManager.OnNewBoid += new AddNewBoid(AddNewBoid);
        }

        public override void DestroyScene()
        {
            base.DestroyScene();
        }

        public override void CreateCamera()
        {
            // Create the camera
            camera = sceneMgr.CreateCamera("PlayerCam");

            // Position it at 500 in Z direction
            camera.Position = new Vector3(750, 60, 750);
            // Look back along -Z
            camera.LookAt(new Vector3(-1, 1, 0));
            camera.NearClipDistance = 0.1f;

            //soundManager = FSLSoundManager.Instance;
            //soundManager.InitializeSound(base.camera); //Init sound system

        }

        public override void CreateFrameListener()
        {
            base.CreateFrameListener();

            root.FrameStarted += new FrameListener.FrameStartedHandler(FrameStartedHandler);
        }

        bool FrameStartedHandler(FrameEvent evt)
        {
            // call AnimateBoid on every boid
            for (int index = 0; index < boidNodesList.Count; index++)
            {
                MoveBoid(boidNodesList[index], boidsManager.KnownBoidsList[index]);
            }
            return true;
        }

        /// <summary>
        /// Boid movement is a rotation and a translation, which use
        /// the current boid velocity and position.
        /// </summary>
        private void MoveBoid(SceneNode boidNode, Boid b)
        {
            boidNode.Position = b.Position;
            Vector3 direction = b.Velocity;
            direction.Normalise();
            // Test for opposite vectors
            float d = 1.0f + Vector3.UNIT_Z.DotProduct(direction);
            if (System.Math.Abs(d) < 0.00001)
            {
                // Diametrically opposed vectors
                Quaternion orientation = new Quaternion();
                orientation.FromAxes(Vector3.NEGATIVE_UNIT_X,
                                     Vector3.UNIT_Y,
                                     Vector3.NEGATIVE_UNIT_Z);
                boidNode.Orientation = orientation;
            }
            else
            {
                boidNode.Orientation =
                    Vector3.UNIT_Z.GetRotationTo(direction);
            }
        }
    }
}