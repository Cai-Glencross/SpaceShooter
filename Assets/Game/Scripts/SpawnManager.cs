using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject[] powerups;
    [SerializeField]
    private GameObject playerPrefab;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(EnemySpawner());
        StartCoroutine(PowerupSpawner());
    }

    public void startSpawning()
    {
        StartCoroutine(EnemySpawner());
        StartCoroutine(PowerupSpawner());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator EnemySpawner()
    {
        while(!gameManager.gameOver)
        {
            yield return new WaitForSeconds(5.0f);

            Instantiate(enemyPrefab, new Vector3(Random.Range(-10f, 10f), -8f, 0), Quaternion.identity);
        }
    }

    IEnumerator PowerupSpawner()
    {

        while (!gameManager.gameOver)
        {
            int randomPowerup = Random.Range(0, 3);

            yield return new WaitForSeconds(5.0f);

            Instantiate(powerups[randomPowerup], new Vector3(Random.Range(-10f, 10f), 8f, 0), Quaternion.identity);
        }
    }

}
