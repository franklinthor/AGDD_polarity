﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AGDDPlatformer;

public class Spike : MonoBehaviour
{
    public GameObject GameManager;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D (Collider2D other) {
        GameManager.GetComponent<GameManager>().ResetLevel();
        Debug.Log("Did trigger");
    }
}
