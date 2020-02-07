using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enclos : MonoBehaviour
{
    public int MoutontDestroy=5;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag=="mouton" && other !=null)
        {
            MoutontDestroy--;
            Destroy(other.gameObject);
        }
    }

    public void Update()
    {
        if (MoutontDestroy<=0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
