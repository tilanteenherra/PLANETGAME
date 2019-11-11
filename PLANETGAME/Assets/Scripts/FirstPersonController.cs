using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float mouseSensitivityX = 250f;
    public float mouseSensitivityY = 250f;
    public float walkSpeed = 8f;

    bool canAttack = true;
    //bool canDoubleAttack = true;

    //Wasn't in use
    //Transform cameraT;
    float verticalLookRotation;

    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;

    Animator anim;

    public bool attRoutineOn = false;

    //Temporarily disabled since it gave errors
    //WeaponDamage weaponDamage;

    // Start is called before the first frame update
    void Start()
    {
        //Temporarily disabled since it gave errors
        //weaponDamage = GameObject.FindGameObjectWithTag("Weapon").GetComponent<WeaponDamage>();
        //Wasn't in use
        //cameraT = Camera.main.transform;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if ((anim.GetBool("attackingA") == true || anim.GetBool("attackingB") == true))
            {
                return;
            }
            else if ((anim.GetBool("attackingA") == false && anim.GetBool("attackingB") == false))
            {
                anim.SetBool("running", true);
                anim.SetInteger("condition", 1);
            }

        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetBool("running", false);
            anim.SetInteger("condition", 0);
        }

        if (Input.GetKey(KeyCode.E))
        {
            
                anim.SetBool("walking", true);
                anim.SetInteger("condition", 9);
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            anim.SetBool("walking", false);
            anim.SetInteger("condition", 0);
        }

        if (Input.GetKey(KeyCode.R))
        {

            anim.SetBool("walkBack", true);
            anim.SetInteger("condition", 20);
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            anim.SetBool("walkBack", false);
            anim.SetInteger("condition", 0);
        }

        if (Input.GetKey(KeyCode.S))
        {

            anim.SetBool("runBack", true);
            anim.SetInteger("condition", 19);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            anim.SetBool("runBack", false);
            anim.SetInteger("condition", 0);
        }



        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivityX);
        verticalLookRotation += Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivityY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -60, 60);
        
        //Mouselook up/down - not needed
        //cameraT.localEulerAngles = Vector3.left * verticalLookRotation;

        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        Vector3 targetMoveAmount = moveDir * walkSpeed;
        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);

        GetInput();

        
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }

    void GetInput()
    {
        if (canAttack)
        {

            if (Input.GetMouseButtonDown(0))
            {

                AttackingA();

            }

            if (Input.GetMouseButtonDown(1))
            {
                AttackingB();

            }

            if (Input.GetKey(KeyCode.T))
            {

                SpecialAttack();
            }
        }

    }

    void AttackingA()
    {
        attRoutineOn = true;
        StartCoroutine(AttackRoutineA());
    }

    void AttackingB()
    {
        attRoutineOn = true;
        StartCoroutine(AttackRoutineB());
    }

    void SpecialAttack()
    {
        attRoutineOn = true;
        StartCoroutine(SpecialAttackRoutine());
    }


    IEnumerator AttackRoutineA()
    {
        canAttack = false;
        anim.SetBool("attackingA", true);
        anim.SetInteger("condition", 2);
        //anim.Play("AttackA", 0, 0);
        yield return new WaitForSeconds(1.0f);
        anim.SetInteger("condition", 3);
        
        yield return new WaitForSeconds(1.3f);
        anim.SetInteger("condition", 0);
        anim.SetBool("attackingA", false);
        attRoutineOn = false;
        canAttack = true;
        //Temporarily disabled since it gave errors
        //weaponDamage.hitOnce = false;
    }

    IEnumerator AttackRoutineB()
    {
        canAttack = false;
        anim.SetBool("attackingB", true);
        anim.SetInteger("condition", 3);
        yield return new WaitForSeconds(1.3f);
        anim.SetInteger("condition", 0);
        anim.SetBool("attackingB", false);
        attRoutineOn = false;
        canAttack = true;
        //Temporarily disabled since it gave errors
        //weaponDamage.hitOnce = false;
    }

    IEnumerator SpecialAttackRoutine()
    {
        canAttack = false;
        anim.SetBool("specialAttack", true);
        anim.SetInteger("condition", 25);
        yield return new WaitForSeconds(2.733f);
        anim.SetInteger("condition", 0);
        anim.SetBool("specialAttack", false);
        attRoutineOn = false;
        canAttack = true;
        //Temporarily disabled since it gave errors
        //weaponDamage.hitOnce = false;
    }
}
