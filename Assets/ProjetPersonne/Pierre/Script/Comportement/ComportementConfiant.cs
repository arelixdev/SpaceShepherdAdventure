using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportementConfiant : ComportementBase
{
    /// <summary>
    /// Fuite envoie le gameObject dans la direction opposés à ce qu'il a peur
    /// </summary>
    /// 

    public override Vector3 MoveTo(Creature Creature, GameObject target)
    {
        return (Creature.transform.position - target.transform.position);
    }
}
