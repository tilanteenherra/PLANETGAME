using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerMovement : MonoBehaviour
{

    PlayerControls controls;

    Vector2 move;
    Vector3 playerMove;

    public float moveSpeed;

    private void Awake()
    {
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");

        controls = new PlayerControls();

        controls.Gameplay.MeleeAttack.performed += ctz => Attack();

        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;
    }

    private void Update()
    {
        //Vector2 m = new Vector2(move.x, move.y) * Time.deltaTime;
        //playerMove = new Vector3(move.x, 0, move.y) * moveSpeed * Time.deltaTime;
        playerMove = new Vector3(move.x, 0, move.y).normalized * moveSpeed * Time.deltaTime;
        transform.Translate(playerMove, Space.Self);
    }

    void Attack()
    {
        // Attack animation
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
