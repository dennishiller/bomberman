using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    private float lifeTime = 3;
    private MovementPlayer mp;
    new Collider collider;
    public GameObject explosionPrefab;
    public LayerMask levelMask;
    private bool isExploded = false;


    private void Start()
    {
        collider = GetComponent<Collider>();
        collider.enabled = false;
        mp = FindObjectOfType<MovementPlayer>();
        Invoke("Explode", 3f);
    }

    private void Update()
    {
 
    }

    void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity); //1
        StartCoroutine(CreateExplosions(Vector3.forward));
        StartCoroutine(CreateExplosions(Vector3.right));
        StartCoroutine(CreateExplosions(Vector3.back));
        StartCoroutine(CreateExplosions(Vector3.left));

        GetComponent<MeshRenderer>().enabled = false; //2
        isExploded = true;
        explosionPrefab.GetComponent<Collider>().enabled = false;
        //transform.Find("Collider").gameObject.SetActive(false); //3
        Destroy(gameObject, .3f); //4
    }

    private IEnumerator CreateExplosions(Vector3 direction)
    {
        //1
        for (int i = 1; i < 3; i++)
        {
            //2
            RaycastHit hit;
            //3
            Physics.Raycast(transform.position + new Vector3(0, .5f, 0), direction, out hit,
              i, levelMask);

            //4
            if (!hit.collider)
            {
                Instantiate(explosionPrefab, transform.position + (i * direction),
                  //5 
                  explosionPrefab.transform.rotation);
                //6
            }
            else
            { //7
                if (hit.collider.tag.Equals("breakable"))
                {
                    Destroy(hit.collider.gameObject);
                }
                break;
            }

            //8
            yield return new WaitForSeconds(.05f);
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered OnTrigger");
        if (!isExploded && other.CompareTag("Explosion"))
        { // 1 & 2  
            CancelInvoke("Explode"); // 2
            Explode(); // 3
        }  
    }


    //void Awake()
    //{
    //    //StartCoroutine(countdown());
    //    Destroy(gameObject, lifeTime);
    //    collider = GetComponent<Collider>();
    //    collider.enabled = false;
    //    mp = FindObjectOfType<MovementPlayer>();

    //}

    private void OnDestroy()
    {
        mp.BombExploded();
        //player.GetComponent<MovementPlayer>().BombExploded();
        //Debug.Log("Destroy");
    }


}
