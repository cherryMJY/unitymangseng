using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCreater : MonoBehaviour
{

    public Vector3 eulerAngles;

    [Header("斜坡尺寸")]
    public float sizeX;
    public float sizeY;
    public float sizeZ;

    [Header("斜坡后面的立方体尺寸")]
    public float planeSize;
    public float m_angle;
    public Material material;

    Vector3[] vertices = new Vector3[54];
    Vector2[] uvs = new Vector2[54];
    List<int> allTris = new List<int>();

    Vector3[] allPoint;

    GameObject gameObject;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DrawMesh(m_angle);
        }
    }

    public void DrawMesh(float angle)
    {
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
        sizeX = sizeY * Mathf.Tan(angle * Mathf.PI / 180);

        gameObject = new GameObject("Cube");
        gameObject.transform.eulerAngles = eulerAngles;

        var mf = gameObject.AddComponent<MeshFilter>();
        var mr = gameObject.AddComponent<MeshRenderer>();

        allPoint = new[]
        {
            //斜坡
             new Vector3(0, 0, 0),
             new Vector3(0, 0, sizeZ),
             new Vector3(sizeX, 0, sizeZ),
             new Vector3(sizeX, 0, 0),
             new Vector3(0, sizeY, 0),
             new Vector3(0, sizeY, sizeZ),

             //斜坡后的立方体
             new Vector3(0,-planeSize,0),
             new Vector3(sizeX,-planeSize,0),
             new Vector3(sizeX,-planeSize,sizeZ),
             new Vector3(0,-planeSize,sizeZ),
        };

        //斜坡
        vertices[0] = allPoint[0];  //正面
        vertices[1] = allPoint[4];
        vertices[2] = allPoint[3];


        vertices[3] = allPoint[0]; //底面1
        vertices[4] = allPoint[3];
        vertices[5] = allPoint[2];

        vertices[6] = allPoint[0];//底面1
        vertices[7] = allPoint[2];
        vertices[8] = allPoint[1];

        vertices[9] = allPoint[1];  //背面1
        vertices[10] = allPoint[4];
        vertices[11] = allPoint[0];

        vertices[12] = allPoint[4];//背面2
        vertices[13] = allPoint[1];
        vertices[14] = allPoint[5];

        vertices[15] = allPoint[5]; //反面
        vertices[16] = allPoint[1];
        vertices[17] = allPoint[2];

        vertices[18] = allPoint[2]; //斜坡1
        vertices[19] = allPoint[3];
        vertices[20] = allPoint[4];

        vertices[21] = allPoint[2]; //斜坡1
        vertices[22] = allPoint[4];
        vertices[23] = allPoint[5];


        //斜坡后的立方体
        vertices[24] = allPoint[6];
        vertices[25] = allPoint[0];
        vertices[26] = allPoint[3];

        vertices[27] = allPoint[6];
        vertices[28] = allPoint[3];
        vertices[29] = allPoint[7];

        vertices[30] = allPoint[6];
        vertices[31] = allPoint[9];
        vertices[32] = allPoint[0];

        vertices[33] = allPoint[0];
        vertices[34] = allPoint[9];
        vertices[35] = allPoint[1];

        vertices[36] = allPoint[7];
        vertices[37] = allPoint[3];
        vertices[38] = allPoint[8];

        vertices[39] = allPoint[8];
        vertices[40] = allPoint[3];
        vertices[41] = allPoint[2];

        vertices[42] = allPoint[7];
        vertices[43] = allPoint[8];
        vertices[44] = allPoint[9];

        vertices[45] = allPoint[7];
        vertices[46] = allPoint[9];
        vertices[47] = allPoint[6];

        vertices[48] = allPoint[1];
        vertices[49] = allPoint[9];
        vertices[50] = allPoint[8];

        vertices[51] = allPoint[1];
        vertices[52] = allPoint[8];
        vertices[53] = allPoint[2];


        for (int i = 0; i < vertices.Length; i++)
        {
            allTris.Add(i);
        }

        uvs[0] = new Vector2(0, 0);
        uvs[1] = new Vector2(0, 1);
        uvs[2] = new Vector2(1, 0);

        uvs[3] = new Vector2(0, 0);
        uvs[4] = new Vector2(1, 0);
        uvs[5] = new Vector2(1, 1);

        uvs[6] = new Vector2(0f, 0f);
        uvs[7] = new Vector2(1f, 1f);
        uvs[8] = new Vector2(0f, 1f);

        uvs[9] = new Vector2(0, 1);
        uvs[10] = new Vector2(1, 0);
        uvs[11] = new Vector2(0, 0);

        uvs[12] = new Vector2(1, 0);
        uvs[13] = new Vector2(0, 1);
        uvs[14] = new Vector2(1, 1);

        uvs[15] = new Vector2(0f, 1f);
        uvs[16] = new Vector2(0, 0);
        uvs[17] = new Vector2(1, 0);

        uvs[18] = new Vector2(1f, 1f);
        uvs[19] = new Vector2(1f, 0f);
        uvs[20] = new Vector2(0f, 0f);

        uvs[21] = new Vector2(1, 1);
        uvs[22] = new Vector2(0, 0);
        uvs[23] = new Vector2(0, 1);


        uvs[24] = new Vector2(1, 1);
        uvs[25] = new Vector2(0, 0);
        uvs[26] = new Vector2(0, 1);

        uvs[27] = new Vector2(1, 1);
        uvs[28] = new Vector2(0, 0);
        uvs[29] = new Vector2(0, 1);

        uvs[30] = new Vector2(1, 1);
        uvs[31] = new Vector2(0, 0);
        uvs[32] = new Vector2(0, 1);

        uvs[33] = new Vector2(1, 1);
        uvs[34] = new Vector2(0, 0);
        uvs[35] = new Vector2(0, 1);

        uvs[36] = new Vector2(1, 1);
        uvs[37] = new Vector2(0, 0);
        uvs[38] = new Vector2(0, 1);

        uvs[39] = new Vector2(1, 1);
        uvs[40] = new Vector2(0, 0);
        uvs[41] = new Vector2(0, 1);

        uvs[42] = new Vector2(1, 1);
        uvs[43] = new Vector2(0, 0);
        uvs[44] = new Vector2(0, 1);

        uvs[45] = new Vector2(1, 1);
        uvs[46] = new Vector2(0, 0);
        uvs[47] = new Vector2(0, 1);

        uvs[48] = new Vector2(1, 1);
        uvs[49] = new Vector2(0, 0);
        uvs[50] = new Vector2(0, 1);

        uvs[51] = new Vector2(1, 1);
        uvs[52] = new Vector2(0, 0);
        uvs[53] = new Vector2(0, 1);

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = allTris.ToArray();
        mr.material = material;
        mesh.RecalculateTangents();
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mf.mesh = mesh;
    }

    private void OnDrawGizmos()
    {
        if (allPoint != null)
        {
            for (int i = 0; i < allPoint.Length; i++)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(allPoint[i], 0.1f);
            }
        }
    }
}