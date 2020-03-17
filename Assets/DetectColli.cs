using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectColli : MonoBehaviour
{
    public bool triggerCol;
    private void OnTriggerEnter(Collider other)
    {
        triggerCol = true;
    }
}
