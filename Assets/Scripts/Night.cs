using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Night : MonoBehaviour
{
    public float speed;
    public bool active;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            active = !active;
        }
        if (active)
        {
            float rota = transform.eulerAngles.y + speed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, rota, 0);
        }
    }
}
