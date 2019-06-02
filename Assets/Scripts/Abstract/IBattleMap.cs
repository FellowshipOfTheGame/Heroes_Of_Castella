using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IBattleMap
{
    bool AddBattler(IBattler battler, Vector3 position); // don't use for now
    bool AddTeam(List<IBattler> team);
    bool RemoveBattler(IBattler battler);
    event EventHandler OnBattlerDead;
    bool MoveBattlerTo(IBattler battler, Vector3 position);
    float GetDistance(Vector3 v1, Vector3 v2);
    float GetWalkDistance(Vector3 v1, Vector3 v2);
}
