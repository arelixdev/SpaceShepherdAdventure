using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CreateNode : MonoBehaviour
{
    public bool spawn;
    public bool clear;
    Mesh mesh;
    public bool barycentre;

    void Update()
    {
        if(spawn)
        {
            spawn = false;
            ClearListEditor();

            mesh = GetComponent<MeshFilter>().sharedMesh;
            GameObject parentNode = new GameObject("Nodes");
            parentNode.transform.parent = transform;
            parentNode.transform.localScale = Vector3.one;
            parentNode.transform.position = Vector3.zero;

            for (int i = 0; i < mesh.vertices.Length; i++)
            {
                GameObject sphere = new GameObject("Node_" + i.ToString());
                sphere.transform.parent = parentNode.transform;
                sphere.transform.localPosition = mesh.vertices[i];
            }
            if (barycentre)
            {
                int[] triangles = mesh.triangles;
                for (int i = 0; i < triangles.Length / 3; i++)
                {
                    Vector3 p0 = mesh.vertices[triangles[i * 3 + 0]];
                    Vector3 p1 = mesh.vertices[triangles[i * 3 + 1]];
                    Vector3 p2 = mesh.vertices[triangles[i * 3 + 2]];

                    p0 = transform.TransformPoint(p0);
                    p1 = transform.TransformPoint(p1);
                    p2 = transform.TransformPoint(p2);
                    Vector3 center = ((p0 + p1 + p2) / 3);

                    GameObject sphere = new GameObject("Node_" + i.ToString() + "_Centre");
                    sphere.transform.parent = parentNode.transform;
                    sphere.transform.position = center;
                }
            }
            AstarPath.active.Scan();
        }
        if(clear)
        {
            clear = false;
            ClearListEditor();
        }
    }

    private void Start()
    {
        if (Application.isPlaying)
        {
            ClearList();
        }
    }

    public void ClearListEditor()
    {
        int childs = transform.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            if(transform.GetChild(i).name.Contains("Node"))
                DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
    public void ClearList()
    {
        int childs = transform.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            if (transform.GetChild(i).name.Contains("Node"))
                Destroy(transform.GetChild(i).gameObject);
        }
    }
}
