﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerMovement : MonoBehaviour
{

    PlayerControls controls;

    Vector2 move;
    Vector2 rotate;

    Vector3 playerMove;
    Vector3 playerRotate;

    Transform playerCamera;

    Animator animator;

    public float moveSpeed;
    public float rotateSpeed;

    private void Awake()
    {
        playerCamera = Camera.main.transform;
        animator = GetComponent<Animator>();

        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");

        controls = new PlayerControls();

        controls.Gameplay.MeleeAttack.performed += ctz => Attack();

        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;

        controls.Gameplay.Rotate.performed += ctx => rotate = ctx.ReadValue<Vector2>();
        controls.Gameplay.Rotate.canceled += ctx => rotate = Vector2.zero;
    }

    private void Update()
    {
        //Vector2 m = new Vector2(move.x, move.y) * Time.deltaTime;
        //playerMove = new Vector3(move.x, 0, move.y) * moveSpeed * Time.deltaTime;

        // Player movement with controller
        playerMove = new Vector3(move.x, 0, move.y).normalized * moveSpeed * Time.deltaTime;
        transform.Translate(playerMove, Space.Self);

        // Player rotation with controller

        //playerRotate = new Vector3()

    }

    void Attack()
    {
        // Attack animation
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        animator.SetBool("attacking", true);
        animator.SetInteger("condition", 2);
        yield return new WaitForSeconds(1);
        animator.SetInteger("condition", 0);
        animator.SetBool("attacking", false);
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }
}