using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoMovement : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(0, 1 * 55*Time.deltaTime, 0);
    }
}
