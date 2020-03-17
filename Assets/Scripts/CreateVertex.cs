using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class CreateVertex : MonoBehaviour
{
    public Vector3 scaling;
    public GameObject sapin;
    public GameObject node;
    public Transform parentObstacle;
    Mesh mesh;
    public float chanceSapin;
    public bool barycentre;
    public LayerMask layer;

    void Start()
    {
        parentObstacle.position = transform.position;
        parentObstacle.localScale = transform.localScale;
        parentObstacle.rotation = transform.rotation;

        mesh = GetComponent<MeshFilter>().mesh;
        //StartCoroutine(StartCheck());
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            float rd = Random.Range(0, 10);
            if (rd < chanceSapin)
            {
                GameObject sphere = Instantiate(sapin);
                sphere.name = i.ToString();
                sphere.transform.parent = parentObstacle;
                sphere.transform.localPosition = mesh.vertices[i];

                OrientBody(sphere.GetComponent<Rigidbody>(), FindSurface(sphere.transform));
            }
            else
            {
                GameObject sphere = Instantiate(node);
                sphere.name = i.ToString();
                sphere.transform.parent = transform;
                sphere.transform.localScale = scaling;
                sphere.transform.localPosition = mesh.vertices[i];
                //sphere.GetComponent<CreateGrid>().detect = true;
            }
        }
        if (barycentre)
        {
            int[] triangles = mesh.triangles;
            Debug.Log(triangles.Length);
            for (int i = 0; i < triangles.Length/3; i++)
            {
                Vector3 p0 = mesh.vertices[triangles[i * 3 + 0]];
                Vector3 p1 = mesh.vertices[triangles[i * 3 + 1]];
                Vector3 p2 = mesh.vertices[triangles[i * 3 + 2]];

                p0 = transform.TransformPoint(p0);
                p1 = transform.TransformPoint(p1);
                p2 = transform.TransformPoint(p2);
                Vector3 center = ((p0 + p1 + p2) / 3);

                GameObject sphere = Instantiate(node);
                sphere.name = i.ToString()+ "_Centre";
                sphere.transform.parent = transform;
                sphere.transform.localScale = scaling;
                sphere.transform.position = center;
                //sphere.GetComponent<CreateGrid>().detect = true;
            }
        }

        AstarPath.active.Scan();
    }

    private void Update()
    {
        Vector3 start = transform.TransformPoint(mesh.vertices[3]);
        Vector3 dir = transform.position - start;
        Debug.DrawRay(start - dir * .1f, dir, Color.red);
        //Debug.DrawLine(start - dir*.1f, start + dir * 3, Color.red);
    }

    Vector3 FindSurface(Transform tr)
    {
        Vector3 dir = transform.position - tr.position;
        Vector3 startVert = tr.position - dir * .1f;
        float distance = Vector3.Distance(transform.position, startVert);
        Vector3 surfaceNorm = Vector3.zero;

        RaycastHit hit;
        if (Physics.Raycast(startVert, dir, out hit, layer))
        {
            surfaceNorm = hit.normal;
        }
        return surfaceNorm;
    }

    void OrientBody(Rigidbody attractedBody, Vector3 surfaceNorm)
    {
        Quaternion targetRotation = Quaternion.FromToRotation(attractedBody.transform.up, surfaceNorm) * attractedBody.rotation;
        attractedBody.transform.localRotation = targetRotation;
    }
}
