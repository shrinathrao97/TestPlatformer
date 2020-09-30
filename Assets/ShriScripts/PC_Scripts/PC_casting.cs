using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PC_casting : MonoBehaviour
{
    //This is an FFXIV mudra style casting system
    //There is an enum for Mudras and Spells


    #region References
    private Animator anim;
    private SpriteRenderer sr;
    private Rigidbody2D rb;

    [SerializeField]
    GameObject TenParticle;

    [SerializeField]
    GameObject ChiParticle;

    [SerializeField]
    GameObject JinParticle;

    [SerializeField]
    GameObject RaitonSpell;

    [SerializeField]
    GameObject KatonSpell;

    [SerializeField]
    GameObject DotonSpell;

    [SerializeField]
    GameObject HutonSpell;


    #endregion

    #region Values

    public float raitonProjectileSpeed;
    public float katonX, katonY;

    enum Mudras {

        ten = 1,
        chi = 2,
        jin = 3,

    }

    enum Spells {
        bunnyHat = 0,
        raiton = 1,
        katon = 2,
        doton = 3,
        huton = 4,
    }

    private List<int> SpellQueue;
    private List<int> Raiton;
    private List<int> Doton;
    private List<int> Katon;
    private List<int> Huton;

    #endregion

    #region Unity Functions

    // Start is called before the first frame update
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        SpellQueue = new List<int>(2);
        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("CastOH", false);
        anim.SetBool("CastGround", false);

        #region Spell Defenitions
        Raiton = new List<int> {(int)Mudras.ten, (int)Mudras.chi};
        Katon = new List<int> {(int)Mudras.chi, (int)Mudras.ten};
        Doton = new List<int> {(int)Mudras.ten, (int)Mudras.jin, (int)Mudras.chi};
        Huton = new List<int> {(int)Mudras.jin, (int)Mudras.chi, (int)Mudras.ten};
        #endregion
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.U))
        { 
            SpellQueue.Add((int)Mudras.ten);
            Instantiate(TenParticle, transform.position, Quaternion.identity);

            Debug.Log("Ten");
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            SpellQueue.Add((int)Mudras.chi);
            Instantiate(ChiParticle, transform.position, Quaternion.identity);
            Debug.Log("Chi");
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            SpellQueue.Add((int)Mudras.jin);
            Instantiate(JinParticle, transform.position, Quaternion.identity);
            Debug.Log("Jin");
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            EvalAndCastSpell();
        }
    }



    #endregion

    #region My Funcs

    private void EvalAndCastSpell()
    {
        int spell = EvaluateSpellQueue(); //Evaluate the spell Queue

        CastSpell(spell);

        SpellQueue.Clear(); //Kill our current list
    }

    private int EvaluateSpellQueue()
    {
        //Evaluates the spell queue
        if (SpellQueue.SequenceEqual(Raiton))
        {
            Debug.Log("Raiton");
            return (int)Spells.raiton;
            
        }

        if (SpellQueue.SequenceEqual(Katon))
        {
            Debug.Log("Katon");
            return (int)Spells.katon;
        }

        if (SpellQueue.SequenceEqual(Huton))
        {
            Debug.Log("Huton");
            return (int)Spells.huton;
        }

        if (SpellQueue.SequenceEqual(Doton))
        {
            Debug.Log("Doton");
            return (int)Spells.doton;
        }

        Debug.Log("Bunny Hat");
        return (int)Spells.bunnyHat; //Fall through to bunnyHat if we don't find a spell that works with the curr spellQueue
    }

    //Given an input spell, it casts the appropriate spell
    private void CastSpell(int spell)
    {
        

        if (spell == (int)Spells.raiton)
        {
            anim.SetBool("CastOH", true);
            GameObject temp = Instantiate(RaitonSpell, transform);

            if (sr.flipX == true)
            {
                temp.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1 * raitonProjectileSpeed, 1));
            }

            else
            {
                temp.GetComponent<Rigidbody2D>().AddForce(new Vector3(1 * raitonProjectileSpeed, 1));
            }
            

        }

        if (spell == (int)Spells.katon)
        {
            
            rb.AddForce(new Vector2(0, 3.5f), ForceMode2D.Impulse); // hardcoded jforce, it works.
            anim.SetBool("CastKaton", true);


        }

        if (spell == (int)Spells.huton)
        {
            anim.SetBool("CastGround", true);
            Instantiate(HutonSpell, transform, false);

        }

        if (spell == (int)Spells.doton)
        {
            anim.SetBool("CastGround", true);
            GameObject temp = Instantiate(DotonSpell);
            temp.GetComponent<Transform>().position = transform.position;
        }
    }


    //Animation Functions
    public void CastOHReset()
    {
        anim.SetBool("CastOH", false);

    }

    public void CastGroundReset()
    {
        Debug.Log("CastGroundReset called");

        anim.SetBool("CastGround", false);

    }

    public void CastKaton()
    {

        
        GameObject temp = Instantiate(KatonSpell, transform);

        if (sr.flipX == true)
        {
            temp.GetComponent<Rigidbody2D>().AddForce(new Vector3(-katonX, -katonY));
        }

        else
        {
            temp.GetComponent<Rigidbody2D>().AddForce(new Vector3(katonX, -katonY));
        }
        
    }

    public void CastKatonReset()
    {
        Debug.Log("CastKatonReset called");
        anim.SetBool("CastKaton", false);
    }

    #endregion

    #region Coroutines

    #endregion
}
