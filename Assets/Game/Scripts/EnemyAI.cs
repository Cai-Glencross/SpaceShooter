using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private float speed = 3.0f;

    [SerializeField]
    private GameObject enemyExplosionPrefab;

    private UIManager uiManager;
    private GameManager gameManager;
    private Random random = new Random();
    [SerializeField]
    private int worth = 50;

    [SerializeField]
    private AudioClip audioClip;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < -7f)
        {
            transform.position = new Vector3(Random.Range(-10.0f,10.0f), 7f, transform.position.z);
        }

        if (gameManager.gameOver)
        {
            Destroy(this.gameObject);
        }

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collision with " + other.name);
        if (other.tag == "Player")
        {
            try
            {
                Player p = other.GetComponent<Player>();
                p.Damage();
                AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);

            }
            catch
            {
                Debug.Log("issue getting compenent from player");
            }
            Destroy(this.gameObject);
            Instantiate(enemyExplosionPrefab, transform.position, Quaternion.identity);
        }else if (other.tag == "Laser")
        {
            Debug.Log("collision with laser!");
            try
            {
                Laser l = other.GetComponent<Laser>();
                Destroy(l.gameObject);
                AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);

            }
            catch
            {
                Debug.Log("problem destroying laser game object");
            }
            Destroy(this.gameObject);
            Instantiate(enemyExplosionPrefab, transform.position, Quaternion.identity);
            uiManager.UpdateScore(worth);
        }

    }


}
