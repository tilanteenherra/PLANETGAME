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

    // Player variables
    public float moveSpeed = 10;
    public float controllerRotateSpeed = 100f;
    public float mouseRotateSpeed = 250f;
    private float buffTimer = 0f;
    

    public bool attRoutineOn = false;
    bool canAttack = true;
    private bool walking;
    private bool interacting;

    // Move direction
    Vector2 moveInput;
    //Rotate direction
    Vector2 rotateInput;
    private static readonly int Condition = Animator.StringToHash("condition");

    private void Awake()
    {
        anim = GetComponent<Animator>();
        weaponDamage = gameObject.transform.Find("WeaponCollider").GetComponent<WeaponDamage>();

        // Input Controller Related Things Start Here
        
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

        // Input Controller Related Things End Here

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

        //vRotateInput = Mathf.Clamp(vRotateInput, -20, 20);

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
        
        if(moveInput.y == 0)
        {
            anim.SetBool("running", false);
            anim.SetBool("runBack", false);
            anim.SetBool("walking", false);
            anim.SetBool("walkBack", false);
            anim.SetInteger("condition", 0);
            //anim.SetInteger(Condition, 0);
        }
        
        else if(moveInput.y > 0.8f && !walking)
        {
            if(anim.GetBool("attackingA") == true || anim.GetBool("attackingB") == true)
            {
                return;
            }
            else if(anim.GetBool("attackingA") == false && anim.GetBool("attackingB") == false)
            {
                anim.SetBool("running", true);
                anim.SetInteger("condition", 1);
            }
        }
        else if (moveInput.y > 0 && moveInput.y < 0.8f || walking)
        {
            if(anim.GetBool("attackingA") == true || anim.GetBool("attackingB") == true)
            {
                return;
            }
            if(anim.GetBool("attackingA") == false && anim.GetBool("attackingB") == false)
            {
                anim.SetBool("walking", true);
                anim.SetInteger("condition", 9);
            }

            if (anim.GetBool("running") == true)
            {
                anim.SetBool("running", false);
                anim.SetBool("walking", true);
                anim.SetInteger("condition", 9);
            }
        }
        else if (moveInput.y < 0 && moveInput.y > -0.8f || walking)
        {
            if(anim.GetBool("attackingA") == true || anim.GetBool("attackingB") == true)
            {
                return;
            }
            if(anim.GetBool("attackingA") == false && anim.GetBool("attackingB") == false)
            {
                anim.SetBool("walkBack", true);
                anim.SetInteger("condition", 20);
            }

            if (anim.GetBool("running") == true)
            {
                anim.SetBool("running", false);
                anim.SetBool("walkBack", true);
                anim.SetInteger("condition", 20);
            }
        }
        else if(moveInput.y <= -0.8f && !walking)
        {
            if(anim.GetBool("attackingA") == true || anim.GetBool("attackingB") == true)
            {
                return;
            }
            if(anim.GetBool("attackingA") == false && anim.GetBool("attackingB") == false)
            {
                anim.SetBool("runBack", true);
                anim.SetInteger("condition", 19);
            }
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

    IEnumerator AttackRoutine()
    {
        canAttack = false;
        anim.SetBool("attackingA", true);
        anim.SetInteger("condition", 2);
        yield return new WaitForSeconds(1.7f);
        anim.SetInteger("condition", 0);
        anim.SetBool("attackingA", false);
        attRoutineOn = false;
        canAttack = true;
        weaponDamage.hitOnce = false;
    }

    private void MeleeAttack()
    {
        attRoutineOn = true;
        StartCoroutine(AttackRoutine());
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
        Debug.Log("Spell1 pressed");
    }

    private void Spell2()
    {
        Debug.Log("Spell2 pressed");
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
}
