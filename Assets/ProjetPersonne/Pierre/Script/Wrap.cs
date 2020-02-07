using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrap : MonoBehaviour
{
    /// <summary>
    /// ce script va déplacer les personnage hors de l'écran
    /// </summary>

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.position.x)>10f)
        {
            transform.position = new Vector3(-(transform.position.x*0.95f),transform.position.y) ;
        }
        if (Mathf.Abs(transform.position.y) > 5f)
        {
            transform.position = new Vector3(transform.position.x, (-transform.position.y * 0.95f));
        }
    }
}
