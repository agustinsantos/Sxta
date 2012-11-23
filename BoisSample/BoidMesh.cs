namespace Sxta.Rti1516.BoidSample
{
    using System;
    using Mogre;
    //using Mogre.Helpers;

    public class BoidMesh
    {
        // the shape's coordinates
        static Vector3[] pts = new Vector3[]{new Vector3(0.0f, 0.0f, 0.25f),
                                      new Vector3(0.2f, 0.0f, -0.25f),
                                      new Vector3(-0.2f, 0.0f, -0.25f),
                                      new Vector3(0.0f, 0.25f, -0.2f)};

        static Vector3[] baseColors = new Vector3[]{new Vector3(0.0f, 1.0f, 0.0f),
                                      new Vector3(1.0f, 1.0f, 0.0f),
                                      new Vector3(1.0f, 1.0f, 0.0f),
                                      new Vector3(0.0f, 0.0f, 1.0f)};


        // anti-clockwise face definition
        static int[] indices = { 2, 0, 3,      // left face
                          2, 1, 0,      // bottom face
                          0, 1, 3,      // right face
                          1, 2, 3  };   // back face
/*
        static public void CreateBoidMesh(string name, string color)
        {
            Vector3[] colors = baseColors;
            if (color.Equals("green"))
            {
                colors[0] = colors[1] = colors[2] = new Vector3(0.0f, 1.0f, 0.0f);
            }
            else if (color.Equals("red"))
            {
                colors[0] = colors[1] = colors[2] = new Vector3(1.0f, 0.0f, 0.0f);
            }

            // Return now if already exists
            if (MeshManager.Singleton.ResourceExists(name))
                return;

            MeshBuilderHelper mbh = new MeshBuilderHelper(name, "Flocking", false, 0, (uint)pts.Length);

            UInt32 offPos = mbh.AddElement(VertexElementType.VET_FLOAT3, VertexElementSemantic.VES_POSITION).Offset;
            UInt32 offDiff = mbh.AddElement(VertexElementType.VET_FLOAT3, VertexElementSemantic.VES_DIFFUSE).Offset;

            mbh.CreateVertexBuffer(HardwareBuffer.Usage.HBU_STATIC_WRITE_ONLY);
            for (int count = 0; count < pts.Length; count++)
            {
                mbh.SetVertFloat((uint)count, offPos, pts[count].x, pts[count].y, pts[count].z);           //position
                mbh.SetVertFloat((uint)count, offDiff, colors[count].x, colors[count].y, colors[count].z); //color
            }

            mbh.CreateIndexBuffer((uint)(indices.Length / 3), HardwareIndexBuffer.IndexType.IT_16BIT, HardwareBuffer.Usage.HBU_STATIC_WRITE_ONLY);
            for (uint index = 0; index < indices.Length / 3; index++)
            {
                mbh.SetIndex16bit(index, (UInt16)(indices[index * 3]),
                                         (UInt16)(indices[index * 3 + 1]),
                                         (UInt16)(indices[index * 3 + 2]));
            }

            MaterialPtr material = MaterialManager.Singleton.CreateOrRetrieve("Test/ColourTest", ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME).first;
            material.GetTechnique(0).GetPass(0).VertexColourTracking = (int)TrackVertexColourEnum.TVC_AMBIENT;
            MeshPtr m = mbh.Load("Test/ColourTest");
            m._setBounds(new AxisAlignedBox(-1.0f, -1.0f, -1.0f,
                                             1.0f, 1.0f, 1.0f), false);
            m._setBoundingSphereRadius((float)System.Math.Sqrt(1.0f * 1.0f + 1.0f * 1.0f));
        }
        */

        public static ManualObject CreateBoidObject(string name, SceneManager sceneMgr, string color)
        {
            Vector3[] colors = baseColors;
            if (color.Equals("green"))
            {
                colors[0] = colors[1] = colors[2] = new Vector3(0.0f, 1.0f, 0.0f);
            }
            else if (color.Equals("red"))
            {
                colors[0] = colors[1] = colors[2] = new Vector3(1.0f, 0.0f, 0.0f);
            }

            // Return now if already exists
            if (MeshManager.Singleton.ResourceExists(name))
                return null;

            ManualObject boidObj = sceneMgr.CreateManualObject(name);
            RenderOperation.OperationTypes operation = RenderOperation.OperationTypes.OT_TRIANGLE_LIST;
            MaterialPtr material = MaterialManager.Singleton.CreateOrRetrieve("Test/Boid", ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME).first;
            material.GetTechnique(0).GetPass(0).VertexColourTracking = (int)TrackVertexColourEnum.TVC_AMBIENT;

            boidObj.Begin("Test/Boid", operation);

            for (int count = 0; count < pts.Length; count++)
            {
                boidObj.Position(pts[count]);                                      //position
                boidObj.Colour(colors[count].x, colors[count].y, colors[count].z); //color
            }
            for (uint index = 0; index < indices.Length / 3; index++)
            {
                boidObj.Index((ushort)(indices[index * 3]));
                boidObj.Index((ushort)(indices[index * 3 + 1]));
                boidObj.Index((ushort)(indices[index * 3 + 2]));
            }

            boidObj.End();

            return boidObj;
        }

    }
}
