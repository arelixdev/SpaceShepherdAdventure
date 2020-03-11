using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SnapPlanet : MonoBehaviour
{
    public LayerMask layer;
    public bool snap;
    public float AngleWanted;

    // Update is called once per frame
    void Update()
    {
        if (Application.isEditor)
        {
            RaycastHit hit;
            Debug.DrawRay(transform.position + transform.up * 2, -transform.up, Color.red);
            if (Physics.Raycast(transform.position + transform.up * 5, -transform.up, out hit, Mathf.Infinity, 1 << 10) && snap)
            {
                transform.position = hit.point;
                transform.eulerAngles = Vector3.zero;
                OrientBody(transform, hit.normal);
            }
            Quaternion rotationY = Quaternion.AngleAxis(AngleWanted, transform.up);
            transform.rotation = rotationY * transform.rotation;
        }
    }

    void OrientBody(Transform attractedBody, Vector3 surfaceNorm)
    {
        Quaternion targetRotation = Quaternion.FromToRotation(attractedBody.transform.up, surfaceNorm);
        attractedBody.transform.rotation = targetRotation;
    }
}
