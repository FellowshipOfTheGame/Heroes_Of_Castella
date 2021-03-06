﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill : ISerializable
{
    bool Apply(IBattler agent, params Vector3[] positions);
}
