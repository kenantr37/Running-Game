using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyPower : MonoBehaviour
{
    void MoveLeft()
    {
        //there is a problem 'bout coordinate of vector3.left
        transform.Translate(Vector3.left * 10 * Time.deltaTime);
    }
}
