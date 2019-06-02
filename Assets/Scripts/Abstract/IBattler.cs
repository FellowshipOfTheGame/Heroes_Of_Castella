using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public interface IBattler
{
    Vector3 GetPosition();
    void SetPosition(Vector3 position);
    int GetTeam();
    void SetTeam(int team);
    float GetInitiative();
    void UpdateInitiative();
    void SetInitiative(float value);
    bool IsReady();
    bool IsActive();
    void SetActive(bool value);
    //status effects...
    void OnBattlerTurn(System.Object sender, EventArgs e);
    int TakeDamage(int damage);
    //To subscribe and unsubscribe to void OnActionChosen(Action a)
    event EventHandler OnActionChosen;
    Byte[] Serialized();
    void Deserialize(Byte[] data);
    void Initialize();
}
