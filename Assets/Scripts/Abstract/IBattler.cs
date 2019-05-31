using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IBattler
{
    Vector3 GetPosition();
    void SetPosition(Vector3 position);
    float GetInitiative();
    //status effects...
    void OnBattlerTurn(System.Object sender, EventArgs e);
    int TakeDamage(int damage);
    //To subscribe and unsubscribe to void OnActionChosen(Action a)
    event EventHandler OnActionChosen;
    void UpdateInitiative();
    bool IsReady();
}
