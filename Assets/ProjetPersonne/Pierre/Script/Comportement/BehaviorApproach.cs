using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorApproach : BehaviorBase
{
    /// <summary>
    /// Fuite envoie le gameObject dans la direction opposés à ce qu'il a peur
    /// </summary>
    /// 

    public override Vector3 MoveTo(Creature Creature, GameObject target)
    {
        return (Creature.transform.position - target.transform.position);
    }

    public override bool TakeAction(Creature Creature, Creature.Stimuli stimuli, GameObject target)
    {
        if (Creature.curWeight < stimuli.maxWeightAct)
        {
            return true;
        }
        return false;
    }
}
