using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun.Demo.SlotRacer.Utils;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerController : MonoBehaviour
{

    // Class usage variables
    PlayerControls controls;
    public WeaponDamage weaponDamage;
    public SpellHit spellHit;
    DashSmokeScripts dashSmoker;
    
    //Component variables
    Animator anim;
    Rigidbody rb;
    
    // Renders ---------
    public Renderer shovel;
    public Renderer shield;
    
    // Float & Int variables ----------
    public float moveSpeed = 10;
    public float controllerRotateSpeed;
    public float mouseRotateSpeed;
    public float dashSpeed = 8f;
    public int noOfClicks = 0;
    public float walkSpeed = 8f;
    public float buffTimer;
    private float xInput;
    private float yInput;

    // Boolean variables
    //------ PUBLIC ------
    public bool attRoutineOn = false;
    public bool canDash = true;
    public bool paused = false;
    public bool keepPlace = false;
    public bool interacting = false;
    //------ PRIVATE ------
    private bool canAttack = true;
    private bool dashing = false;
    private bool walking = false;
    private bool canClick = true;

    // Vectors ----------
    Vector3 playerPos;
    Vector3 endPosition;
    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;
    Vector2 rotateInput;


    public bool dashSmoke = false;

    // Start is called before the first frame update
    void Awake()
    {
        //Temporarily disabled since it gave errors
        weaponDamage = GameObject.FindGameObjectWithTag("Weapon").GetComponent<WeaponDamage>();
        spellHit = GameObject.FindGameObjectWithTag("SpellCollider").GetComponent<SpellHit>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        shovel = GameObject.Find("Shovel").GetComponent<Renderer>();
        shield = GameObject.Find("Shield").GetComponent<Renderer>();
        dashSmoker = GetComponent<DashSmokeScripts>();
        noOfClicks = 0;
        canClick = true;
        endPosition = new Vector3(0, 0, 0);

        // Input Controller Related Things Start Here ------------------------------------
        
        controls = new PlayerControls();

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
        controls.Gameplay.Pause.performed += ctx => Pause();
        controls.Gameplay.AngleChange.performed += ctx => ChangeAngle();

        // Input Controller Related Things End Here --------------------------------------

    }
    
    void Update()
    {

        // Animations Start ------------------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetBool("running", true);
            //        anim.SetInteger("condition", 1);
            //        noOfClicks = 0;
            noOfClicks = 0;
            canClick = true;
        }

        //}
        if (Input.GetKeyUp(KeyCode.W) && (Input.GetKey(KeyCode.A) == false) && (Input.GetKey(KeyCode.S) == false) && (Input.GetKey(KeyCode.D) == false))
        {
            anim.SetBool("running", false);
        //    anim.SetInteger("condition", 98);
            noOfClicks = 0;
            canClick = true;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetBool("running", true);
        //    anim.SetInteger("condition", 44);
            noOfClicks = 0;
            canClick = true;
        }
        if (Input.GetKeyUp(KeyCode.A) && (Input.GetKey(KeyCode.W) == false) && (Input.GetKey(KeyCode.S) == false) && (Input.GetKey(KeyCode.D) == false))
        {
            anim.SetBool("running", false);
        //    anim.SetInteger("condition", 98);
            noOfClicks = 0;
            canClick = true;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetBool("running", true);
        //    anim.SetInteger("condition", 44);
            noOfClicks = 0;
            canClick = true;
        }
        if (Input.GetKeyUp(KeyCode.D) && (Input.GetKey(KeyCode.A) == false) && (Input.GetKey(KeyCode.S) == false) && (Input.GetKey(KeyCode.W) == false))
        {
            anim.SetBool("running", false);
        //    anim.SetInteger("condition", 98);
            noOfClicks = 0;
            canClick = true;
        }

        if (Input.GetKey(KeyCode.R))
        {
            anim.SetInteger("condition", 20);
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            anim.SetInteger("condition", 98);
            noOfClicks = 0;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            anim.SetBool("running", true);
        //    anim.SetInteger("condition", 19);
            noOfClicks = 0;
            canClick = true;
        }
        if (Input.GetKeyUp(KeyCode.S) && (Input.GetKey(KeyCode.A) == false) && (Input.GetKey(KeyCode.W) == false) && (Input.GetKey(KeyCode.D) == false))
        {
            anim.SetBool("running", false);
        //    anim.SetInteger("condition", 98);
            noOfClicks = 0;
            canClick = true;
        }
        
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
        anim.SetFloat("VelX", xInput);
        anim.SetFloat("VelY", yInput);

        if ((yInput > 0 && yInput <= 0.5) || (xInput > 0 && xInput <= 0.5))
        {
            anim.SetBool("walking", true);
        }
        else if ((yInput < 0 && yInput >= -0.5) || (xInput < 0 && xInput >= -0.5))
        {
            anim.SetBool("walking", true);
        }     
        else
        {
            anim.SetBool("walking", false);
        }

        if (yInput > 0.5 || xInput > 0.5)
        {
            anim.SetBool("running", true);
        }
        else if (yInput < -0.5 || xInput < -0.5)
        {
            anim.SetBool("running", true);
        }
        else
        {
            anim.SetBool("running", false);
        }


        // Animations end ----------------------------------------------------------------------------------------
    }

    void FixedUpdate()
    {
        // Actual player movement
        if (!attRoutineOn)
        {
            Vector3 moveDir = new Vector3(xInput, 0, yInput);
            gameObject.transform.Translate(moveDir * moveSpeed * Time.fixedDeltaTime, Space.Self);
        }

        // Player rotation
        var hRotateInput = rotateInput.x;
        var rotate = new Vector2(0, hRotateInput) * controllerRotateSpeed * Time.deltaTime;
        gameObject.transform.Rotate(rotate, Space.Self);

        // Player rotation with mouse
        var hMouseInput = Input.GetAxis("Mouse X") * mouseRotateSpeed * Time.deltaTime;
        gameObject.transform.Rotate(0, hMouseInput,0, Space.Self);

        if (dashing)
        {
            endPosition = transform.forward * 0.3f;
            transform.position = Vector3.Lerp(transform.position, transform.position + endPosition, Time.time);
        }

        if(keepPlace)
        {
            transform.position = playerPos;
        }
    }
    
    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    void MeleeAttack()
    {
        Debug.Log(("Melee"));
        ComboStarter();
    }
    
    void Spell1()
    {
        Debug.Log("Spell1");
        if (canAttack)
        {
            if (canDash)
            {
                attRoutineOn = true;
                StartCoroutine(SpecialAttackRoutine());
            }
        }
    }
    
    void Spell2()
    {
        Debug.Log("Spell2");
        if (canAttack)
        {
            if (canDash)
            {
                attRoutineOn = true;
                StartCoroutine(SpecialAttackRoutine2());   
            }
        }
    }

    void Walk()
    {
        walking = true;
        moveSpeed *= 0.5f;
    }

    void Run()
    {
        walking = false;
        moveSpeed *= 2f;
    }

    void OnInteract()
    {
        interacting = true;
    }

    void NoInteract()
    {
        interacting = false;
        buffTimer = 0f;
    }

    public void Pause()
    {
        Debug.Log("Online games can't be paused mom!");
        if (paused)
        {
            paused = false;
        }
        else if (!paused)
        {
            paused = true;
        }
    }

    void ChangeAngle()
    {
        // Change camera angle
        GameObject cammy = gameObject.transform.GetChild(0).gameObject;
        var parent = cammy.transform.parent;
        cammy.transform.RotateAround(parent.position, parent.up, 45f);
    }
    /*
    void GetInput()
    {
            //storeweapons
            anim.SetInteger("condition", 85);
       
            //diesBackward
            anim.SetInteger("condition", 66);
        
            //diesForward
            anim.SetInteger("condition", 67);
        
            //showweapons
            anim.SetInteger("condition", 86);
        
            //eat mushroom
            anim.SetInteger("condition", 9);
       
            //cactus
            anim.SetInteger("condition", 10);
        
            //snowangel
            anim.SetInteger("condition", 50);
*/
    void ComboStarter()
    {
        if (canClick)
        {
            noOfClicks++;
        }

        if (noOfClicks >= 1 && ((anim.GetBool("running") == true) || (anim.GetBool("walking") == true)))
        {   
            anim.SetInteger("condition", 30);
            canDash = false;
        }

        if (noOfClicks >= 1 && ((anim.GetBool("running") == false) && (anim.GetBool("walking") == false)))
        {
            if(canAttack)
            {
                anim.SetInteger("condition", 2);
                canDash = false;
                playerPos = transform.position;
                keepPlace = true;
            }
            
        }
    }

    public void ComboCheck()
    {

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("AttackA") && noOfClicks == 1)
        {
            anim.SetInteger("condition", 98);
            canClick = true;
            noOfClicks = 0;
            canDash = true;
            keepPlace = false;
        }
         
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("AttackA") && noOfClicks >= 2)
        {
            anim.SetInteger("condition", 3);
            canClick = true;
            canDash = false;
            keepPlace = true;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("AttackB"))
        {
            anim.SetInteger("condition", 98);
            canClick = true;
            noOfClicks = 0;
            canDash = true;
            keepPlace = false;
        }
        else if (anim.GetCurrentAnimatorStateInfo(1).IsName("AttackA") && noOfClicks == 1)
        {
            anim.SetInteger("condition", 98);
            canClick = true;
            noOfClicks = 0;
            canDash = true;
        }
        else if (anim.GetCurrentAnimatorStateInfo(1).IsName("AttackA") && noOfClicks >= 2)
        {
            anim.SetInteger("condition", 31);
            canClick = true;
            canDash = false;
        }
        else if (anim.GetCurrentAnimatorStateInfo(1).IsName("AttackB"))
        {
            anim.SetInteger("condition", 98);
            canClick = true;
            noOfClicks = 0;
            canDash = true;
        }
    }

    public void ExitAnimation()
    {
        anim.SetInteger("condition", 98);
        noOfClicks = 0;
        keepPlace = false;
    }

    public void SetChargeFalse()
    {
        dashing = false;
        noOfClicks = 0;
    }

    public void SmokeOn()
    {
        dashSmoker.smokeOn = true;
    }

    public void SmokeOff()
    {
        dashSmoker.smokeOn = false;
        noOfClicks = 0;
    }

    public void Charge()
    {
        dashing = true;
    }

    public void WeaponShow()
    {
        // Enables renderers
        shovel.enabled = true;
        shield.enabled = true;
    }

    public void WeaponHide()
    {
        // Disables renderers
        shovel.enabled = false;
        shield.enabled = false;
    }

    IEnumerator SpecialAttackRoutine()
    {
        canAttack = false;
        canDash = false;
        //anim.SetBool("specialAttack", true);
        anim.SetInteger("condition", 25);
        yield return new WaitForSeconds(1.067f);
        anim.SetInteger("condition", 98);
        attRoutineOn = false;
        canAttack = true;
        canDash = true;
        noOfClicks = 0;
        weaponDamage.hitOnce = false;
        spellHit.hitOnce = false;
    }

    IEnumerator SpecialAttackRoutine2()
    {
        canAttack = false;
        canDash = false;
        //anim.SetBool("specialAttack2", true);
        anim.SetInteger("condition", 26);
        yield return new WaitForSeconds(1.8f);
        anim.SetInteger("condition", 98);
        attRoutineOn = false;
        canAttack = true;
        canDash = true;
        noOfClicks = 0;
        weaponDamage.hitOnce = false;
        spellHit.hitOnce = false;
    }
}
