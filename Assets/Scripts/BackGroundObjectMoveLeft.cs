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
        if (!gameObject.CompareTag("ground") && transform.position.x <-150)
        {
            Destroy(gameObject);
        }
        if (gameObject.name == "Ground(Clone)" && transform.position.x < -140)
        {
            Destroy(gameObject);
        }
        if (gameObject.CompareTag("ground") && transform.position.x < -140)
        {
            gameObject.transform.Translate(Vector3.right * Time.deltaTime * spawner.leftSpeed, Space.World);

        }
    }
}