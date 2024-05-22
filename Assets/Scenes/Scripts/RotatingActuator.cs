using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingActuator : MonoBehaviour
{
    private float startRotation;
    [SerializeField] private float endRotation;
    // Need to calculate positive or negative (clockwise or counterclockwise)


    void Start()
    {
        startRotation = gameObject.transform.eulerAngles.z;
    }

    
    void Update()
    {
        
    }
}
