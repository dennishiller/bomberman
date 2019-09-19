using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;      //!!!

public class MovementPlayer : MonoBehaviour
{
    Rigidbody rb;
    public GameObject bomb;
    private int maxBombs = 2;
    private int bombsOnField=0;
    private List<GameObject> bombList = new List<GameObject>();
    private float cooldownTime=0.5f;
    private float nextBombTime=0;
    private new Collider collider;

    private GridScript grid;

    [Header("Test")]
    public GameObject gridGO;

    private movement movementScript;
    public bool dead = false;       //reingehauen
    private int health = 3;         //reingehauen
    public RawImage dreiLeben;
    public RawImage zweiLeben;
    public RawImage einLeben;
    public float invincibilityLenght = 5;
    private float invincibilityCounter;
    public GameObject StrongerBombsPowerUp;
    private BombScript bombScript;
    public int bombPower = 2;



    private void Awake()
    {
        grid = gridGO.GetComponent<GridScript>();
        movementScript = GetComponent<movement>();
        //bombScript = FindObjectOfType<BombScript>();

        // BombScript bombScript_placeholder = bombList[0].GetComponent<BombScript>();
        // bombScript_placeholder.explode();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        collider = GetComponent<Collider>();
    }

    void Update()
    {
        PlaceBomb();
        TurnOnCollider();

        if (invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;
        }
    }

  
    private void PlaceBomb()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!(maxBombs == bombsOnField) && Time.time>nextBombTime)
            {
                GameObject Bomb = Instantiate(bomb, grid.GetNearestPointOnGrid(transform.position), Quaternion.identity);
                bombList.Add(Bomb);
                Bomb.GetComponent<BombScript>().Instantiate(gameObject);
                nextBombTime = Time.time + cooldownTime;
                bombsOnField++;
            }
        }    
    }

    public void BombExploded()
    {
        bombList.RemoveAt(0);
        bombsOnField--;      
    }

    public void IncreaseMaxBombs()
    {
        this.maxBombs++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Explosion"))          //reingehauen
        {
            if (invincibilityCounter <= 0)
            {
                PlayerGetsHurt();
            }
        }

        if (other.gameObject.CompareTag("PowerUp"))
        {
            PlayerGetsPowerUp(other);
        }
    }

    private void PlayerGetsHurt()
    {
        Debug.Log("P" + " hit by explosion!");

        invincibilityCounter = invincibilityLenght;
        Debug.Log("COunter ist jetzt= " + invincibilityCounter);
        health--;

        if (health == 2)
        {
            dreiLeben.enabled = false;
        }
        else if (health == 1)
        {
            zweiLeben.enabled = false;
        }
        else if (health == 0)
        {
            einLeben.enabled = false;
            dead = true;
            Time.timeScale = 0;
        }
    }

    private void PlayerGetsPowerUp(Collider other)
    {
        Debug.Log("OK");
        if (other.gameObject.name.Contains("SpeedPowerUp"))
        {
            Debug.Log("in der if");

            if (movementScript.speed<8)
            {
                other.gameObject.SetActive(false);
                movementScript.speed += 1f;
            }
        }

        if (other.gameObject.name.Contains("StrongerBombsPowerUp"))
        {
            other.gameObject.SetActive(false);
            bombPower++;
        }

        if (other.gameObject.name.Contains("MaxBombsPowerUp"))
        {
            other.gameObject.SetActive(false);
            this.IncreaseMaxBombs();
        }
    }

    private void TurnOnCollider()
    {
        if (bombList.Count != 0)
        {
            if (Vector3.Distance(bombList[bombsOnField - 1].transform.position, transform.position) > 0.9)
            {
                bombList[bombsOnField - 1].GetComponent<Collider>().enabled = true;
            }
        }
    }

}
