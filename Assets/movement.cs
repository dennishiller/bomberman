using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class movement : MonoBehaviour
{
    private Animator animator;
    public float moveVertical;
    public float moveHorizontal;
    public float speed = 10;
    public float turnSpeed = 100;
    public bool FirstPerson;

    public Camera globalCam;
    public Camera playerCam;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        FirstPerson = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("c"))
        {
            // Umschalten auf Firstperson
            if (globalCam.gameObject.activeSelf)
            {
                globalCam.gameObject.SetActive(false);
                playerCam.gameObject.SetActive(true);
                FirstPerson = false;

            }
            else
            {
                globalCam.gameObject.SetActive(true);
                playerCam.gameObject.SetActive(false);
                FirstPerson = true;
            }
        }
        
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
        
        if (!FirstPerson)
        {
            if (moveVertical > 0)
            {
                animator.SetBool("isRunning", true);
                animator.SetFloat("starter", moveVertical);
                /*this.gameObject.transform.eulerAngles = new Vector3(
                    this.gameObject.transform.eulerAngles.x,
                    0,
                    this.gameObject.transform.eulerAngles.z
                );*/
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }
            else if (moveVertical < 0)
            {
                animator.SetBool("isRunning", true);
                animator.SetFloat("starter", -moveVertical);
                /*  this.gameObject.transform.eulerAngles = new Vector3(
                      this.gameObject.transform.eulerAngles.x,
                      180,
                      this.gameObject.transform.eulerAngles.z
                  );*/
                transform.Translate(Vector3.back * Time.deltaTime * speed);
            }


            if (moveHorizontal < 0)
            {
                animator.SetBool("isRunning", true);
                transform.Rotate(Vector3.down, turnSpeed * Time.deltaTime);
                animator.SetFloat("starter", -moveHorizontal);
          
            }
            else if (moveHorizontal > 0)
            {
                animator.SetBool("isRunning", true);
                animator.SetFloat("starter", moveHorizontal);
                transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
            }

        }
        // Movement first Person
        else
        {
            if (moveVertical > 0)
            {
                animator.SetBool("isRunning", true);
                animator.SetFloat("starter", moveVertical);
                this.gameObject.transform.eulerAngles = new Vector3(
                    this.gameObject.transform.eulerAngles.x,
                    0,
                    this.gameObject.transform.eulerAngles.z
                );
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }
            else if (moveVertical < 0)
            {
                animator.SetBool("isRunning", true);
                animator.SetFloat("starter", -moveVertical);
                this.gameObject.transform.eulerAngles = new Vector3(
                    this.gameObject.transform.eulerAngles.x,
                      180,
                    this.gameObject.transform.eulerAngles.z
                  );
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }


            if (moveHorizontal < 0)
            {
                animator.SetBool("isRunning", true);
                animator.SetFloat("starter", -moveHorizontal);
                this.gameObject.transform.eulerAngles = new Vector3(
                     this.gameObject.transform.eulerAngles.x,
                     270,
                     this.gameObject.transform.eulerAngles.z
                 );
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }
            else if (moveHorizontal > 0)
            {
                animator.SetBool("isRunning", true);
                animator.SetFloat("starter", moveHorizontal);
                this.gameObject.transform.eulerAngles = new Vector3(
                     this.gameObject.transform.eulerAngles.x,
                     90,
                     this.gameObject.transform.eulerAngles.z
                 );
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }

        }
        
        if (moveVertical == 0 && moveHorizontal == 0)
        {
            animator.SetBool("isRunning", false);
        }
    }
}