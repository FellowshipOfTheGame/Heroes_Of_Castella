using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class TestSO : SerializableSO<TestSO>
{
    public new string name;
    public int val1;
    
}
