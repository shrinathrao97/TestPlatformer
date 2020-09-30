using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_movement : MonoBehaviour
{
    #region Values
    //Values
    public float JumpForce;
    public float MoveSpeed;
    #endregion

    #region References

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;
    #endregion

    #region Checks
    //Checks
    public bool touchingGround;
    public bool crouching;
    public bool jumping;
    public bool canJump;
    #endregion

    #region Unity Functions



    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        canJump = true;
        anim = gameObject.GetComponent<Animator>();
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        #region Movement
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();

        }

        if (Input.GetKey(KeyCode.A))
        {
            MoveL();

        }

        if (Input.GetKey(KeyCode.S))
        {

            Crouch();

        }

        if (Input.GetKey(KeyCode.D))
        { 
            MoveR();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {

            Attack_Light();

        }

        if (Input.GetKeyDown(KeyCode.K))
        {

            Attack_Heavy();

        }

        if (!Input.anyKey)
        {

            Idle();

        }
        #endregion



    }

    private void Update()
    {
        GroundCheck();

        if (Input.GetKeyDown(KeyCode.Escape)) {

            Application.Quit();

        }
    }






    #endregion

    #region Player Movement Functions

    void Jump()
    {

        if (canJump == true)
        {
            rb.AddForce(new Vector2(0, 1 * JumpForce), ForceMode2D.Impulse);
            jumping = true;
            canJump = false;
            anim.SetBool("Jump", true);
        }

    }

    void MoveR()
    {
        float temp = 1 * MoveSpeed * Time.deltaTime;

        anim.SetFloat("Speed", temp);

        if(sr.flipX == true) {

            sr.flipX = false;
        }

        transform.Translate(new Vector2(temp, 0));



    }

    void MoveL()
    {
        float temp = -1 * MoveSpeed * Time.deltaTime;
        sr.flipX = true;
        anim.SetFloat("Speed", temp);

        transform.Translate(new Vector2(temp, 0));
    }

    void Crouch()
    {
        //Do animation and hitbox work here...
    }

    void Idle()
    {

        anim.SetFloat("Speed", 0f);

    }

    #endregion

    #region Player Attack Functions

    void Attack_Heavy()
    {
        
    }

    void Attack_Light()
    {

    }

    #endregion

    #region Player Checker Functions

    private void GroundCheck()
    {
        
        
        LayerMask layerMask = 1 << 8;
        layerMask = ~layerMask;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, .2f);

        //if the Raycast hits something below the PC
        if (hit.collider != null)
        {
            touchingGround = true;
            jumping = false;
            canJump = true;
            anim.SetBool("Jump", false);


        }

        // You're currently in the air somehow
        else
        {

            touchingGround = false;

        }
    
    }
    #endregion






}
