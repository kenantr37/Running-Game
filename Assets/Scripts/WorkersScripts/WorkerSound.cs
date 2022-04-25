using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerSound : MonoBehaviour
{
    private GameManager gameManager;
    private AudioSource audioSource;
    void Start()
    {
        gameManager =GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (gameManager.isGameOver)
        {
            audioSource.Stop();
        }
    }
}
