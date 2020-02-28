using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dome : MonoBehaviour
{
    public List<GameObject> moutons;
    public Text nbMoutons;
    int index;
    public GameObject win;

    private void Update()
    {
        nbMoutons.text = index + "/" + moutons.Count + " moutons";

        if(index >= moutons.Count)
        {
            win.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name.Contains("DomeCol"))
        {
            index++;
            Destroy(other.transform.parent.gameObject);
        }
    }
}
