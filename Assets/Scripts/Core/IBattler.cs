using System.Collections;
using System.Collections.Generic;

public delegate void OnActionChosenDelegate(IAction action);

public interface IBattler : ITurnTaker, IMapElement, ISerializable
{
    string GetName();
    //Battle
    int GetTeam();
    void SetTeam(int team);
    void InitializeBattler();
    //status effects...
    int TakeDamage(int damage);
    //To subscribe and unsubscribe to void OnActionChosen(Action a)
    event OnActionChosenDelegate OnActionChosen;
}
