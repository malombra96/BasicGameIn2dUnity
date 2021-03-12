using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            LevelManager.shaderInstance.AddLevelBlock();
            LevelManager.shaderInstance.RemoveLevelBlock();
            print(other.tag);
        }
        else
        {
            print(other.tag);
        }
    }
}
