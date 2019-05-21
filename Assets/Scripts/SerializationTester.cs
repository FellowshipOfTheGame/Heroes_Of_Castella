using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerializationTester : MonoBehaviour
{
    public TestSO test;

    // Start is called before the first frame update
    void Start()
    {
        //byte[] bytes = test.SerializedForNetwork();
        //TestSO newTest = SerializableSO<TestSO>.DeserializedFromNetwork(bytes);
        //test = newTest;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
