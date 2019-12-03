using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun.Demo.SlotRacer.Utils;
using UnityEngine;
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

    // Float & Int variables
    public float moveSpeed = 10;
    public float controllerRotateSpeed = 100f;
    public float mouseRotateSpeed = 250f;
    private float buffTimer = 0f;
    public float dashSpeed = 8f;
    int noOfClicks = 0;
    
    // Boolean variables
    public bool attRoutineOn = false;
    bool canAttack = true;
    private bool walking;
    public bool interacting;
    bool canClick = true;
    bool dashing = false;
    public bool dashSmoke = false;

    // Vectors
    Vector3 endPosition;
    Vector3 moveAmount;
    Vector3 targetMoveAmount;
    Vector3 smoothMoveVelocity;

    public Renderer shovel;
    public Renderer shield;

    // Move direction
    Vector2 moveInput;
    //Rotate direction
    Vector2 rotateInput;

    private static readonly int Condition = Animator.StringToHash("condition");

    private void Awake()
    {
        anim = GetComponent<Animator>();
        weaponDamage = gameObject.transform.Find("WeaponCollider").GetComponent<WeaponDamage>();
        endPosition = new Vector3(0, 0, 0);
        rb = this.GetComponent<Rigidbody>();
        targetMoveAmount = Vector3.forward * dashSpeed;

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

        // Input Controller Related Things End Here --------------------------------------

    }
    
    private void FixedUpdate()
    {
        // Player movement
        float hMoveInput = moveInput.x;
        float vMoveInput = moveInput.y;

        var movement = new Vector3(hMoveInput, 0, vMoveInput);
        this.gameObject.transform.Translate(movement * moveSpeed * Time.deltaTime, Space.Self);

        // Player rotation below
        float hRotateInput = rotateInput.x;
        float vRotateInput = rotateInput.y;

        Vector2 rotate = new Vector2(0, hRotateInput) * controllerRotateSpeed * Time.deltaTime;
        this.gameObject.transform.Rotate(rotate, Space.Self);

        // Player rotation with mouse
        float hMouseInput = Input.GetAxis("Mouse X") * mouseRotateSpeed * Time.deltaTime;
        this.gameObject.transform.Rotate(0, hMouseInput,0, Space.Self);

        // Dashing
        if(dashing)
        {
            endPosition = transform.forward * 0.3f;
            this.transform.position = Vector3.Lerp(this.transform.position, this.transform.position + endPosition, Time.deltaTime);
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
                Debug.Log("GOT THE BUFF!");
            }
        }

        // Other stuff end --------------------------------------------------------------------------------------

        // Animations start -------------------------------------------------------------------------------------

        // Standing still
        if (moveInput.y == 0 && moveInput.x == 0)
        {

            anim.SetBool("running", false);
            anim.SetBool("runBack", false);
            anim.SetBool("walking", false);
            anim.SetBool("walkBack", false);
            anim.SetInteger("condition", 0);
            noOfClicks = 0;
        }

        // Running Forward
        else if (moveInput.y > 0.8f && !walking)
        {

            anim.SetBool("running", true);
            anim.SetInteger("condition", 1);
        }
        // Walking Forward
        else if (moveInput.y > 0 && moveInput.y < 0.8f || walking)
        {

            anim.SetBool("walking", true);
            anim.SetInteger("condition", 9);

        }

        // Walking Back
        else if (moveInput.y < 0 && moveInput.y > -0.8f || walking)
        {

            anim.SetBool("walkBack", true);
            anim.SetInteger("condition", 20);
            
        }

        // Running Back
        else if (moveInput.y <= -0.8f && !walking)
        {

            anim.SetBool("runBack", true);
            anim.SetInteger("condition", 19);
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
        attRoutineOn = true;
        //StartCoroutine(AttackRoutine());
        ComboStarter();
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
        attRoutineOn = true;
        StartCoroutine(SpecialAttackRoutine());
    }

    private void Spell2()
    {
        attRoutineOn = true;
        StartCoroutine(SpecialAttackRoutine2());
    }

    public void Charge()
    {
        dashing = true;
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

    public void WeaponShow()
    {
        shovel.enabled = true;
        shield.enabled = true;
    }

    public void WeaponHide()
    {
        shovel.enabled = false;
        shield.enabled = false;
    }

    public void ExitAnimation()
    {
        anim.SetInteger("condition", 98);
        noOfClicks = 0;
    }

    void ComboStarter()
    {
        if (canClick)
        {
            noOfClicks++;
        }

        if (noOfClicks >= 1)
        {
            anim.SetInteger("condition", 2);
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
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("AttackA") && noOfClicks >= 2)
        {
            anim.SetInteger("condition", 3);
            canClick = true;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("AttackB"))
        {
            anim.SetInteger("condition", 98);
            canClick = true;
            noOfClicks = 0;
        }


    }

    //IEnumerator AttackRoutine()
    //{
    //    canAttack = false;
    //    anim.SetBool("attackingA", true);
    //    anim.SetInteger("condition", 2);
    //    yield return new WaitForSeconds(1.7f);
    //    anim.SetInteger("condition", 0);
    //    anim.SetBool("attackingA", false);
    //    attRoutineOn = false;
    //    canAttack = true;
    //    weaponDamage.hitOnce = false;
    //}
    IEnumerator SpecialAttackRoutine()
    {
        canAttack = false;
        anim.SetBool("specialAttack", true);
        anim.SetInteger("condition", 25);
        yield return new WaitForSeconds(1.067f);
        anim.SetInteger("condition", 98);
        anim.SetBool("specialAttack", false);
        attRoutineOn = false;
        canAttack = true;
        //Temporarily disabled since it gave errors
        //weaponDamage.hitOnce = false;
    }

    IEnumerator SpecialAttackRoutine2()
    {
        canAttack = false;
        anim.SetBool("specialAttack2", true);
        anim.SetInteger("condition", 26);
        yield return new WaitForSeconds(1.8f);
        anim.SetInteger("condition", 98);
        anim.SetBool("specialAttack2", false);
        attRoutineOn = false;
        canAttack = true;
        //Temporarily disabled since it gave errors
        //weaponDamage.hitOnce = false;
    }
}
