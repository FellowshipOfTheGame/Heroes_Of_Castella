using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonCallback : MonoBehaviour
{
    public BattlerRef[] battlers;

    public void OnButtonPressed()
    {
        Debug.Log("Pressionou");
        foreach (BattlerRef b in battlers)
            BattleConfig.battlers.Add(b);
        SceneManager.sceneLoaded += SendLocalBattlers;
    }

    public void SendLocalBattlers(Scene scene, LoadSceneMode mode)
    {
        BattleConfig.SendBattlers();
        SceneManager.sceneLoaded -= SendLocalBattlers;
    }
}
