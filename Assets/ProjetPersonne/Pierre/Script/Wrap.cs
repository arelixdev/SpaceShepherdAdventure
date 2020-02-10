using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrap : MonoBehaviour
{
    /// <summary>
    /// ce script va déplacer les personnage hors de l'écran
    /// </summary>

    public Creature thiscreature;

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.position.x)>11f)
        {
            transform.position = new Vector3(-(transform.position.x*0.95f), 0.5f, transform.position.z) ;
            thiscreature.deplacementCreature.destination= transform.position = new Vector3((transform.position.x * 0.7f), 0.5f, transform.position.z*0.7f);
        }
        if (Mathf.Abs(transform.position.z) > 9f)
        {
            transform.position = new Vector3(transform.position.x, 0.5f,(-transform.position.z * 0.95f));
            thiscreature.deplacementCreature.destination = transform.position = new Vector3((transform.position.x * 0.7f), 0.5f, transform.position.z * 0.7f);
        }
    }
}
