using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float speed = 2.0f;
    private Vector3 jump;
    public float jumpForce;
    public bool isGrounded;
    public bool jumpedOnce = true;
    private Animator PlayerAnimator;
    [SerializeField] private GameObject rim;
    private bool hang = false;
    private RigidbodyConstraints originalConstraints;

    void Start()
    {
        Time.timeScale = 2.5f;
        rb = GetComponent<Rigidbody>();
        PlayerAnimator = GetComponent<Animator>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
        originalConstraints = rb.constraints;
    }

    void Update() // yönler ters çünkü avatar ters duruyor.
    {
        // hang
        if(hang)
        {
            rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        }        

        // jump version 2
        if (transform.position.y < 1.5f)
        {
            PlayerAnimator.SetBool("IsGrounded", true);
            Debug.Log("1.5f den aşağıda!");

            if (transform.position.y < 0)
            {
                Debug.Log("0f den aşağıda!");

                if (Input.GetKeyDown(KeyCode.Space) && jumpedOnce)
                {
                    rb.AddForce(jump * jumpForce, ForceMode.Impulse);
                    // jumpedOnce = false; şimdilik kapıyorum!
                }

            }
            else
            {
                jumpedOnce = true; 
            }
        }        
        else if (transform.position.y > 2.5f)
        {
            Debug.Log("2.5f den yukarda!");
            PlayerAnimator.SetBool("IsGrounded", false);
        }
        

        // jump
        //if (isGrounded)
        //{
        //    PlayerAnimator.SetBool("IsGrounded", true);
        //}
        //else
        //{
        //    PlayerAnimator.SetBool("IsGrounded", false);
        //}

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    // rb.AddForce(jump * jumpForce, ForceMode.Impulse);

        //    if (isGrounded)
        //    {
        //        isGrounded = false;
        //        rb.AddForce(jump * jumpForce, ForceMode.Impulse);
        //        PlayerAnimator.SetTrigger("JumpWithBall");
        //    }
        //    else
        //    {
        //        PlayerAnimator.SetTrigger("JumpWithBall");
        //    }
            
        //}

        // movements 
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.back * speed * Time.deltaTime;
        }
    }

    public void Hang()
    {
        hang = true;
    }
    public void StopHanging()
    {
        hang = false;
        rb.constraints = originalConstraints;
    }


    //void OnCollisionEnter()
    //{
    //    // yer ile temas halinde olduğunda diye özellikle belirt.
    //    isGrounded = true;
    //}    

    //void OnCollisionExit()
    //{
    //    isGrounded = false;

    //}
}
