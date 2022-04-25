using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private float rotationSpeed = 100.0f;
    private GameManager gameManager;
    private Spawner spawner;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();

    }
    void Update()
    {
        CoinMovement();
    }
    void CoinMovement()
    {
        if (!gameManager.isGameOver)
        {
            transform.Translate(Vector3.left * Time.deltaTime * spawner.leftSpeed, Space.World);
        }
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}