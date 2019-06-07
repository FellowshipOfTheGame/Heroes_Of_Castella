using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public delegate void OnTurnStartDelegate(ITurnTaker actor); // TODO: if using a more modular role-based architecture, replace IBattler for the corresponding role interface

public interface ITurnManager
{
    void Update(); //TODO should really be public? - probably not, might use Start(), Lock(), Pause() instead to manage flow
    List<ITurnTaker> TurnTakers { get; set; }
    event OnTurnStartDelegate OnBattlerTurn;
    //event EventHandler OnBattlerTurn; //TODO replace by AddListener/RemoveListener?
    //void OnBattlerTurnEnded(System.Object sender, EventArgs e);
    void Lock();
    void Unlock();
}

