using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SetNode : MonoBehaviour
{
    Transform nodeParent;
    public bool detect;
    Mesh mesh;
    public float radius;

    public List<nodeTag> tagNode = new List<nodeTag>();

    public static SetNode _instance;

    Collider[] hitColliders;

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
        nodeParent = transform.GetChild(0);
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
        for (int i = 1; i < pg.nodes.Length; i++)
        {
            if(nodeParent.GetChild(i - 1).GetComponent<ForceNode>())
            {
                pg.nodes[i].Walkable = nodeParent.GetChild(i - 1).GetComponent<ForceNode>().walkable;
                pg.nodes[i].Tag = nodeParent.GetChild(i - 1).GetComponent<ForceNode>().layer;
            }
            else
            {
                NodeInfo(pg.nodes[i], true);
            }
        }
        GetComponent<CreateNode>().ClearList();
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

