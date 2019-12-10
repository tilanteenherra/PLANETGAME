using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun.Demo.SlotRacer.Utils;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerController : MonoBehaviour
{

    // Class usage variables
    PlayerControls controls;
    WeaponDamage weaponDamage;

    //Component variables
    Animator anim;
    Rigidbody rb;

    // Float & Int variables ----------
    public float moveSpeed = 10;
    public float controllerRotateSpeed = 100f;
    public float mouseRotateSpeed = 250f;
    private float buffTimer = 0f;
    public float dashSpeed = 8f;
    public int noOfClicks = 0;
    
    // Boolean variables
    //------ PUBLIC ------
    public bool attRoutineOn = false;
    public bool canDash = true;
    public bool keepPlace = false;
    public bool usingSpell = false;
    public bool interacting;
    public bool dashSmoke = false;
    public bool paused = false;
    public bool myIdle = false;
    public bool myWFor = false;
    public bool myWBack = false;
    public bool myRFor = false;
    public bool myRBack = false;
    public bool useMask = false;

    //------ PRIVATE ------
    bool canAttack = true;
    bool walking;
    bool canClick = true;
    bool dashing = false;

    // Vectors ----------
    Vector3 endPosition;
    Vector3 moveAmount;
    Vector3 targetMoveAmount;
    Vector3 smoothMoveVelocity;
    Vector3 playerPos;
    
    // Move direction
    Vector2 moveInput;
    //Rotate direction
    Vector2 rotateInput;

    // Renders ---------
    public Renderer shovel;
    public Renderer shield;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        //weaponDamage = gameObject.transform.Find("WeaponCollider").GetComponent<WeaponDamage>();
        endPosition = new Vector3(0, 0, 0);
        rb = GetComponent<Rigidbody>();
        noOfClicks = 0;
        canClick = true;
        shovel = GameObject.Find("Shovel").GetComponent<Renderer>();
        shield = GameObject.Find("Shield").GetComponent<Renderer>();

        // Input Controller Related Things Start Here ------------------------------------
        
        controls = new PlayerControls();
        
        // Movement
        controls.Gameplay.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => moveInput = Vector2.zero;
        
        // Rotation
        controls.Gameplay.Rotate.performed += ctx => rotateInput = ctx.ReadValue<Vector2>();
        controls.Gameplay.Rotate.canceled += ctx => rotateInput = Vector2.zero;

        // Other Stuff
        controls.Gameplay.MeleeAttack.performed += ctx => MeleeAttack();
        controls.Gameplay.Spell1.performed += ctx => Spell1();
        controls.Gameplay.Spell2.performed += ctx => Spell2();
        controls.Gameplay.Walk.performed += ctx => Walk();
        controls.Gameplay.Walk.canceled += ctx => Run();
        controls.Gameplay.Interactive.performed += ctx => OnInteract();
        controls.Gameplay.Interactive.canceled += ctx => NoInteract();
        controls.Gameplay.Pause.performed += ctx => Pause();

        // Input Controller Related Things End Here --------------------------------------

    }
    
    private void FixedUpdate()
    {
        // Player movement
        float hMoveInput = moveInput.x;
        float vMoveInput = moveInput.y;

        if(!usingSpell)
        {
            var movement = new Vector3(hMoveInput, 0, vMoveInput);
            gameObject.transform.Translate(movement * moveSpeed * Time.deltaTime, Space.Self);

            // Player rotation below
            float hRotateInput = rotateInput.x;
            float vRotateInput = rotateInput.y;

            Vector2 rotate = new Vector2(0, hRotateInput) * controllerRotateSpeed * Time.deltaTime;
            gameObject.transform.Rotate(rotate, Space.Self);

            // Player rotation with mouse
            float hMouseInput = Input.GetAxis("Mouse X") * mouseRotateSpeed * Time.deltaTime;
            gameObject.transform.Rotate(0, hMouseInput, 0, Space.Self);
        }


        // Dashing
        if(dashing)
        {
            endPosition = transform.forward * 30f;
            transform.position = Vector3.Lerp(transform.position, transform.position + endPosition, Time.deltaTime);
        }

        if (keepPlace)
        {
            transform.position = playerPos;
        }

    }

    private void Update()
    {
        // Other stuff start ------------------------------------------------------------------------------------
        if (interacting)
        {
            buffTimer += Time.deltaTime;
            
            if (buffTimer >= 5f)
            {
                // Gets buff code

            }
        }
        // Other stuff end --------------------------------------------------------------------------------------

        // Animations start -------------------------------------------------------------------------------------

        // Standing still
        if (moveInput.y == 0 && moveInput.x == 0 && myIdle == false)
        {
            myRFor = false;
            myWFor = false;
            myIdle = true;
            myRBack = false;
            myWBack = false;
            anim.SetBool("running", false);
            anim.SetBool("walking", false);
            anim.SetBool("walkBack", false);
            anim.SetBool("runBack", false);
            anim.SetInteger("condition", 98);
            noOfClicks = 0;
            canClick = true;
            keepPlace = false;
        }

        // Running Forward
        else if (moveInput.y > 0.7f && !walking && myRFor == false)
        {
            myRFor = true;
            myWFor = false;
            myIdle = false;
            myRBack = false;
            myWBack = false;
            anim.SetBool("walking", false);
            anim.SetBool("walkBack", false);
            anim.SetBool("runBack", false);
            anim.SetBool("running", true);
            useMask = true;
            anim.SetInteger("condition", 15);

        }
        // Walking Forward
        else if (moveInput.y > 0 && moveInput.y < 0.7f || walking && myWFor == false)
        {
            myRFor = false;
            myWFor = true;
            myIdle = false;
            myRBack = false;
            myWBack = false;
            anim.SetBool("running", false);
            anim.SetBool("walkBack", false);
            anim.SetBool("runBack", false);
            anim.SetBool("walking", true);
            useMask = true;
            anim.SetInteger("condition", 13);
        }
        
        // Running Back
        else if (moveInput.y <= -0.7f && !walking && myRBack == false)
        {
            myRFor = false;
            myWFor = false;
            myIdle = false;
            myRBack = true;
            myWBack = false;
            anim.SetBool("walking", false);
            anim.SetBool("running", false);
            anim.SetBool("walkBack", false);
            anim.SetBool("runBack", true);
            useMask = true;
            anim.SetInteger("condition", 12);
        }

        // Walking Back
        else if (moveInput.y < 0 && moveInput.y > -0.7f || walking && myWBack == false)
        {
            myRFor = false;
            myWFor = false;
            myIdle = false;
            myRBack = false;
            myWBack = true;
            anim.SetBool("walking", false);
            anim.SetBool("running", false);
            anim.SetBool("walkBack", true);
            anim.SetBool("runBack", false);
            useMask = true;
            anim.SetInteger("condition", 11);
        }

        // Animations end ---------------------------------------------------------------------------------------
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    private void MeleeAttack()
    {
        Debug.Log("Melee");
        ComboStarter();
    }
    // Used to open "PauseMenu"
    void Pause()
    {
        Debug.Log("Online games can't be paused mom!");
        if (paused)
        {
            paused = false;
        }
        else if (!paused)
        {
            paused = true;
        }
    }

    private void OnInteract()
    {
        interacting = true;
    }

    void NoInteract()
    {
        interacting = false;
        buffTimer = 0f;
    }

    private void Spell1()
    {
        if (canAttack)
        {
            if (canDash)
            {
                attRoutineOn = true;
                StartCoroutine(SpecialAttackRoutine());
            }
        }
    }

    private void Spell2()
    {
        if (canAttack)
        {
            if (canDash)
            {
                attRoutineOn = true;
                StartCoroutine(SpecialAttackRoutine2());   
            }
        }
    }
    
    private void Walk()
    {
        
        walking = true;
        moveSpeed *= 0.5f;
    }

    private void Run()
    {
        walking = false;
        moveSpeed *= 2f;
    }

    public void Charge()
    {
        dashing = true;
    }
    
    public void SetChargeFalse()
    {
        dashing = false;
        noOfClicks = 0;
    }
    
    public void SmokeOn()
    {
        GetComponent<DashSmokeScripts>().smokeOn = true;
    }

    public void SmokeOff()
    {
        GetComponent<DashSmokeScripts>().smokeOn = false;
        noOfClicks = 0;
    }

    public void WeaponShow()
    {
        // Enables renderers
        shovel.enabled = true;
        shield.enabled = true;
    }

    public void WeaponHide()
    {
        // Disables renderers
        shovel.enabled = false;
        shield.enabled = false;
    }

    public void ExitAnimation()
    {
        anim.SetInteger("condition", 98);
        noOfClicks = 0;
        keepPlace = false;
    }

    void ComboStarter()
    {       
        if (canClick)
        {
            noOfClicks++;
        }

        //if(noOfClicks >= 1 && ((anim.GetBool("running") == true) || (anim.GetBool("walking") == true)))
        if (noOfClicks >= 1 && useMask == true)
        {   
            anim.SetInteger("condition", 30);
            canDash = false;
        }

        if (noOfClicks >= 1 && (anim.GetBool("running") == false) && (anim.GetBool("walking") == false) && (anim.GetBool("walkBack") == false) && (anim.GetBool("runBack") == false))
        {
            if(canAttack)
            {
                anim.SetInteger("condition", 2);
                canDash = false;
                playerPos = transform.position;
                keepPlace = true;
            }
        }
    }

    public void ComboCheck()
    {
        canClick = false;

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("AttackA") && noOfClicks == 1)
        {
            anim.SetInteger("condition", 98);
            canClick = true;
            noOfClicks = 0;
            canDash = true;
            keepPlace = false;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("AttackA") && noOfClicks >= 2)
        {
            anim.SetInteger("condition", 3);
            canClick = true;
            canDash = false;
            keepPlace = true;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("AttackB"))
        {
            anim.SetInteger("condition", 98);
            canClick = true;
            noOfClicks = 0;
            canDash = true;
            keepPlace = false;
        }
        else if (anim.GetCurrentAnimatorStateInfo(1).IsName("AttackA") && noOfClicks == 1)
        {
            anim.SetInteger("condition", 98);
            canClick = true;
            noOfClicks = 0;
            canDash = true;
        }
        else if (anim.GetCurrentAnimatorStateInfo(1).IsName("AttackA") && noOfClicks >= 2)
        {
            anim.SetInteger("condition", 31);
            canClick = true;
            canDash = false;
        }
        else if (anim.GetCurrentAnimatorStateInfo(1).IsName("AttackB"))
        {
            anim.SetInteger("condition", 98);
            canClick = true;
            noOfClicks = 0;
            canDash = true;
        }
    }

    IEnumerator SpecialAttackRoutine()
    {
        anim.SetBool("walking", false);
        anim.SetBool("running", false);
        anim.SetBool("walkBack", false);
        anim.SetBool("runBack", false);
        usingSpell = true;
        canAttack = false;
        //anim.SetBool("specialAttack", true);
        anim.SetBool("special1", true);
        anim.SetInteger("condition", 25);
        yield return new WaitForSeconds(0.8f);
        anim.SetBool("special1", false);
        anim.SetInteger("condition", 98);
        //anim.SetBool("specialAttack", false);
        attRoutineOn = false;
        usingSpell = false;
        canAttack = true;
        noOfClicks = 0;
        //Temporarily disabled since it gave errors
        //weaponDamage.hitOnce = false;
    }

    IEnumerator SpecialAttackRoutine2()
    {
        anim.SetBool("walking", false);
        anim.SetBool("running", false);
        anim.SetBool("walkBack", false);
        anim.SetBool("runBack", false);
        usingSpell = true;
        canAttack = false;
        //anim.SetBool("specialAttack2", true);
        anim.SetBool("special2", true);
        anim.SetInteger("condition", 26);
        yield return new WaitForSeconds(0.6f);
        anim.SetBool("special2", false);
        anim.SetInteger("condition", 98);
        //anim.SetBool("specialAttack2", false);
        attRoutineOn = false;
        usingSpell = false;
        canAttack = true;
        noOfClicks = 0;
        //Temporarily disabled since it gave errors
        //weaponDamage.hitOnce = false;
    }
}
