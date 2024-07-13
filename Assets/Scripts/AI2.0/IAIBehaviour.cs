using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAIBehaviour
{
    behaviourEnum  Priority { get; }
    void Execute(ref int points);
}
