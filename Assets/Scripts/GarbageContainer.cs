using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageContainer : Container, IService
{
    public float MaxGarbageContains { get; set; }

    public void Service()
    {
        MaxGarbageContains = 0.0f;
    }
}
