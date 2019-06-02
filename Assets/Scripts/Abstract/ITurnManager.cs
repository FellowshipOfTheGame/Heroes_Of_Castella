using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public interface ITurnManager
{
    void Update(); //TODO should really be public? - probably not, might use Start(), Lock(), Pause() instead to manage flow
    void SetBattlers(List<IBattler> battlers);
    event EventHandler OnBattlerTurn; //TODO replace by AddListener/RemoveListener?
    //void OnBattlerTurnEnded(System.Object sender, EventArgs e);
    void Lock();
    void Unlock();
}
