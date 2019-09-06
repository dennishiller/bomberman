using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    Rigidbody rb;
    public GameObject bomb;
    private int maxBombs = 2;
    private int bombsOnField=0;
    private List<GameObject> bombList = new List<GameObject>();
    private float cooldownTime=1;
    private float nextBombTime=0;
    private new Collider collider;
    private GridScript grid;
    private movement movementScript;

    private void Awake()
    {
        grid = FindObjectOfType<GridScript>();
        movementScript = FindObjectOfType<movement>();
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

    }

  
    private void PlaceBomb()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!(maxBombs == bombsOnField) && Time.time>nextBombTime)
            {
                bombList.Add(Instantiate(bomb, grid.GetNearestPointOnGrid(transform.position), Quaternion.identity));
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

    public void IncreaseMaxBombs()
    {
        this.maxBombs++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PowerUp"))
        {
            if (other.gameObject.name == "SpeedPowerUp")
            {
                other.gameObject.SetActive(false);
                movementScript.speed += 0.5f;
                
                Debug.Log(movementScript.speed);
            }

            if (other.gameObject.name == "MaxBombsPowerUp")
            {
                Debug.Log("if drinne bomb");
                other.gameObject.SetActive(false);
                this.IncreaseMaxBombs();
            }
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
