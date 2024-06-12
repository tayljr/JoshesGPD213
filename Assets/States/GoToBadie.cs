using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class GoToBadie : AntAIState
{
    public Memory memory;
    public TurnTowards turnTowards;
    private AISensor aiSensor;
    public GameObject mainObject;

    public GameObject BadieObject;

    private void OnEnable()
    {
        if (mainObject == null)
        {
            mainObject = gameObject.transform.parent.gameObject;
        }
        mainObject.GetComponent<ColliderTriggerManager>().OnTriggerEnter_Event += mainTrigger;
    }
    private void OnDisable()
    {
        if (mainObject == null)
        {
            mainObject = gameObject.transform.parent.gameObject;
        }
        mainObject.GetComponent<ColliderTriggerManager>().OnTriggerEnter_Event -= mainTrigger;
    }

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        mainObject = aGameObject;
        aiSensor = aGameObject.GetComponent<AISensor>();
    }

    public override void Enter()
    {
        base.Enter();
        
        if (memory == null)
        {
            memory = gameObject.GetComponentInParent<Memory>();
        }
        
        BadieObject = FindObjectOfType<Baddie>().gameObject;
        turnTowards = gameObject.GetComponentInParent<TurnTowards>();
        turnTowards.enabled = true;
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        if (memory.memoryDic.ContainsKey(BadieObject))
        {
            turnTowards.NewTarget(memory.memoryDic[BadieObject]);
        }
    }

    private void mainTrigger(Collider other)
    {
        if (other == BadieObject.GetComponent<Collider>())
        {
            //mainObject.GetComponent<Inventory>().hasSword = true;
            turnTowards.enabled = false;
            Finish();
        }
    }
}
