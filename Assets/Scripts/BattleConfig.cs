using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public struct BattlerRef{
    Character character;
    Vector2 pos;
    bool isPlayerControlled;
}

public static class BattleConfig
{
    public static List<BattlerRef> battlers = new List<BattlerRef>();

    public static int SendBattlers(){
        int returnValue = 0;
        PlayerHub hub = PlayerHub.Instance;
        if(hub != null){
            // Envia todos os battlers para o servidor
            IFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            foreach(BattlerRef br in battlers){
                formatter.Serialize(stream, br);
                hub.CmdSendNewBattler(stream.ToArray());
                returnValue++;
            }
        }
        return returnValue;
    }
}
