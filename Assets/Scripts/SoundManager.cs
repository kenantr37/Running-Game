using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private GameManager gameManager;
    private PlayerMovement player;
    private AudioSource audioSource;
    public AudioClip beforeStartSound;
    public AudioClip jump;
    public AudioClip coin;
    public AudioClip running;
    public AudioClip explosion;
    public AudioClip deathSong,slide;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();
        BeforeStart();
    }
    void BeforeStart()
    {
        if (!gameManager.startGame)
        {
            audioSource.PlayOneShot(beforeStartSound,.4f);
        }
    }
    public void CoinSound()
    {
        audioSource.PlayOneShot(coin);
    }
    public void JumpSound()
    {
        audioSource.PlayOneShot(jump,1.0f);
    }
    public void Crashed()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(explosion, .5f);
        audioSource.PlayOneShot(deathSong, 1.0f);        
    }
    public void SlideSound()
    {
        audioSource.PlayOneShot(slide);
    }
}
