using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerHub : NetworkBehaviour
{
    // Error: Cannot have abstract parameter
    [Command]
    public void CmdSendNewPlayer(Character.CharacterInfo charInfo){
    }
}
