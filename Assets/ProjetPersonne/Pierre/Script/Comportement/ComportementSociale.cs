using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportementSociale : ComportementBase
{
    /// <summary>
    /// Fuite envoie le gameObject dans la direction opposés à ce qu'il a peur
    /// </summary>
    /// 

    public override Vector3 MoveTo(Creature Creature, GameObject Target)
    {
        if (Target == null)
        {
            return Vector3.zero;
        }
        if (Target.GetComponentInParent<Creature>()!=null)
        {
            if (Target.GetComponentInParent<Creature>().DurationReaction > 1)
            {
                Debug.Log("prout " + -(Creature.transform.position - Target.GetComponentInParent<Creature>().posCreature.posToGo));
                return -(Creature.transform.position - Target.GetComponentInParent<Creature>().posCreature.posToGo) ;
            }
            else
            {
                return Vector3.zero;
            }
        }
        else
        {
            return Vector3.zero;
        }
    }
}
