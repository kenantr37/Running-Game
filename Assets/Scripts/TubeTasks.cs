using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeTasks : MonoBehaviour
{
    private Spawner spawner;
    private GameManager gameManager;
    void Start()
    {
        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * spawner.leftSpeed);
        transform.Rotate(Vector3.left * Time.deltaTime, Space.World);
        TubeDestroy();
    }
    void TubeDestroy()
    {
        if (transform.position.x < -150)
        {
            Destroy(gameObject);
        }
    }
}
