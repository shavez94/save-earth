﻿

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destoy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("destoyit", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void destoyit()
    {
        Destroy(gameObject);
    }
}
