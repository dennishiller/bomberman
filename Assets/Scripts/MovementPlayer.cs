using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 2.0f;
    public GameObject bomb;
    private int maxBombs = 2;
    private int bombsOnField=0;
    private List<GameObject> bombList = new List<GameObject>();
    private float cooldownTime=1;
    private float nextBombTime=0;
    private new Collider collider;
    private GridScript grid;

    private void Awake()
    {
        grid = FindObjectOfType<GridScript>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        collider = GetComponent<Collider>();
    }

    void Update()
    {
        PlayerMovement();
        PlaceBomb();
        CheckBomb();
        TurnOnCollider();

    }

    private void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = transform.right * speed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = -transform.right * speed;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = new Vector3(0, 0, 1) * speed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = new Vector3(0, 0, -1) * speed;
        }
        else
        {
            rb.velocity = new Vector3(0, 0, 0);
        }

        
    }

    private void PlaceBomb()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!(maxBombs == bombsOnField) && Time.time>nextBombTime)
            {
                bombList.Add(Instantiate(bomb, grid.GetNearestPointOnGrid(transform.position) , Quaternion.identity));
                nextBombTime = Time.time + cooldownTime;
                bombsOnField++;
            }
        }    
    }

    public void BombExploded()
    {
        Debug.Log("BOOM");
        bombList.RemoveAt(0);
        bombsOnField--;
        
    }

    private void CheckBomb()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            Debug.Log(bombList[bombsOnField - 1].transform.position);
        }
    }

    public void IncreaseMaxBombs()
    {
        this.maxBombs++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PowerUp"))
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
