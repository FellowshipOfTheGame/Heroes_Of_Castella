using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{
    bool Apply(IBattler agent, params Vector3[] positions);
}
