using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Unity.VisualScripting;
using UnityEngine;

public class GetSword : AntAIState
{

    private AISensor aiSensor;
    public GameObject mainObject;

    public GameObject swordItem;

    private void OnEnable()
    {
        mainObject.GetComponent<ColliderTriggerManager>().OnTriggerEnter_Event += mainTrigger;
    }
    private void OnDisable()
    {
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
        swordItem = FindObjectOfType<Sword>().gameObject;
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        
    }

    private void mainTrigger(Collider other)
    {
        if (other == swordItem.GetComponent<Collider>())
        {
            mainObject.GetComponent<Inventory>().hasSword = true;
            Finish();
        }
    }
}
