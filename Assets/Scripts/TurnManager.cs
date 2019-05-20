using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TurnManager : NetworkBehaviour
{
    TurnQueue turnQueue;
    List<Battler> battlers; //initialize elsewhere
    public List<CharacterSO> characters; //wut?

    

    private bool waitingForInput = false;

    // Start is called before the first frame update
    void Start()
    {
        battlers = new List<Battler>();
        foreach (CharacterSO c in characters)
        {
            battlers.Add(new Battler(c));
        }
        BattleField.Battlers = battlers;
        turnQueue = new TurnQueue(battlers);
    }

    // Update is called once per frame
    void Update()
    {
        if (waitingForInput)
        {
            return;
        }
        if (!turnQueue.Update())
        {
            StartCoroutine(WaitForInput());
        }
    }

    IEnumerator WaitForInput()
    {
        waitingForInput = true;
        yield return new WaitForSeconds(3);
        waitingForInput = false;
    }
}
