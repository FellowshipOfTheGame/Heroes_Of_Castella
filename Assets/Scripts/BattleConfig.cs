using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BattlerRef{
    Character character;
    Vector2 pos;
    bool isPlayerControlled;
}

public static class BattleConfig
{
    public static List<BattlerRef> battlers = new List<BattlerRef>();
}
