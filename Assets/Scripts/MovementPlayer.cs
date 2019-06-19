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
    private Collider collider;
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
        BombExploded();
        //TurnOnCollider();

    }

    void PlayerMovement()
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

    void PlaceBomb()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!(maxBombs == bombsOnField) && Time.time>nextBombTime)
            {
                bombList.Add(Instantiate(bomb, grid.GetNearestPointOnGrid(transform.position) , Quaternion.identity));
                nextBombTime = Time.time + cooldownTime;
                bombsOnField++;
                collider.enabled = false;
                Debug.Log("Test");
            }
        }    
    }

    public void BombExploded()
    {
        if (bombList[0]==null)
        {
            bombList.RemoveAt(0);
            bombsOnField--;
        }
    }

    private void CheckBomb()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            Debug.Log(bombList.Count);
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
            Debug.Log("TEST");
            other.gameObject.SetActive(false);
            this.IncreaseMaxBombs();
        }
    }

    //private void TurnOnCollider()
    //{
    //    if (Vector3.Distance(bombList[bombsOnField].GetComponent<Transform>().position, transform.position) > 1)
    //    {
    //        Debug.Log("ENTFERNT");
    //    }
    //    else
    //    {
    //        Debug.Log("NEIN");
    //    }
    //}

}
