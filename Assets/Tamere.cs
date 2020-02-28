using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tamere : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int layerMask = ~LayerMask.GetMask("Node", "Ignore Raycast");
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, .3f, layerMask);
        foreach(Collider col in hitColliders)
        {
            Debug.Log(col.name);
        }
    }
}
