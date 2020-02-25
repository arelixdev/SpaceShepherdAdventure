using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    /// <summary>
    /// DetectionZone is used for a creature to add stimuly for the coroutine tick.
    /// </summary>

    public Creature creature;

    public List<Creature.Stimuli> stimulyZoneDay;

    public Dictionary<EBehavior, IBehavior> DComp;


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent != creature.transform)
        {
            for (int i = 0; i < stimulyZoneDay.Count; i++)
            {
                if (other.tag == stimulyZoneDay[i].tag)
                {
                    if (stimulyZoneDay[i].actNow && creature.DComp[stimulyZoneDay[i].compNom].TakeAction(creature, stimulyZoneDay[i], other.gameObject))
                    {
                        creature.StopCoroutine(creature.Tick(0));
                        creature.StartCoroutine(creature.Tick(0));
                    }
                    stimulyZoneDay[i].target = other.gameObject;
                    creature.InZone.Add(stimulyZoneDay[i]);
                }
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (Creature.Stimuli stimuliEnter in stimulyZoneDay)
        {
            if (other.tag == stimuliEnter.tag)
            {
                creature.InZone.Remove(stimuliEnter);
            }
        }
    }
}
