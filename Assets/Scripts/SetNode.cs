using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SetNode : MonoBehaviour
{
    public bool detect;
    Mesh mesh;
    public float radius;

    public List<nodeTag> tagNode = new List<nodeTag>();

    public static SetNode _instance;

    public Collider[] hitColliders;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        DetectUnwalk();
    }

    private void Update()
    {
        if(detect)
        {
            detect = false;
            DetectUnwalk();
        }
    }

    public void DetectUnwalk()
    {
        PointGraph pg = AstarPath.active.graphs[0] as PointGraph;

        foreach (var node in pg.nodes)
        {
            NodeInfo(node, true);
        }
    }

    public void NodeInfo(PointNode node, bool clear)
    {
        node.Walkable = true;
        hitColliders = Physics.OverlapSphere((Vector3)node.position, radius);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            foreach (nodeTag info in tagNode)
            {
                if (hitColliders[i].CompareTag(info.tagHit))
                {
                    if (!clear)
                    {
                        Debug.Log((hitColliders[i].name));
                        Debug.Log(info.walkable);
                    }
                    node.Walkable = info.walkable;
                    node.Tag = info.layerNode;
                    if (hitColliders[i].GetComponent<NightEnvironnement>() && clear)
                    {
                        hitColliders[i].GetComponent<NightEnvironnement>().nodes.Add(node);
                    }
                }
            }
        }
    }

    [System.Serializable]
    public class nodeTag
    {
        public string tagHit;
        public bool walkable;
        public uint layerNode;
    }
}

