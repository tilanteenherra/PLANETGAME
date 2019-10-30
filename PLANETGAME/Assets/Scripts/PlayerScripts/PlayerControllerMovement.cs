using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerControllerMovement : MonoBehaviour
{

    // Class usage variables
    PlayerControls controls;
    WeaponDamage weaponDamage;

    //Component variables
    Animator animator;

    // Player variables
    public float moveSpeed;
    public float rotateSpeed;

    public bool attRoutineOn = false;
    
    // Move direction
    Vector2 moveInput;
    //Rotate direction
    Vector2 rotateInput;

    private void Awake()
    {
        
        animator = GetComponent<Animator>();
        weaponDamage = gameObject.transform.Find("WeaponCollider").GetComponent<WeaponDamage>();
        
        // Input Controller Related Things
        controls = new PlayerControls();

        controls.Gameplay.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => moveInput = Vector2.zero;
        
        controls.Gameplay.Rotate.performed += ctx => rotateInput = ctx.ReadValue<Vector2>();
        controls.Gameplay.Rotate.canceled += ctx => moveInput = Vector2.zero;
        
        //controls.Gameplay.MeleeAttack
            
    }
    
    private void FixedUpdate()
    {

        // Player movement
        float hMovetInput = moveInput.x;
        float vMoveInput = moveInput.y;
        
        var movement = new Vector3(hMovetInput, 0, vMoveInput);
        movement.Normalize();
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.Self);
        
        // Player rotation
        float hRotateInput = rotateInput.x;
        float vRotateInput = rotateInput.y;
    }

    void ShowChannelUI()
    {
        //Code here :D
    }

    void HideChannelUI()
    {
        //Code here :D
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

    public void OnSpell2()
    {
        Debug.Log("Spell2 pressed");
    }
}
