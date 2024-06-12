using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class CanUseGun : AntAIState
{
    public AISensor sensor;
    public override void Enter()
    {
        base.Enter();
        if (sensor == null)
        {
            sensor = gameObject.GetComponentInParent<AISensor>();
        }
        sensor.CanUseWeapon = true;
        Finish();
    }
}
