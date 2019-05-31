using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IBattleMap
{
    bool AddBattler(IBattler battler, Vector3 position);
    bool RemoveBattler(IBattler battler);
    event EventHandler OnBattlerDead;
    bool MoveBattlerTo(IBattler battler, Vector3 position);
}
