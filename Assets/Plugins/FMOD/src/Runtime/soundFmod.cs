using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class soundFmod : MonoBehaviour
{
    public sound sounds;
    FMOD.Studio.EventInstance test;

    public void Start()
    {
        test = FMODUnity.RuntimeManager.CreateInstance(sounds.Event);
        test.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        test.setProperty(FMOD.Studio.EVENT_PROPERTY.MINIMUM_DISTANCE, 0);
        test.setProperty(FMOD.Studio.EVENT_PROPERTY.MAXIMUM_DISTANCE, 150);
        
        test.start();
    }

    void Update()
    {
        SetParameter(test, "Jour_Nuit", sounds.value);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            FMOD.Studio.EventInstance test = FMODUnity.RuntimeManager.CreateInstance(sounds.Event);
            test.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
            test.setProperty(FMOD.Studio.EVENT_PROPERTY.MINIMUM_DISTANCE, 0);
            test.setProperty(FMOD.Studio.EVENT_PROPERTY.MAXIMUM_DISTANCE, 150);
            SetParameter(test, "Event_Sheep", sounds.value);
            test.start();
            StartCoroutine(StopSound(test));
            //RuntimeManager.PlayOneShotAttached(sounds.Event, gameObject);
        }
    }

    IEnumerator StopSound(FMOD.Studio.EventInstance e)
    {
        yield return new WaitForSeconds(1);
        e.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
    
    void SetParameter(FMOD.Studio.EventInstance e, string name, float value)
    {
        e.setParameterByName(name, value);
    }

    [System.Serializable]
    public class sound
    {
        public string Event;

        public enum EmitterGameEvent
        {
            None,
            ObjectStart,
            TriggerEnter,
            TriggerExit,
            TriggerStay,
            customVoid
        }

        public EmitterGameEvent type;

        public string tagName;

        public float minDist, MaxDist;

        public string[] nameParam;

        [Range(0,1)]
        public int value;
    }
}
