using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateManualObject : MonoBehaviour
{
    MeshRenderer mr;
    MeshFilter mf;
    Mesh m;
    public Material mat;

    // Start is called before the first frame update
    void Start()
    {
        m = new Mesh();
        Vector3[] vertices = new Vector3[3];
        vertices[0] = new Vector3(0, 0, 0);
        vertices[1] = new Vector3(0, 1, 0);
        vertices[2] = new Vector3(1, 0, 0);
        m.SetVertices(vertices);
        int[] indices = new int[3] { 2, 1, 0 };

        m.SetIndices(indices, MeshTopology.Triangles, 0);
        mf = gameObject.AddComponent<MeshFilter>();
        mf.mesh = m;
        mr = gameObject.AddComponent<MeshRenderer>();
        mr.material = mat;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
