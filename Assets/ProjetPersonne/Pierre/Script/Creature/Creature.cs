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
    /// 
    [System.Serializable]
    public class Stimuli
    {
        public EBehavior compNom;
        public DetectionZone detection;
        public float weight;
        public float distance;
        public string tag;
        public GameObject target;
        public bool actNow;
        public bool frightening;
        public float maxWeightAct;
        public float minWeightAct;
    }

    [SerializeField]
    public Creature.Stimuli[] DayStimuli;
    [SerializeField]
    public Creature.Stimuli[] NightStimuli;

    public List<Creature.Stimuli> InZone;

    public Vector3 positionToGo;

    public float speed;

    public float duration;

    public float curWeight;

    public float reactionTime;

    public AIPathAlignedToSurface aiCreature;
    public AIDestinationSetter posCreature;

    public IBehavior Action;

    public Dictionary<EBehavior, IBehavior> DComp = new Dictionary<EBehavior, IBehavior>();

    public void Start()
    {
        InitializeComportement();
        AttributeStimuly();
        StartCoroutine(Tick(reactionTime));
        posCreature.posToGo = transform.position;
    }

    public void InitializeComportement()
    {
        DComp = new Dictionary<EBehavior, IBehavior>() {
            { EBehavior.approach, BehaviorBase.approach },
            { EBehavior.flee, BehaviorBase.flee },
            { EBehavior.idle, BehaviorBase.idle },
            { EBehavior.social, BehaviorBase.social }
        };
    }

    public IEnumerator Tick(float reactionOne)
    {
        /// 
        /// Every reaction time, the animal will check if he sees something he has a reaction for :
        ///  if he does have a reaction, he will give the creature a position to go, with a duration.
        ///  
        Vector3 AverageDirection = new Vector3();
        float WeightTick=0;
        bool isStimule=false;
        if (InZone.Count > 0)
        {
            foreach (Stimuli reaction in InZone)
            {
                if (reaction.weight > WeightTick)
                {
                    WeightTick = reaction.weight;
                    duration = reaction.distance;
                }
                if (DComp[reaction.compNom].TakeAction(this,reaction, reaction.target))
                {
                    AverageDirection += DComp[reaction.compNom].MoveTo(this, reaction.target);
                    isStimule = true;
                }
            }
            if (isStimule)
            {
                curWeight = WeightTick;
                AverageDirection = AverageDirection.normalized * duration * speed;
                posCreature.posToGo = GivePosition(AverageDirection * duration);
                aiCreature.SearchPath();
            }
        }
        yield return new WaitForSeconds(reactionOne);
        StartCoroutine(Tick(reactionTime));
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
        if (aiCreature.reachedDestination)
        {
            curWeight = 0;
        }
    }
}
