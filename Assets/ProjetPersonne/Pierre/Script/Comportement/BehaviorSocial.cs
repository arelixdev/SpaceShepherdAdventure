using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorSocial : BehaviorBase
{
    /// <summary>
    /// suit son camarade mouton
    /// </summary>
    /// 

    public override Vector3 MoveTo(Creature Creature, GameObject target)
    {
        if (target == null)
        {
            return Vector3.zero;
        }
        if (target.GetComponentInParent<Creature>()!=null)
        {
            if (target.GetComponentInParent<Creature>().curWeight > 1)
            {
                return -(Creature.transform.position - target.GetComponentInParent<Creature>().posCreature.posToGo) ;
            }
        }
        else
        {
            return Vector3.zero;
        }
        return Vector3.zero;
    }

    public override bool TakeAction(Creature Creature, Creature.Stimuli stimuli, GameObject target)
    {
        if (target != null)
        {
            if (target.GetComponentInParent<Creature>() != null)
            {
                if (target.GetComponentInParent<Creature>().curWeight > stimuli.minWeightAct && Creature.curWeight < stimuli.maxWeightAct)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
