using System;
using System.Collections.Generic;
using Mogre;

namespace Sxta.Rti1516.BoidSample
{
    public abstract class Example
    {
        public static void ShowOgreException()
        {
            if (OgreException.IsThrown)
                System.Windows.Forms.MessageBox.Show(OgreException.LastException.FullDescription, "An exception has occured!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        }

        protected Root root;
        protected Camera camera;
        protected Viewport viewport;
        protected SceneManager sceneMgr;
        protected RenderWindow window;
        protected Overlay debugOverlay;
        protected MOIS.InputManager inputManager;
        protected MOIS.Keyboard inputKeyboard;
        protected MOIS.Mouse inputMouse;

        protected bool showDebugOverlay = true;
        protected float debugTextDelay = 0.0f;
        protected float toggleDelay = 1.0f;
        protected float statDelay = 0.0f;
        protected TextureFilterOptions filtering = TextureFilterOptions.TFO_BILINEAR;
        protected uint aniso = 1;
        protected float camSpeed = 100f;
        protected Degree rotateSpeed = 36;

        protected bool shutDown = false;
        protected string mDebugText;


        //Do NOT call root.Dispose at the finalizer thread because GL renderer requires that disposing of its objects is made
        //in the same thread as the thread that created the GL renderer.
        //~ExampleApplication()
        //{
        //    if (root != null)
        //    {
        //        root.Dispose();
        //        root = null;
        //    }
        //}

        public virtual void Go()
        {
            if (!Setup())
                return;

            root.StartRendering();

            // clean up
            DestroyScene();

            root.Dispose();
            root = null;
        }

        public virtual bool Setup()
        {
            root = new Root();

            SetupResources();

            bool carryOn = Configure();
            if (!carryOn) return false;

            ChooseSceneManager();
            CreateCamera();
            CreateViewports();

            // Set default mipmap level (NB some APIs ignore this)
            TextureManager.Singleton.DefaultNumMipmaps = 5;

            // Create any resource listeners (for loading screens)
            CreateResourceListener();
            // Load resources
            LoadResources();

            // Create the scene
            CreateScene();

            CreateFrameListener();

            CreateInput();

            return true;

        }

        /// <summary>
        /// Configures the application - returns false if the user chooses to abandon configuration.
        /// </summary>
        public virtual bool Configure()
        {
            // Show the configuration dialog and initialise the system
            // You can skip this and use root.restoreConfig() to load configuration
            // settings if you were sure there are valid ones saved in ogre.cfg
            if (root.ShowConfigDialog())
            {
                // If returned true, user clicked OK so initialise
                // Here we choose to let the system create a default rendering window by passing 'true'
                window = root.Initialise(true);
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual void ChooseSceneManager()
        {
            // Get the SceneManager, in this case a generic one
            sceneMgr = root.CreateSceneManager(SceneType.ST_GENERIC, "SceneMgr");
        }

        public virtual void CreateCamera()
        {
            // Create the camera
            camera = sceneMgr.CreateCamera("PlayerCam");

            // Position it at 500 in Z direction
            camera.Position = new Vector3(0, 0, 500);
            // Look back along -Z
            camera.LookAt(new Vector3(0, 0, -300));
            camera.NearClipDistance = 5;

        }

        public virtual void CreateFrameListener()
        {
            debugOverlay = OverlayManager.Singleton.GetByName("Core/DebugOverlay");
            debugOverlay.Show();
            root.FrameStarted += new FrameListener.FrameStartedHandler(ExampleApp_FrameStarted);
        }

        bool ExampleApp_FrameStarted(FrameEvent evt)
        {
            if (window.IsClosed)
                return false;

            HandleInput(evt);

            return !shutDown;
        }

        protected virtual void HandleInput(FrameEvent evt)
        {
			// Move about 100 units per second,
			float moveScale = camSpeed * evt.timeSinceLastFrame;
			// Take about 10 seconds for full rotation
			Degree rotScale = rotateSpeed * evt.timeSinceLastFrame;

	        Vector3 translateVector = Vector3.ZERO;

            
            // set the scaling of camera motion
            Degree scaleRotate = rotateSpeed * evt.timeSinceLastFrame;

            Vector3 camVelocity = Vector3.ZERO;

            inputKeyboard.Capture();

            if (inputKeyboard.IsKeyDown(MOIS.KeyCode.KC_ESCAPE))
            {
                // stop rendering loop
                shutDown = true;
            }

            if (inputKeyboard.IsKeyDown(MOIS.KeyCode.KC_A))
            {
                translateVector.x = -moveScale;
            }

            if (inputKeyboard.IsKeyDown(MOIS.KeyCode.KC_D))
            {
                translateVector.x = moveScale;
            }

            if (inputKeyboard.IsKeyDown(MOIS.KeyCode.KC_W))
            {
                translateVector.z = -moveScale;
            }

            if (inputKeyboard.IsKeyDown(MOIS.KeyCode.KC_S))
            {
                translateVector.z = moveScale;
            }

            if (inputKeyboard.IsKeyDown(MOIS.KeyCode.KC_LEFT))
            {
                camera.Yaw(scaleRotate);
            }

            if (inputKeyboard.IsKeyDown(MOIS.KeyCode.KC_RIGHT))
            {
                camera.Yaw(-scaleRotate);
            }

            if (inputKeyboard.IsKeyDown(MOIS.KeyCode.KC_UP))
            {
                camera.Pitch(scaleRotate);
            }

            if (inputKeyboard.IsKeyDown(MOIS.KeyCode.KC_DOWN))
            {
                camera.Pitch(-scaleRotate);
            }

            // subtract the time since last frame to delay specific key presses
            toggleDelay -= evt.timeSinceLastFrame;

            // toggle rendering mode
            if (inputKeyboard.IsKeyDown(MOIS.KeyCode.KC_R) && toggleDelay < 0)
            {
                if (camera.PolygonMode == PolygonMode.PM_POINTS)
                {
                    camera.PolygonMode = PolygonMode.PM_SOLID;
                }
                else if (camera.PolygonMode == PolygonMode.PM_SOLID)
                {
                    camera.PolygonMode = PolygonMode.PM_WIREFRAME;
                }
                else
                {
                    camera.PolygonMode = PolygonMode.PM_POINTS;
                }

                Console.WriteLine("Rendering mode changed to '{0}'.", camera.PolygonMode);

                toggleDelay = 1;
            }

            if (inputKeyboard.IsKeyDown(MOIS.KeyCode.KC_T) && toggleDelay < 0)
            {
                // toggle the texture settings
                switch (filtering)
                {
                    case TextureFilterOptions.TFO_BILINEAR:
                        filtering = TextureFilterOptions.TFO_TRILINEAR;
                        aniso = 1;
                        break;
                    case TextureFilterOptions.TFO_TRILINEAR:
                        filtering = TextureFilterOptions.TFO_ANISOTROPIC;
                        aniso = 8;
                        break;
                    case TextureFilterOptions.TFO_ANISOTROPIC:
                        filtering = TextureFilterOptions.TFO_BILINEAR;
                        aniso = 1;
                        break;
                }

                Console.WriteLine("Texture Filtering changed to '{0}'.", filtering);

                // set the new default
                MaterialManager.Singleton.SetDefaultTextureFiltering(filtering);
                MaterialManager.Singleton.DefaultAnisotropy = aniso;

                toggleDelay = 1;
            }

            if (inputKeyboard.IsKeyDown(MOIS.KeyCode.KC_SYSRQ))
            {
                string[] temp = System.IO.Directory.GetFiles(Environment.CurrentDirectory, "screenshot*.jpg");
                string fileName = string.Format("screenshot{0}.jpg", temp.Length + 1);

                TakeScreenshot(fileName);

                // show briefly on the screen
                mDebugText = string.Format("Wrote screenshot '{0}'.", fileName);

                // show for 2 seconds
                debugTextDelay = 2.0f;
            }

            if (inputKeyboard.IsKeyDown(MOIS.KeyCode.KC_B))
            {
                sceneMgr.ShowBoundingBoxes = !sceneMgr.ShowBoundingBoxes;
            }

            if (inputKeyboard.IsKeyDown(MOIS.KeyCode.KC_F))
            {
                // hide all overlays, includes ones besides the debug overlay
                viewport.OverlaysEnabled = !viewport.OverlaysEnabled;
            }

            inputMouse.Capture();
            MOIS.MouseState_NativePtr mouseState = inputMouse.MouseState;

            if (!mouseState.ButtonDown(MOIS.MouseButtonID.MB_Left))
            {
                Degree cameraYaw = -mouseState.X.rel * .13f;
                Degree cameraPitch = -mouseState.Y.rel * .13f;

                camera.Yaw(cameraYaw);
                camera.Pitch(cameraPitch);
            }
            else
            {
                translateVector.x += mouseState.X.rel * 0.13f;
            }


            // move the camera based on the accumulated movement vector
            camera.MoveRelative(translateVector);

            // update performance stats once per second
            if (statDelay < 0.0f && showDebugOverlay)
            {
                UpdateStats();
                statDelay = 1.0f;
            }
            else
            {
                statDelay -= evt.timeSinceLastFrame;
            }

            // turn off debug text when delay ends
            if (debugTextDelay < 0.0f)
            {
                debugTextDelay = 0.0f;
                mDebugText = "";
            }
            else if (debugTextDelay > 0.0f)
            {
                debugTextDelay -= evt.timeSinceLastFrame;
            }
        }

        protected void TakeScreenshot()
        {
            string[] temp = System.IO.Directory.GetFiles(Environment.CurrentDirectory, "screenshot*.jpg");
            string fileName = string.Format("screenshot{0}.jpg", temp.Length + 1);

            TakeScreenshot(fileName);
        }

        protected void TakeScreenshot(string fileName)
        {
            window.WriteContentsToFile(fileName);
        }

        protected void UpdateStats()
        {
            string currFps = "Current FPS: ";
            string avgFps = "Average FPS: ";
            string bestFps = "Best FPS: ";
            string worstFps = "Worst FPS: ";
            string tris = "Triangle Count: ";

            // update stats when necessary
            try
            {
                OverlayElement guiAvg = OverlayManager.Singleton.GetOverlayElement("Core/AverageFps");
                OverlayElement guiCurr = OverlayManager.Singleton.GetOverlayElement("Core/CurrFps");
                OverlayElement guiBest = OverlayManager.Singleton.GetOverlayElement("Core/BestFps");
                OverlayElement guiWorst = OverlayManager.Singleton.GetOverlayElement("Core/WorstFps");

                RenderTarget.FrameStats stats = window.GetStatistics();

                guiAvg.Caption = avgFps + stats.AvgFPS;
                guiCurr.Caption = currFps + stats.LastFPS;
                guiBest.Caption = bestFps + stats.BestFPS + " " + stats.BestFrameTime + " ms";
                guiWorst.Caption = worstFps + stats.WorstFPS + " " + stats.WorstFrameTime + " ms";

                OverlayElement guiTris = OverlayManager.Singleton.GetOverlayElement("Core/NumTris");
                guiTris.Caption = tris + stats.TriangleCount;

                OverlayElement guiDbg = OverlayManager.Singleton.GetOverlayElement("Core/DebugText");
                guiDbg.Caption = mDebugText;
            }
            catch
            {
                // ignore
            }
        }

        public virtual bool UseBufferedInput
        {
            get { return false; }
        }

        public virtual void CreateInput()
        {
            LogManager.Singleton.LogMessage("*** Initializing OIS ***");
            MOIS.ParamList pl = new MOIS.ParamList();
            IntPtr windowHnd;
            window.GetCustomAttribute("WINDOW", out windowHnd);
            pl.Insert("WINDOW", windowHnd.ToString());

            inputManager = MOIS.InputManager.CreateInputSystem(pl);

            //Create all devices (We only catch joystick exceptions here, as, most people have Key/Mouse)
            inputKeyboard = (MOIS.Keyboard)inputManager.CreateInputObject(MOIS.Type.OISKeyboard, UseBufferedInput);
            inputMouse = (MOIS.Mouse)inputManager.CreateInputObject(MOIS.Type.OISMouse, UseBufferedInput);
        }

        public abstract void CreateScene();    // pure virtual - this has to be overridden

        public virtual void DestroyScene() { }    // Optional to override this

        public virtual void CreateViewports()
        {
            // Create one viewport, entire window
            viewport = window.AddViewport(camera);
            viewport.BackgroundColour = new ColourValue(0, 0, 0);

            // Alter the camera aspect ratio to match the viewport
            camera.AspectRatio = ((float)viewport.ActualWidth) / ((float)viewport.ActualHeight);
        }

        /// Method which will define the source of resources (other than current folder)
        public virtual void SetupResources()
        {
            // Load resource paths from config file
            ConfigFile cf = new ConfigFile();
            cf.Load("resources.cfg", "\t:=", true);

            // Go through all sections & settings in the file
            ConfigFile.SectionIterator seci = cf.GetSectionIterator();

            String secName, typeName, archName;

            // Normally we would use the foreach syntax, which enumerates the values, but in this case we need CurrentKey too;
            while (seci.MoveNext())
            {
                secName = seci.CurrentKey;
                ConfigFile.SettingsMultiMap settings = seci.Current;
                foreach (KeyValuePair<string, string> pair in settings)
                {
                    typeName = pair.Key;
                    archName = pair.Value;
                    ResourceGroupManager.Singleton.AddResourceLocation(archName, typeName, secName);
                }
            }
        }

        /// Optional override method where you can create resource listeners (e.g. for loading screens)
        public virtual void CreateResourceListener()
        {

        }

        /// Optional override method where you can perform resource group loading
        /// Must at least do ResourceGroupManager.Singleton.InitialiseAllResourceGroups();
        public virtual void LoadResources()
        {
            // Initialise, parse scripts etc
            ResourceGroupManager.Singleton.InitialiseAllResourceGroups();
        }
    }
}
