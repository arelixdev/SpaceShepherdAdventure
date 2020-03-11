using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestRota : MonoBehaviour
{
    public float angleWanted;

    void Start()
    {
        Quaternion rotationX = Quaternion.AngleAxis(transform.eulerAngles.x, transform.right);
        Quaternion rotationY = Quaternion.AngleAxis(angleWanted, transform.up);
        Quaternion rotationZ = Quaternion.AngleAxis(transform.eulerAngles.z, transform.forward);

        //transform.rotation = rotationY * rotationX * rotationZ;
        transform.rotation = rotationY * transform.rotation;
    }
}
