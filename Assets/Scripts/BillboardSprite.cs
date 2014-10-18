﻿using UnityEngine;
using System.Collections;

public class BillboardSprite : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.back,
            Camera.main.transform.rotation * Vector3.up);
    }
}