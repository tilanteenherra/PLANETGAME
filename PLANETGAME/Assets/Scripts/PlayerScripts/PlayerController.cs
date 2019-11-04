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
    Animator animator;

    // Player variables
    public float moveSpeed;
    public float rotateSpeed;
    private float mouseX;
    private float mouseY;
    private float mouseVerticalRotation;

    public bool attRoutineOn = false;
    
    // Move direction
    Vector2 moveInput;
    //Rotate direction
    Vector2 rotateInput;

    private void Awake()
    {
        
        animator = GetComponent<Animator>();
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

        // Player movement
        float hMovetInput = moveInput.x;
        float vMoveInput = moveInput.y;
        
        var movement = new Vector3(hMovetInput, 0, vMoveInput);
        movement.Normalize();
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.Self);
        
        // Player rotation below
        // Rotation for x and y values, y not implemented yet:
        // Vector2 rotate = new Vector2(rotateInput.y, rotateInput.x) * rotateSpeed * Time.deltaTime;
        Vector2 rotate = new Vector2(0, rotateInput.x) * rotateSpeed * Time.deltaTime;
        transform.Rotate(rotate, Space.Self);
        
        //vRotateInput = Mathf.Clamp(vRotateInput, -20, 20);
    }

    private void Update()
    {
        /*
        mouseX += Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
        mouseY -= Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;
        mouseY = Mathf.Clamp(mouseY, -20, 20);
        
        transform.rotation = Quaternion.Euler(0, mouseX, 0);
        
        //target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        //player.rotation = Quaternion.Euler(0, mouseX, 0);
        
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed);
        mouseVerticalRotation += Input.GetAxis("Mouse Y") * Time.deltaTime * rotateSpeed;
        mouseVerticalRotation = Mathf.Clamp(mouseVerticalRotation, -20, 20);
        */
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
        animator.SetBool("attacking", true);
        animator.SetInteger("condition", 2);
        yield return new WaitForSeconds(1);
        animator.SetInteger("condition", 0);
        animator.SetBool("attacking", false);
        attRoutineOn = false;
        weaponDamage.hitOnce = false;
    }

    public void MeleeAttack()
    {
        attRoutineOn = true;
        StartCoroutine(AttackRoutine());
    }

    public void Rotate()
    {
        var rotateInput = controls.Gameplay.Rotate.ReadValue<Vector2>();

        var rotation = new Vector3
        {
          y = rotateInput.x,
          x = rotateInput.y
        };
        
        rotation.Normalize();

        // controls.Gameplay.Rotate.performed += ctx => rotate = ctx.ReadValue<Vector2>();
        // controls.Gameplay.Rotate.canceled += ctx => rotate = Vector2.zero;
    }

    public void Interactive()
    {
        //Something
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
