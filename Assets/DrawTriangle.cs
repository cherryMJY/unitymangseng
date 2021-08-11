/*************************************************************************
 *  FileName: DrawTriangle.cs
 *  Author: LaiZhangYin(Eagle)   Version: 1.0   Date: 6/21/2017
 * if you have some question, please call 
 *  QQ/Wechat : 782966734
 *************************************************************************/

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class DrawTriangle : MonoBehaviour
{
    public float length = 6;
    public float width = 6;
    public float height = 6;

    private MeshFilter meshFilter;
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = CreateTriangle(length, width, height);
        if (this.gameObject.AddComponent<MeshCollider>() == null)
            this.gameObject.AddComponent<MeshCollider>();
        this.GetComponent<MeshRenderer>().enabled = true;
    }

    private Mesh CreateTriangle(float length, float width, float height)
    {
        int verticesCount = 4 * 6;//3* 4;
        Vector3[] vertices = new Vector3[verticesCount];

        vertices[0] = Vector3.zero;                 //前面左下角的点
        vertices[1] = new Vector3(0, height, 0);    //前面左上角的点
        vertices[2] = new Vector3(length, 0, 0);   // 前面右下角的点    
        vertices[3] = new Vector3(length, height, 0);//前面右上角的点

        vertices[4] = Vector3.zero;//new Vector3(length, 0, width);        //后面右下角的点
        vertices[5] = Vector3.zero;//new Vector3(length, height, width);    //后面右上角的点
        vertices[6] = new Vector3(0, 0, width);             //后面左下角的点
        vertices[7] = new Vector3(0, height, width);        //后面左上角的点

        vertices[8] = vertices[6];                              //左
        vertices[9] = vertices[7];
        vertices[10] = vertices[0];
        vertices[11] = vertices[1];

        vertices[12] = vertices[2];                              //右
        vertices[13] = vertices[3];
        vertices[14] = vertices[6];
        vertices[15] = vertices[7];

        vertices[16] = vertices[1];                              //上
        vertices[17] = vertices[7];
        vertices[18] = vertices[3];
        vertices[19] = vertices[7];

        vertices[20] = vertices[2];                              //下
        vertices[21] = vertices[6];
        vertices[22] = vertices[0];
        vertices[23] = vertices[6];


        int triangleCount = 6 * 2 * 3;
        int[] triangles = new int[triangleCount];
        for (int i = 0, v = 0; i < triangleCount; i += 6, v += 4)
        {
            triangles[i] = v;
            triangles[i + 1] = v + 1;
            triangles[i + 2] = v + 2;
            triangles[i + 3] = v + 3;
            triangles[i + 4] = v + 2;
            triangles[i + 5] = v + 1;
        }
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        return mesh;
    }
#if UNITY_EDITOR
    //用于在编辑状态设置大小
    void OnDrawGizmos()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = CreateTriangle(length, width, height);
    }
#endif
}