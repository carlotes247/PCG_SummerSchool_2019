using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneGenerator : MonoBehaviour
{
    public Vector3[] vertices;
    public Vector3[] normales;
    public Vector2[] uvs;
    public int[] triangles;

    MeshFilter filter;
    Mesh mesh;

    // Start is called before the first frame update
    void Start()
    {
        GeneratePlane();
    }

    // Update is called once per frame
    void Update()
    {
        mesh.vertices = vertices;
        mesh.normals = normales;
        mesh.uv = uvs;

        mesh.triangles = triangles;
        mesh.RecalculateBounds();
        mesh.Optimize();

    }

    public void GeneratePlane()
    {
        // You can change that line to provide another MeshFilter
        filter = gameObject.AddComponent<MeshFilter>();
        mesh = filter.mesh;
        mesh.Clear();

        float length = 1f;
        float width = 1f;
        int resX = 2; // 2 minimum
        int resZ = 2;

        #region Vertices		
        vertices = new Vector3[resX * resZ];
        for (int z = 0; z < resZ; z++)
        {
            // [ -length / 2, length / 2 ]
            float zPos = ((float)z / (resZ - 1) - .5f) * length;
            for (int x = 0; x < resX; x++)
            {
                // [ -width / 2, width / 2 ]
                float xPos = ((float)x / (resX - 1) - .5f) * width;
                vertices[x + z * resX] = new Vector3(xPos, 0f, zPos);
            }
        }
        #endregion

        #region Normales
        normales = new Vector3[vertices.Length];
        for (int n = 0; n < normales.Length; n++)
            normales[n] = Vector3.up;
        #endregion

        #region UVs		
        uvs = new Vector2[vertices.Length];
        for (int v = 0; v < resZ; v++)
        {
            for (int u = 0; u < resX; u++)
            {
                uvs[u + v * resX] = new Vector2((float)u / (resX - 1), (float)v / (resZ - 1));
            }
        }
        #endregion

        #region Triangles
        int nbFaces = (resX - 1) * (resZ - 1);
        triangles = new int[nbFaces * 6];
        int t = 0;
        for (int face = 0; face < nbFaces; face++)
        {
            // Retrieve lower left corner from face ind
            int i = face % (resX - 1) + (face / (resZ - 1) * resX);

            triangles[t++] = i + resX;
            triangles[t++] = i + 1;
            triangles[t++] = i;

            triangles[t++] = i + resX;
            triangles[t++] = i + resX + 1;
            triangles[t++] = i + 1;
        }
        #endregion

        mesh.vertices = vertices;
        mesh.normals = normales;
        mesh.uv = uvs;
        mesh.triangles = triangles;

        mesh.RecalculateBounds();
        mesh.Optimize();

        // Add a mesh renderer to see the results
        MeshRenderer renderer = this.gameObject.AddComponent<MeshRenderer>();
        // Add the default material from a cube in the scene
        renderer.material = GameObject.Find("CubeWithDefaultMat").GetComponent<Renderer>().sharedMaterial;

    }
}
