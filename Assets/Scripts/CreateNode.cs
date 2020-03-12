using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CreateNode : MonoBehaviour
{
    [Tooltip("Detruit les nodes et les respawn")] public bool spawn;
    [Tooltip("Detruit les nodes")] public bool clear;
    [Tooltip("Permet de choisir de faire spawn les nodes au milieu des triangles")] public bool barycentre;
    [Tooltip("Permet de garder le script forcant les nodes")] public bool keepForce;
    Mesh mesh;
    List<nodeSettings> nf = new List<nodeSettings>();
    int index = 0;

    void Update()
    {
        if (spawn)
        {
            spawn = false;
            Spawn();
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
            //ClearList();
        }
    }

    public void ClearListEditor()
    {
        int childs = transform.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            if (transform.GetChild(i).name.Contains("Node"))
            {
                if (keepForce)
                {
                    for (int j = 0; j < transform.GetChild(i).childCount; j++)
                    {
                        Transform childOfChild = transform.GetChild(i).GetChild(j);
                        if (childOfChild.GetComponent<ForceNode>())
                        {
                            nodeSettings ns = new nodeSettings();
                            ns.index = j;
                            ns.walk = childOfChild.GetComponent<ForceNode>().walkable;
                            ns.layer = childOfChild.GetComponent<ForceNode>().layer;

                            nf.Add(ns);
                        }
                    }
                }
                else
                {
                    nf.Clear();
                }
                keepForce = false;
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
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

    public void Spawn()
    {
        index = 0;
        nf.Clear();
        ClearListEditor();

        mesh = GetComponent<MeshFilter>().sharedMesh;
        GameObject parentNode = new GameObject("Nodes");
        parentNode.transform.parent = transform;
        parentNode.transform.localScale = Vector3.one;
        parentNode.transform.localPosition = Vector3.zero;

        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            //GameObject sphere = new GameObject("Node_" + i.ToString());
            if (true)
            {
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.layer = LayerMask.NameToLayer("Node");
                sphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                sphere.name = "Node_" + i.ToString();
                sphere.transform.parent = parentNode.transform;
                sphere.transform.position = transform.TransformPoint(mesh.vertices[i]);
                if (nf.Count > 0)
                {
                    if (nf[index].index == i)
                    {
                        ForceNode force = sphere.AddComponent<ForceNode>();
                        force.walkable = nf[index].walk;
                        force.layer = nf[index].layer;
                        index = (index + 1) % nf.Count;
                    }
                }
            }
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

                //GameObject sphere = new GameObject("Node_" + i.ToString() + "_Centre");
                if (true)
                {
                    GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    sphere.layer = LayerMask.NameToLayer("Node");
                    sphere.name = "Node_" + i.ToString() + "_Centre";
                    sphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    sphere.transform.parent = parentNode.transform;
                    sphere.transform.position = center;
                    if (nf.Count > 0)
                    {
                        if (nf[index].index == i + mesh.vertices.Length)
                        {
                            ForceNode force = sphere.AddComponent<ForceNode>();
                            force.walkable = nf[index].walk;
                            force.layer = nf[index].layer;
                            index = (index + 1) % nf.Count;
                        }
                    }
                }
            }
        }
        AstarPath.active.Scan();
    }

    public class nodeSettings
    {
        public int index;
        public bool walk;
        public uint layer;
    }
}
