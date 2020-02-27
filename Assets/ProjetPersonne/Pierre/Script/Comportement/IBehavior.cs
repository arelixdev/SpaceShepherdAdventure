using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBehavior
{
    Vector3 MoveTo(Creature Creature,GameObject target);
    bool TakeAction(Creature Creature, Creature.Stimuli stimuli, GameObject target);
}
