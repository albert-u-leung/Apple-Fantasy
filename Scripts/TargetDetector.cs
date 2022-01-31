using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetector : MonoBehaviour
{
    public GameObject tree;
    public GameObject target;

    private void Start()
    {
        target = tree.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.StartsWith("Player"))
        {
            print("player enter");
            target = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.StartsWith("Player"))
        {
            print(("player exit"));
            target = tree.gameObject;
        }
    }
}
