using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ComportementBase :IComportementBase
{
    /// <summary>
    /// Comportement est une suite de charactère designant un ensemble de réactions au monde environnement
    /// </summary>
    /// 

    // Some getters for all game state
    public static readonly IComportementBase fuite = new ComportementFuite();
    public static readonly IComportementBase confiant = new ComportementConfiant();
    public static readonly IComportementBase idle = new ComportementIdle();
    public static readonly IComportementBase sociale = new ComportementSociale();

    public abstract Vector3 MoveTo(Creature Creature, GameObject target);
}
