using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameOver = true;
    public GameObject player;

    private UIManager uimanager;
    private SpawnManager spawnManager;

    private void Start()
    {
        uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    void Update()
    {
        if (gameOver)
        {
            if (Input.GetKeyDown("space"))
            {
                Instantiate(player, new Vector3(0,0,0), Quaternion.identity);
                gameOver = false;
                uimanager.HideTitleScreen();
                spawnManager.startSpawning();
            }
        }
    }
}
