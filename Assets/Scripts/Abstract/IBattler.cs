using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public delegate void OnActionChosenDelegate(IAction action);

public interface IBattler
{
    //Serialization
    Byte[] Serialized();
    void Deserialize(Byte[] data);


    //TurnManagement
    void InitializeInitiative();
    void UpdateInitiative();
    float GetInitiative();
    void SetInitiative(float value);
    bool IsReady(); // whether it is ready to start it's turn - (not sure if this is necessary: the TurnManager could/should probably decide that)
    bool IsActive(); // whether it is it's turn
    void SetActive(bool value); // give or take away it's ability to act
    void SubscribeToOnTurnStart(ref OnTurnStartDelegate e);  // subscribes it to a OnTurnStart event
    void UnSubscribeToOnTurnStart(ref OnTurnStartDelegate e);  // unsubscribes it to a OnTurnStart event


    //Map - Position and Navigation
    Vector3 GetPosition();
    void SetPosition(Vector3 position);


    //Battle
    string GetName();
    int GetTeam();
    void SetTeam(int team);
    //status effects...
    int TakeDamage(int damage);
    //To subscribe and unsubscribe to void OnActionChosen(Action a)
    event OnActionChosenDelegate OnActionChosen;
    
}
