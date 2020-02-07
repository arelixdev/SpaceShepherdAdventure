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
        if (Target.GetComponent<Creature>()!=null)
        {
            if (Target.GetComponent<Creature>().DurationReaction > 1)
            {
                return -(Creature.transform.position - Target.GetComponent<Creature>().positionToGo) ;
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
