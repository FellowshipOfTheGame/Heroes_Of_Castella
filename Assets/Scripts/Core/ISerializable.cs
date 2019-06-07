using System;

public interface ISerializable
{
    //Serialization
    Byte[] Serialized();
    void Deserialize(Byte[] data);
}
