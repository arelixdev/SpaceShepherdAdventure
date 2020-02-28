using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NightEnvironnement : MonoBehaviour
{
    public enum Types { Sapin , Boo};
    public Types currentType;


    //Sapin
    [DrawIf("currentType", Types.Sapin)] public Transform parent;

    //Boo
    public List<PointNode> nodes = new List<PointNode>();
    [HideInInspector] public List<GameObject> sheeps;

    private void Start()
    {
        switch (currentType)
        {
            case Types.Sapin:
                transform.parent = parent;
                break;
            case Types.Boo:
                StartCoroutine(MoveTo(.5f, -transform.up, 5, "RefreshNode"));
                break;
        }
    }

    #region Collision
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("night"))
        {
            switch(currentType)
            {
                case Types.Sapin:
                    StartCoroutine(MoveTo(1, -transform.up, 5, "RefreshNode"));
                    break;
            }
        }
        if (other.CompareTag("mouton" +
            ""))
        {
            switch (currentType)
            {
                case Types.Boo:
                    if (!sheeps.Contains(other.gameObject))
                        sheeps.Add(other.gameObject);
                    if(sheeps.Count < 2)
                        StartCoroutine(MoveTo(.5f, transform.up, 5, "RefreshNode"));
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("night"))
        {
            switch (currentType)
            {
                case Types.Sapin:
                    StartCoroutine(MoveTo(.5f, transform.up, 5 , "RefreshNode"));
                    break;
            }
        }
        if (other.CompareTag("mouton"))
        {
            switch (currentType)
            {
                case Types.Boo:
                    sheeps.Remove(other.gameObject);
                    if (sheeps.Count < 1)
                    {
                        StartCoroutine(MoveTo(.5f, -transform.up, 5, "RefreshNode"));
                    }
                    break;
            }
        }
    }
    #endregion

    #region Movment
    IEnumerator MoveTo(float time, Vector3 dir, float DistMult ,string voidToCall)
    {
        Vector3 start = transform.position;
        Vector3 end = start + dir * DistMult;
        float t = 0;

        /*
        while (t < 1)
        {
            
            t += Time.deltaTime / time;
            transform.position = Vector3.Lerp(start, end, t);
        }*/
        transform.position = end;
        yield return new WaitForSeconds(0.1f);

        if (!string.IsNullOrEmpty(voidToCall))
        {
            Invoke(voidToCall, 0);
        }
    }

    public void RefreshNode()
    {
        foreach (var node in nodes)
        {
            Debug.Log((Vector3)node.position);
            SetNode._instance.NodeInfo(node, false);
        }
    }
    #endregion
}