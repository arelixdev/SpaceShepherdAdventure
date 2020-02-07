using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    /// <summary>
    /// DetectionZone is used for a creature to add stimuly for the coroutine tick.
    /// </summary>

    public Creature CreatureParent;

    public List<Creature.Stimuli> stimulyZoneDay;

    private void OnTriggerEnter(Collider other)
    {
        
        for (int i = 0; i < stimulyZoneDay.Count; i++)
        {
            if (other.tag == stimulyZoneDay[i].target)
            {
                stimulyZoneDay[i].cibleReaction = other.gameObject;
                CreatureParent.InZone.Add(stimulyZoneDay[i]);
            }
        } 
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (Creature.Stimuli stimuliEnter in stimulyZoneDay)
        {
            if (other.tag == stimuliEnter.target)
            {
                CreatureParent.InZone.Remove(stimuliEnter);
            }
        }
    }
}
