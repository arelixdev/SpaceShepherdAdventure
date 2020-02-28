using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviorBase : IBehavior
{
    /// <summary>
    /// Comportement est une suite de charactère designant un ensemble de réactions au monde environnement
    /// </summary>
    /// 

    // Some getters for all game state
    public static readonly IBehavior flee = new BehaviorFlee();
    public static readonly IBehavior approach = new BehaviorApproach();
    public static readonly IBehavior idle = new BehaviorIdle();
    public static readonly IBehavior social = new BehaviorSocial();

    public abstract Vector3 MoveTo(Creature Creature, GameObject target);

    public abstract bool TakeAction(Creature Creature, Creature.Stimuli stimuli, GameObject target);
}
