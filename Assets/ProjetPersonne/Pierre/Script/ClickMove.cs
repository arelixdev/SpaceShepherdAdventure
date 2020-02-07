using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickMove : MonoBehaviour
{
    /// <summary>
    /// ce script déplace le berger sur l'écran en fonction du click du personnage
    /// </summary>
    /// 

    public float speed;
    public NavMeshAgent player;

    public void Update()
    {
        if (Input.GetAxis("Fire1")>0)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 position = hit.point;
                player.SetDestination(position);
                // Do something with the object that was hit by the raycast.
            }
            
        }
    }
}
