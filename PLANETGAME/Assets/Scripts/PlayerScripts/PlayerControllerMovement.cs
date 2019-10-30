using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerControllerMovement : MonoBehaviour
{

    PlayerControls controls;
    WeaponDamage weaponDamage;

    //Transform playerCamera;

    Animator animator;

    public float moveSpeed;
    public float rotateSpeed;

    public bool attRoutineOn = false;
    //private PlayerControls.IGameplayActions _gameplayActionsImplementation;

    private void Awake()
    {

        animator = GetComponent<Animator>();
        
        weaponDamage = gameObject.transform.Find("WeaponCollider").GetComponent<WeaponDamage>();

/*
        controls.Gameplay.Rotate.performed += ctx => rotate = ctx.ReadValue<Vector2>();
        controls.Gameplay.Rotate.canceled += ctx => rotate = Vector2.zero;
        */
    }
    
    private void FixedUpdate()
    {

        // Player movement
        Move();

        // Player rotation
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
        if (controls == null)
        {
            controls = new PlayerControls();
        }
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

    public void Move()
    {
        var movementInput = controls.Gameplay.Move.ReadValue<Vector2>();

        var movement = new Vector3
        {
            x = movementInput.x,
            z = movementInput.y
        };

        movement.Normalize();
        
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.Self);

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
