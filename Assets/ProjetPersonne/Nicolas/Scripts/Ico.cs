﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ico : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(GetComponent<MeshFilter>().mesh.vertices.Length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
