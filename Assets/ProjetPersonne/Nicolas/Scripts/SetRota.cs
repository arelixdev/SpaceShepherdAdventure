using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class SetRota : MonoBehaviour
{
    public float speed;
    public bool active;
    public Material[] mats;
    public bool resetMat;

    // Rajouter un tableau de materials
    void Update()
    {
        if (active && Application.isPlaying)
        {
            float rota = transform.eulerAngles.y + speed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, rota, 0);
        }
        foreach (Material mat in mats)
        {
            mat.SetFloat("Vector1_9B06EB91", -transform.eulerAngles.y);
        }
        if(resetMat)
        {
            resetMat = false;
            GetComponent<Renderer>().sharedMaterials = mats;
        }
    }
}
