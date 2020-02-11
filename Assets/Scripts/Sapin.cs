using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sapin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("night"))
        {
            StartCoroutine(MoveTo(1, -transform.up));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("night"))
        {
            StartCoroutine(MoveTo(.5f, transform.up));
        }
    }

    IEnumerator MoveTo(float time, Vector3 dir)
    {
        Vector3 start = transform.position;
        Vector3 end = start + dir * 5;
        float t = 0;


        while (t < 1)
 {
            yield return null;
            t += Time.deltaTime / time;
            transform.position = Vector3.Lerp(start, end, t);
        }
        transform.position = end;
        GameObject.FindGameObjectWithTag("planet").GetComponent<CreateNode>().DetectUnwalk();
    }
}
