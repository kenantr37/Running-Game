using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject ground;
    public GameObject timerPanel, scorePanel;
    public TextMeshProUGUI scoreYazi, gameOverText, timeText;
    public Button restartButton, startButton;

    public Vector3 groundStartingPosition;
    private float ground_collider_lenght;
    private PlayerMovement player;
    private Spawner spawner;

    public int timer = 1;
    public float leftSpeed = 10.0f;
    public bool isGameOver, startGame;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        scoreYazi = GameObject.Find("Canvas/Score").GetComponent<TextMeshProUGUI>();
        timeText = GameObject.Find("Canvas/Time").GetComponent<TextMeshProUGUI>();
        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
        timerPanel = GameObject.Find("PanelTimer");
        scorePanel = GameObject.Find("PanelScore");

        groundStartingPosition = ground.transform.position;
        ground_collider_lenght = ground.gameObject.GetComponent<BoxCollider>().size.x / 2;
        scoreYazi.text = "Score : 0";
        timeText.gameObject.SetActive(false);
        timerPanel.gameObject.SetActive(false);
        scoreYazi.gameObject.SetActive(false);
        scorePanel.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);

    }
    void Update()
    {
        //GroundMoveLeft();
        Player();
        ScoreCoinAdd();
        GameOverTextButton();
    }
    IEnumerator Timer()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(1);
            TimerManager();
        }
    }
    void GroundMoveLeft()
    {
        //Debug.Log(ground.transform.position.x);
        //Debug.Log("collider length" + " " + (-ground_collider_lenght));
        if (!isGameOver && startGame)
        {
            ground.gameObject.transform.Translate(Vector3.left * Time.deltaTime * spawner.leftSpeed, Space.World);
        }
    }

    void ScoreCoinAdd()
    {
        scoreYazi.text = "Score : " + player.coinNumberCount.ToString();
    }
    void GameOverTextButton()
    {
        if (isGameOver)
        {
            restartButton.gameObject.SetActive(true);
            gameOverText.gameObject.SetActive(true);
        }
    }
    void Player()
    {
        if (player.playerdead)
        {
            isGameOver = true;
            startGame = false;
        }
    }
    void TimerManager()
    {
        timer += 1;
        //Debug.Log(timer);
        timeText.text = "Time : " + timer.ToString();
    }
    //This is for Restart Button
    public void LoadScenes()
    {
        SceneManager.LoadScene("SampleScene");
    }
    // StartGame method is created for Starting game by clicking start-button.
    // This is a technic from Unity Junior Pathway. You need to active all game actions by this button click.
    // That's why we defined this Coroutines here.
    public void StartGame()
    {
        isGameOver = false;
        startGame = true;
        timeText.gameObject.SetActive(true);
        scoreYazi.gameObject.SetActive(true);
        timerPanel.gameObject.SetActive(true);
        scorePanel.gameObject.SetActive(true);
        timeText.text = "Time : 0";
        timeText.text = "Time : 0";
        startButton.gameObject.SetActive(false);

        Destroy(startButton.gameObject);

        StartCoroutine(spawner.SpawnWorkersTime());
        //StartCoroutine(spawner.SpawnBackgroundObstaclesTime());
        //StartCoroutine(spawner.SpawnObstacles());
        StartCoroutine(spawner.SpawnCoinTime());
        //StartCoroutine(spawner.SpawnTubeTime());
        StartCoroutine(Timer());
    }

}