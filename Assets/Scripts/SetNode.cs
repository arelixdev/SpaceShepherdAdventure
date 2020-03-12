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

    int index;

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
        nodeParent = transform.GetChild(0);

        PointGraph pg = AstarPath.active.graphs[0] as PointGraph;
        for (int i = 1; i < pg.nodes.Length; i++)
        {
            Debug.Log(nodeParent.GetChild(i - 1).name);
            if (nodeParent.GetChild(i - 1).GetComponent<ForceNode>())
            {
                Debug.Log(nodeParent.GetChild(i).name);
                if (!nodeParent.GetChild(i - 1).GetComponent<ForceNode>().breakConnections)
                {
                    pg.nodes[i].Walkable = nodeParent.GetChild(i - 1).GetComponent<ForceNode>().walkable;
                    pg.nodes[i].Tag = nodeParent.GetChild(i - 1).GetComponent<ForceNode>().layer;
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
            /*
            if(i == nodeParent.childCount)
            {
                Debug.Log("Test");
                index++;
            }*/
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

