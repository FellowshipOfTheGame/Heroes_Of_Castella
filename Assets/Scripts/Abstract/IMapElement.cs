using UnityEngine;

public interface IMapElement
{
    //Map - Position and Navigation
    Vector3 Position { get; set; }
    IBattleMap Map { get; set; }
}
