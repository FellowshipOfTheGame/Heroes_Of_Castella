using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


[System.Serializable]
public static class Serializable<T>
{
    public static byte[] SerializedForNetwork(T obj)
    {
        MemoryStream stream = new MemoryStream();
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(stream, obj);
        return stream.GetBuffer();
    }

    public static T DeserializedFromNetwork(byte[] serializedObject)
    {
        MemoryStream stream = new MemoryStream(serializedObject);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        return (T)binaryFormatter.Deserialize(stream);
    }
}
