using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody playerRb;
    private float jumpForce = 650;
    private Vector3 playerTop = new Vector3(0.0f, -49.0f, 0.0f);
    private Vector3 playerDown = new Vector3(0.0f, -9.81f, 0.0f);
    private GameManager gameManager;
    public bool playerdead, isOnGround, getCoin, standing;
    public int coinNumberCount;
    public Animator playerAnimator;
    public ParticleSystem playerDeathEffect, playerJumpEffect, coinEffect;
    private SoundManager soundManager;
    private Quaternion startingLocalRotation;
    private BoxCollider playerBoxCollider;
    private float startplayerBoxColliderCenter, startplayerBoxColliderSize;
    private Spawner spawner;
    public GameObject ground;

    void Start()
    {
        playerBoxCollider = GetComponent<BoxCollider>();
        startplayerBoxColliderCenter = playerBoxCollider.center.y;
        startplayerBoxColliderSize = playerBoxCollider.size.y;
        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();

        standing = true;
        startingLocalRotation = transform.localRotation;
        playerAnimator = GameObject.Find("PlayerAnim").GetComponent<Animator>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        playerdead = false;
        coinNumberCount = 0;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        isOnGround = true;
        getCoin = false;
        playerRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (gameManager.startGame)
        {
            if (spawner.leftSpeed < 0.75f)
            {
                playerAnimator.SetBool("Walking", true);
            }
            else if (spawner.leftSpeed >= 0.75f)
            {
                playerAnimator.SetBool("Walking", false);
                playerAnimator.SetBool("Running", true);
            }
        }
        Movements();
        GravityManager();
    }
    void Movements()
    {
        //New box collider to slide on the ground
        float collider_sizey = 0.8722377f;
        float collider_centery = -0.05679202f;

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameManager.isGameOver && gameManager.startGame)
        {
            playerAnimator.SetBool("Running", true);
            playerRb.AddForce(Vector3.up * jumpForce);
            playerAnimator.SetBool("Jumping", true);
            isOnGround = false;
            standing = true;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && isOnGround && !gameManager.isGameOver && gameManager.startGame) // CHECK THIS
        {
            soundManager.SlideSound();
            standing = false;
            jumpForce = 0;
            playerAnimator.SetBool("Slide", true);
            StartCoroutine(SlideBoxColliderSize(collider_sizey, collider_centery));
            StartCoroutine(SlideEndingTime());

        }
        if (standing)
        {
            playerBoxCollider.size = new Vector3(playerBoxCollider.size.x, startplayerBoxColliderSize, playerBoxCollider.size.z);
            playerBoxCollider.center = new Vector3(playerBoxCollider.center.x, startplayerBoxColliderCenter, playerBoxCollider.center.z);

        }
    }
    void SlideEnding()
    {
        standing = true;
        jumpForce = 650; // initial power
        playerAnimator.SetBool("Slide", false);
    }
    IEnumerator SlideEndingTime()
    {
        while (!standing)
        {
            yield return new WaitForSeconds(1.15f);
            SlideEnding();
        }
    }
    IEnumerator SlideBoxColliderSize(float collider_sizey, float collider_centery)
    {
        while (!standing)
        {
            yield return new WaitForSeconds(0);
            playerBoxCollider.size = new Vector3(playerBoxCollider.size.x, collider_sizey, playerBoxCollider.size.z);
            playerBoxCollider.center = new Vector3(playerBoxCollider.center.x, collider_centery, playerBoxCollider.center.z);
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("coin"))
        {
            coinEffect.Play();
            getCoin = true;
            Destroy(other.gameObject);
            coinNumberCount++;
            soundManager.CoinSound();
        }
        if (other.gameObject.CompareTag("obstacle") || other.gameObject.CompareTag("tube"))
        {
            playerdead = true;
            gameManager.isGameOver = true;
            playerAnimator.SetBool("Walking", false);
            playerAnimator.SetBool("Death", true);
            playerDeathEffect.Play();
            soundManager.Crashed();
            //Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("moreGround"))
        {
            Debug.Log("more ground entered");
            Instantiate(ground.gameObject, new Vector3(-56.9f,1.72f,0), ground.gameObject.transform.rotation);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isOnGround = true;
            playerAnimator.SetBool("Jumping", false);
            playerJumpEffect.Simulate(.5f); // This is the func. to start particle effect from spesific time!.
            playerJumpEffect.Play();
            gameManager.startButton.gameObject.SetActive(true);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            soundManager.JumpSound();
        }
    }
    void GravityManager()
    {
        if (!isOnGround)
        {
            Physics.gravity = playerTop;
        }
        else
        {
            Physics.gravity = playerDown;
        }
    }
}