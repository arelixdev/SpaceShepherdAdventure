using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorIdle : BehaviorBase
{
    /// <summary>
    /// Fuite envoie le gameObject dans la direction opposés à ce qu'il a peur
    /// </summary>
    /// 

    public override Vector3 MoveTo(Creature Creature, GameObject target)
    {
        return Vector3.zero;
    }

    /*
     * Vector3 randomDirection = Random.insideUnitSphere * radius;
            randomDirection += transform.position;
            NavMeshHit hit;
            Vector3 finalPosition = Vector3.zero;
            if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1)) {
                finalPosition = hit.position;            
            }
            return finalPosition;
     * */

    public override bool TakeAction(Creature Creature, Creature.Stimuli stimuli, GameObject target)
    {
        return true;
    }
}
