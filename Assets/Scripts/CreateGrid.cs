using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGrid : MonoBehaviour
{
    public bool detect;

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("obstacle") && detect)
        {
            Destroy(gameObject);
        }
    }
}
