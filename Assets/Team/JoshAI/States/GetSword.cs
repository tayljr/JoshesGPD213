using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Unity.VisualScripting;
using UnityEngine;

public class GetSword : AntAIState
{
    public Memory memory;
    public TurnTowards turnTowards;
    private AISensor aiSensor;
    public GameObject mainObject;

    public GameObject swordItem;

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
        
        swordItem = FindObjectOfType<Sword>().gameObject;
        turnTowards = gameObject.GetComponentInParent<TurnTowards>();
        turnTowards.enabled = true;
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        if (memory.memoryDic.ContainsKey(swordItem))
        {
            turnTowards.NewTarget(memory.memoryDic[swordItem]);
        }
    }

    private void mainTrigger(Collider other)
    {
        if (other == swordItem.GetComponent<Collider>())
        {
            mainObject.GetComponent<Inventory>().hasSword = true;
            turnTowards.enabled = false;
            Finish();
        }
    }
}
