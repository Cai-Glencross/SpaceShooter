 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;
    [SerializeField]
    private float fireRate = 0.2f;
    private float nextFire = 0.0f;
    [SerializeField]
    private float speedBoostFactor = 2.0f;

    public bool canTripleShot = false;
    private bool isShieldActive = false;

    [SerializeField]
    private GameObject shieldGameObject;

    [SerializeField]
    private int numLives = 3;

    [SerializeField]
    private GameObject laserPrefab;
    [SerializeField]
    private GameObject triple_shot;

    [SerializeField]
    private GameObject explosion;

    private GameManager gamemanager;

    [SerializeField]
    private int punishment = -10;

    private UIManager uiManager;

    private AudioSource audioSource;

    [SerializeField]
    private GameObject[] engines;

    // Start is called before the first frame update
    void Start()
    {
        //starting position in the middle
        transform.position = new Vector3(0, 0, 0);

        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        uiManager.UpdateLives(numLives);

        gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        Movement();
        Shoot();

    }

    private void Shoot()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0)) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            audioSource.Play();
            if (canTripleShot)
            {
                Instantiate(triple_shot, transform.position + new Vector3(-.45f, 0, 0), Quaternion.identity);
            }
            else
            {
                Instantiate(laserPrefab, transform.position + new Vector3(0, .487f, 0), Quaternion.identity);
            }
        }
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);
        transform.Translate(Vector3.up * verticalInput * speed * Time.deltaTime);

        if (transform.position.y > 0f)
        {
            transform.position = new Vector3(transform.position.x, 0f);
        }
        else if (transform.position.y < -4.3f)
        {
            transform.position = new Vector3(transform.position.x, -4.3f);
        }

        if (Mathf.Abs(transform.position.x) > 12.2f)
        {
            transform.position = new Vector3(-(transform.position.x / Mathf.Abs(transform.position.x) * 12.2f), transform.position.y);
        }
    }

    public void Damage()
    {
        if (isShieldActive)
        {
            isShieldActive = false;
            shieldGameObject.SetActive(false);
            return;
        }
        numLives -= 1;
        uiManager.UpdateLives(numLives);
        uiManager.UpdateScore(punishment);
        int engineToFail = Random.Range(0, 2);
        if (engines[engineToFail].activeSelf)
        {
            if (engineToFail == 1)
            {
                engines[0].SetActive(true);
            }
            else
            {
                engines[1].SetActive(true);
            }
        }
        else
        {
            engines[engineToFail].SetActive(true);
        }
        if (numLives <= 0)
        {
            Destroy(this.gameObject);
            Instantiate(explosion, transform.position, Quaternion.identity);
            gamemanager.gameOver = true;
            uiManager.ShowTitleScreen();

        }



    }

    public void TriggerShield()
    {
        isShieldActive = true;
        shieldGameObject.SetActive(true);
    }

    public void TriggerTripleShot()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);

        canTripleShot = false;
    }

    public void TriggerSpeedBoost()
    {
        speed = speed * speedBoostFactor;
        StartCoroutine(SpeedBoostPowerDownRoutine()); 
    }
    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(10.0f);

        speed = speed / speedBoostFactor;
    }
}
