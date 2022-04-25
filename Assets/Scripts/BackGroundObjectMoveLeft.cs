using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundObjectMoveLeft : MonoBehaviour
{
    private Spawner spawner;
    void Start()
    {
        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
    }
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * spawner.leftSpeed,Space.World);
        if (!gameObject.CompareTag("moreGround") && transform.position.x <-150)
        {
            Destroy(gameObject);
        }
    }
}