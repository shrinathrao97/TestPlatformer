using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_attack : MonoBehaviour
{
    #region Values

    private int comboState;
    private float timer;


    #endregion

    #region References
    private Animator anim;
    private Rigidbody rb;
    #endregion

    #region Unity Functions
    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();
        timer = 0.0f;
        comboState = 0;
    }

    void Update()
    {
        //Timer decrease logic
        timer = timer - Time.deltaTime;
        anim.SetFloat("ComboTimer", timer);


        //Combo state reset via timer
        if (timer <= 0f)
        {

            comboState = 0;
            anim.SetInteger("ComboState", 0);
            timer = 0f;
        }

        //Attacking Logic  

        if (Input.GetKeyDown(KeyCode.J) && timer > 0f)
        { 

            if (comboState == 2)
            {
                comboState = 3;
                anim.SetInteger("ComboState", 3);
                timer = .66f;
            }

            if (comboState == 1)
            {
                comboState = 2;
                anim.SetInteger("ComboState", 2);
                timer = .41f;
            }


        }

        if (Input.GetKeyDown(KeyCode.J) && timer <= 0f)
        {


            if (comboState == 0)
            {
                comboState = 1;
                anim.SetInteger("ComboState", 1);
                timer = .83f;
            }



        }


    }

    #endregion

    #region Attacking Functions


    #endregion
    




}
