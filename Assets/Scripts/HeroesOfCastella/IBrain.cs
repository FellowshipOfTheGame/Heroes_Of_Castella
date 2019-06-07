using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBrain
{
    void Initialize(ITurnTaker turnTaker, IBattleMap battleMap); // Who am I? What is the world I'm in?
    void ChooseAction();
    event OnActionChosenDelegate OnActionChosen;
}
