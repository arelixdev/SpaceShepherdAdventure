using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlaySound : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string eventPlay;
    // Start is called before the first frame update
    void Start()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(eventPlay, gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached(eventPlay, gameObject);
        }
    }
}
