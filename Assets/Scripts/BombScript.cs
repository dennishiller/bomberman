using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    private float lifeTime = 3;
    //public GameObject player;
    private MovementPlayer mp;
    new Collider collider;
 
    //IEnumerator countdown()
    //{
    //    yield return new WaitForSecondsRealtime(lifeTime);
    //    isActive = false;
    //}

    void Awake()
    {
        //StartCoroutine(countdown());
        Destroy(gameObject, lifeTime);
        collider = GetComponent<Collider>();
        collider.enabled = false;
        mp = FindObjectOfType<MovementPlayer>();

    }

    private void OnDestroy()
    {
        mp.BombExploded();
        //player.GetComponent<MovementPlayer>().BombExploded();
        //Debug.Log("Destroy");
    }


}
