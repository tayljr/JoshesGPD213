using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class CantUseGun : AntAIState
{
    public override void Enter()
    {
        base.Enter();
        Finish();
    }
}
