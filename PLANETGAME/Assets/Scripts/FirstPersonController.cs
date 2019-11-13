using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float mouseSensitivityX = 250f;
    public float mouseSensitivityY = 250f;
    public float walkSpeed = 8f;

    bool canAttack = true;

    int noOfClicks;
    bool canClick;

    //Wasn't in use
    //Transform cameraT;
    float verticalLookRotation;

    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;

    Animator anim;

    public Renderer shovel;
    public Renderer shield;

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
        noOfClicks = 0;
        canClick = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.W))
        {
            //if ((anim.GetBool("attackingA") == true || anim.GetBool("attackingB") == true))
            //{
            //    return;
            //}
            //else if ((anim.GetBool("attackingA") == false && anim.GetBool("attackingB") == false))
            //{
                anim.SetBool("running", true);
                anim.SetInteger("condition", 1);
            //}

        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetBool("running", false);
            anim.SetInteger("condition", 98);
            noOfClicks = 0;
        }

        if (Input.GetKey(KeyCode.E))
        {
            
                anim.SetBool("walking", true);
                anim.SetInteger("condition", 9);
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            anim.SetBool("walking", false);
            anim.SetInteger("condition", 98);
            noOfClicks = 0;
        }

        if (Input.GetKey(KeyCode.R))
        {

            anim.SetBool("walkBack", true);
            anim.SetInteger("condition", 20);
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            anim.SetBool("walkBack", false);
            anim.SetInteger("condition", 98);
            noOfClicks = 0;
        }

        if (Input.GetKey(KeyCode.S))
        {

            anim.SetBool("runBack", true);
            anim.SetInteger("condition", 19);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            anim.SetBool("runBack", false);
            anim.SetInteger("condition", 98);
            noOfClicks = 0;
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

        if (Input.GetMouseButtonDown(0))
        {
            ComboStarter();
        }

        if (Input.GetKey(KeyCode.T))
        {

            SpecialAttack();
        }

        if (Input.GetKey(KeyCode.Y))
        {

            SpecialAttack2();
        }

        if (Input.GetKey(KeyCode.J))
        {
            //storeweapons
            anim.SetInteger("condition", 85);
            shovel.enabled = false;
            shield.enabled = false;
        }

        if (Input.GetKeyUp(KeyCode.K))
        {
            //showweapons
            anim.SetInteger("condition", 86);
            shovel.enabled = true;
            shield.enabled = true;
        }

        if (Input.GetKeyUp(KeyCode.N))
        {
            //snowangel
            anim.SetInteger("condition", 50);
        }

    }


    void SpecialAttack()
    {
        attRoutineOn = true;
        StartCoroutine(SpecialAttackRoutine());
    }

    void SpecialAttack2()
    {
        attRoutineOn = true;
        StartCoroutine(SpecialAttackRoutine2());
    }

    void ComboStarter()
    {
        if (canClick)
        {
            noOfClicks++;
        }

        if(noOfClicks == 1)
        {
            anim.SetInteger("condition", 2);
        }
    }

    public void ComboCheck()
    {
        canClick = false;

        if(anim.GetCurrentAnimatorStateInfo(0).IsName("AttackA") && noOfClicks == 1)
        {
            anim.SetInteger("condition", 98);
            canClick = true;
            noOfClicks = 0;
        }
        else if(anim.GetCurrentAnimatorStateInfo(0).IsName("AttackA") && noOfClicks >= 2)
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

    public void ExitAnimation()
    {
        anim.SetInteger("condition", 98);
    }

    IEnumerator SpecialAttackRoutine()
    {
        canAttack = false;
        anim.SetBool("specialAttack", true);
        anim.SetInteger("condition", 25);
        yield return new WaitForSeconds(2.733f);
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
        yield return new WaitForSeconds(2.667f);
        anim.SetInteger("condition", 98);
        anim.SetBool("specialAttack2", false);
        attRoutineOn = false;
        canAttack = true;
        //Temporarily disabled since it gave errors
        //weaponDamage.hitOnce = false;
    }
}
