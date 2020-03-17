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

    int index, indexChild;

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
        index = 0;
        indexChild = 0;
        nodeParent = transform.GetChild(index);

        PointGraph pg = AstarPath.active.graphs[0] as PointGraph;
        for (int i = 1; i < pg.nodes.Length; i++)
        {
            if (nodeParent.GetChild(indexChild).GetComponent<ForceNode>())
            {
                if (!nodeParent.GetChild(indexChild).GetComponent<ForceNode>().breakConnections)
                {
                    pg.nodes[i].Walkable = nodeParent.GetChild(indexChild).GetComponent<ForceNode>().walkable;
                    pg.nodes[i].Tag = nodeParent.GetChild(indexChild).GetComponent<ForceNode>().layer;
                }
                else
                {
                    pg.nodes[i].ClearConnections(true);
                }
            }
            else
            {
                NodeInfo(pg.nodes[i], true);
            }
            indexChild = (indexChild + 1) % nodeParent.childCount;

            if (i == nodeParent.childCount)
            {
                indexChild = 0;
                index = (index + 1) % transform.childCount;
                nodeParent = transform.GetChild(index);
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

