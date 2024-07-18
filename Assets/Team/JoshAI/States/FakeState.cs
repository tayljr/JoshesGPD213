using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class FakeState : AntAIState
{
    private AISensor aiSensor;


    public override void Create(GameObject aGameObject)
    {
        aiSensor = aGameObject.GetComponent<AISensor>();
    }

}
