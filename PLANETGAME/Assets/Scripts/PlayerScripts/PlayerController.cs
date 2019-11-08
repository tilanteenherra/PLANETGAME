using System;
using System.Collections;
using System.Collections.Generic;
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
    public float moveSpeed;
    public float controllerRotateSpeed = 100f;
    public float mouseRotateSpeed = 250f;
    

    public bool attRoutineOn = false;
    bool canAttack = true;
    
    // Move direction
    Vector2 moveInput;
    //Rotate direction
    Vector2 rotateInput;

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

        // Input Controller Related Things End Here

    }
    
    private void FixedUpdate()
    {
        // Add if statement to do stuff below if controller is connected
        // Player movement
        float hMovetInput = moveInput.x;
        float vMoveInput = moveInput.y;
        
        var movement = new Vector3(hMovetInput, 0, vMoveInput);
        movement.Normalize();
        this.gameObject.transform.Translate(movement * moveSpeed * Time.deltaTime, Space.Self);

        // Player rotation below
        float hRotateInput = rotateInput.x;
        float vRotateInput = rotateInput.y;

        Vector2 rotate = new Vector2(0, hRotateInput).normalized * controllerRotateSpeed * Time.deltaTime;
        this.gameObject.transform.Rotate(rotate, Space.Self);

        //vRotateInput = Mathf.Clamp(vRotateInput, -20, 20);

        // Add else if statement to do stuff below if controller is not connected and using keyboard and mouse
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * mouseRotateSpeed);
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

    public void MeleeAttack()
    {
        attRoutineOn = true;
        StartCoroutine(AttackRoutine());
    }

    public void Interactive()
    {
        Debug.Log("Interacting");
    }

    public void Spell1()
    {
        Debug.Log("Spell1 pressed");
    }

    public void Spell2()
    {
        Debug.Log("Spell2 pressed");
    }
}
