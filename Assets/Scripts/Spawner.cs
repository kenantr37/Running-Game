using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float leftSpeed = 10.0f;
    private GameManager gameManager;
    public GameObject[] obstacles;
    public GameObject[] backgroundObstacles;
    public GameObject coin, tube,ufo;
    public GameObject[] workers;
    private int[] randomTubeTimes = { 10, 15, 20 };
    private int[] randomUfoTimes = { 7, 12,14};
    int randomTubeIndex;
    private PlayerMovement playerMovement;
    public GameObject ground;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    void Update()
    {
        MoveThemLeft();
        //DestroyThem();
    }
    void DestroyThem()
    {
        if (obstacles[RandomObstacles()].gameObject.transform.position.x <= -70.0f)
        {
            Destroy(obstacles[RandomObstacles()].gameObject);
        }
    }
    void MoveThemLeft()
    {
        if (gameManager.isGameOver || !gameManager.startGame)
        {
            leftSpeed = 0;
        }
        else if (gameManager.timer > 0 && gameManager.timer < 3)
        {
            leftSpeed += .8f * Time.deltaTime;
        }
        else if (gameManager.timer > 3 && gameManager.timer < 30)
        {
            leftSpeed += .1f * Time.deltaTime;
        }
        else if (gameManager.timer > 60)
        {
            leftSpeed += .1f * Time.deltaTime;
        }
    }
    public IEnumerator SpawnObstacles()
    {
        while (!gameManager.isGameOver)
        {
            yield return new WaitForSeconds(7.0f);
            SpawnObstacle();
        }
    }
    int RandomObstacles()
    {
        int randomObstacles = Random.Range(0, obstacles.Length);
        return randomObstacles;
    }
    public IEnumerator SpawnBackgroundObstaclesTime()
    {

        while (!gameManager.isGameOver)
        {
            yield return new WaitForSeconds(5);
            SpawnBackgroundObstacles();
        }
    }
    public IEnumerator SpawnCoinTime()
    {
        while (!gameManager.isGameOver)
        {
            yield return new WaitForSeconds(9.0f);
            SpawnCoins();
        }
    }
    public IEnumerator SpawnWorkersTime()
    {
        while (!gameManager.isGameOver)
        {
            yield return new WaitForSeconds(6);
            SpawnRandomWorkers();
        }
    }
    public IEnumerator SpawnTubeTime()
    {
        while (!gameManager.isGameOver)
        {
            randomTubeIndex = Random.Range(0, randomTubeTimes.Length);
            Debug.Log("Tube will spawn in : " + randomTubeTimes[randomTubeIndex]);
            yield return new WaitForSeconds(randomTubeTimes[randomTubeIndex]);
            SpawnTubes();
        }
    }
    public IEnumerator SpawnUfoTime()
    {
        while (!gameManager.isGameOver)
        {
            int randomUfoIndex = Random.Range(0, randomUfoTimes.Length);
            Debug.Log("Ufo will spawn in : " + randomUfoTimes[randomUfoIndex]);
            yield return new WaitForSeconds(randomUfoTimes[randomTubeIndex]);
            SpawnUfo();
        }
    }
    void SpawnObstacle()
    {
        Vector3 obstaclesPosition = new Vector3(-110f, 1.61f, 0);
        Instantiate(obstacles[RandomObstacles()], obstaclesPosition, obstacles[RandomObstacles()].gameObject.transform.rotation);
    }
    void SpawnRandomWorkers()
    {
        int randomWorker = Random.Range(0, workers.Length);
        Vector3 workerPosition = new Vector3(-116.0f, 1.4f, 0.57f);
        Instantiate(workers[randomWorker], workerPosition, workers[randomWorker].transform.rotation);
    }
    GameObject SpawnBackgroundObstacles()
    {
        int randomBackgroundObstacle = Random.Range(0, backgroundObstacles.Length);
        Vector3 backgroundObstaclesSpawnVector = new Vector3(43.0f, 0, 8.5f);

        GameObject a = Instantiate(backgroundObstacles[randomBackgroundObstacle], backgroundObstaclesSpawnVector
            , backgroundObstacles[randomBackgroundObstacle].transform.rotation);
        Debug.Log(a);

        return a;
    }
    void SpawnCoins()
    {
        Vector3 coinRandomPosition = new Vector3(Random.Range(-113, -107.0f), Random.Range(1.5f, 3.096f), -0.096f);

        Instantiate(coin, coinRandomPosition, coin.transform.rotation);
    }
    void SpawnTubes()
    {
        Vector3 tubePosition = new Vector3(-116, 4.0f, -0.27f);

        Instantiate(tube, tubePosition, tube.transform.rotation);
    }
    void SpawnUfo()
    {
        Vector3 ufoPosition = new Vector3(-116, Random.Range(2,2.2f), -0.15f);

        Instantiate(ufo, ufoPosition, ufo.transform.rotation);
    }
}
