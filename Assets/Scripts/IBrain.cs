using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IBrain : ScriptableObject
{
    Battler battler;
    public delegate void OnMyTurnDelegate(IBrain brain);
    public event OnMyTurnDelegate OnMyTurn;

    public IBrain (Battler battler)
    {
        this.battler = battler;
    }

    public void OnBattlerTurn(Battler battler)
    {
        if (battler == this.battler)
        {
            //my turn; act
            OnMyTurn?.Invoke(this);
        }
    }


}
