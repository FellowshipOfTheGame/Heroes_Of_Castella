using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public abstract class TurnManager : NetworkBehaviour
{
    [SerializeField]private TurnQueue.Implementations queueType;
    private TurnQueue queue;
}
