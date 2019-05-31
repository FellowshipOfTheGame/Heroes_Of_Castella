using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface ITurnManager
{
    void Update();
    event EventHandler OnBattlerTurn;
}
