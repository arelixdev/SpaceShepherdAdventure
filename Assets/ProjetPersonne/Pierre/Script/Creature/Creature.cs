using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;

public class Creature : MonoBehaviour
{
    /// <summary>
    /// this script give to an animal a behaviour to its environnement.
    /// 
    /// </summary>

    [System.Serializable]
    public class Stimuli
    {
        public string comportement;
        public DetectionZone detection;
        public float Weight;
        public string target;
        public GameObject cibleReaction;
    }

    [SerializeField]
    public Creature.Stimuli[] DayStimuli;
    [SerializeField]
    public Creature.Stimuli[] NightStimuli;

    public List<Creature.Stimuli> InZone;

    public Vector3 positionToGo;

    public float Speed;

    public float DurationReaction;

    public float ReactionTime;

    public float range;

    //public NavMeshAgent deplacementCreature;

    public AIPathAlignedToSurface aiCreature;
    public AIDestinationSetter posCreature;

    public IComportementBase Action;

    protected Dictionary<string, IComportementBase> Comportement = new Dictionary<string, IComportementBase>();

    public Transform cible;



    public void Start()
    {
        InitializeComportement();
        AttributeStimuly();
        InvokeRepeating("Tick", ReactionTime, ReactionTime);
    }

    public void InitializeComportement()
    {
        Comportement = new Dictionary<string, IComportementBase>() {
        { "fuite", ComportementBase.fuite },
        {"sociale", ComportementBase.sociale },
        {"idle", ComportementBase.idle },
        {"confiant", ComportementBase.sociale }
        };
    }

    public void Tick()
    {
        /// 
        /// Every reaction time, the animal will check if he sees something he has a reaction for :
        ///  if he does have a reaction, he will give the creature a position to go, with a duration.
        ///  
        Vector3 AverageDirection= new Vector3();
        DurationReaction = 0;
        if (InZone.Count>0)
        {
            foreach (Stimuli reaction in InZone)
            {
                if (reaction.Weight > DurationReaction)
                {
                    DurationReaction = reaction.Weight;
                    Debug.Log(reaction.comportement);
                }
                if (Comportement[reaction.comportement].MoveTo(this, reaction.cibleReaction).magnitude>1)
                {
                    AverageDirection += Comportement[reaction.comportement].MoveTo(this, reaction.cibleReaction);
                }
            }
            if (AverageDirection.magnitude>1)
            {
                AverageDirection = AverageDirection.normalized * DurationReaction * Speed;
                aiCreature.maxSpeed = Speed*DurationReaction;
                cible.position = GivePosition(AverageDirection* range);
                Debug.Log("cible pôsition " + cible.position) ; 
                aiCreature.SearchPath();
                //deplacementCreature.SetDestination(positionToGo);
            }
        }

    }

    public Vector3 GivePosition(Vector3 Direction)
    {
        Vector3 Position = transform.position + Direction;
        
        return Position;
    }

    public void AttributeStimuly()
    {
        foreach(Creature.Stimuli stimuli in DayStimuli)
        {
            stimuli.detection.stimulyZoneDay.Add(stimuli);
        }
    }

    public void Update()
    {
        if (aiCreature.isStopped)
        {
            positionToGo = Vector3.zero;
        }
    }
}
