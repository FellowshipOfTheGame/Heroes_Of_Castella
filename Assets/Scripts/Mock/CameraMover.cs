﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mock
{
    public class CameraMover : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) / 10;
        }
    }
}

