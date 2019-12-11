using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float mouseSensitivityX = 100f;
    //public float mouseSensitivityY = 250f;
    public float walkSpeed = 8f;

    bool canAttack = true;

    public bool canDash = true;
    public bool keepPlace = false;

    Vector3 playerPos;


    public int noOfClicks;
    bool canClick;

    //Wasn't in use
    //Transform cameraT;

    //Obsolete code
    //public float speed;
    //public float maxSpeed;
    //public float acceleration;
    //public float deceleration;

    Vector3 endPosition;
    bool dashing = false;

    float verticalLookRotation;
    Rigidbody rb;

    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;

    Animator anim;


    PhotonView PV;

    //Used to hide and show weapons
    public Renderer shovel;
    public Renderer shield;

    //Disabled invicibility variables
    /*
    public Color tempColor = Color.white;
    public Renderer hideMaterial;
    */

    public bool attRoutineOn = false;

    public bool dashSmoke = false;

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
        rb = GetComponent<Rigidbody>();
        noOfClicks = 0;
        canClick = true;
        endPosition = new Vector3(0, 0, 0);

        PV = GetComponent<PhotonView>();

    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                //    //Deprecated code
                //    //if ((anim.GetBool("attackingA") == true || anim.GetBool("attackingB") == true))
                //    //{
                //    //    return;
                //    //}
                //    //else if ((anim.GetBool("attackingA") == false && anim.GetBool("attackingB") == false))
                //    //{
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

            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");



            transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivityX);


            //Mouselook up/down - not needed
            //verticalLookRotation += Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivityY;
            //verticalLookRotation = Mathf.Clamp(verticalLookRotation, -60, 60);

            //Mouselook up/down - not needed
            //cameraT.localEulerAngles = Vector3.left * verticalLookRotation;

            Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
            Vector3 targetMoveAmount = moveDir * walkSpeed;
            moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);

            Move(x, y);

        if(y > 0 && y <= 0.1)
        {
            anim.SetBool("walking", true);
        }
        else
        {
            anim.SetBool("walking", false);
        }

        if(y > 0.1)
        {
            anim.SetBool("running", true);
        }
        else
        {
            anim.SetBool("running", false);
        }

    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);

        if (dashing)
        {
            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);

            if (dashing)
            {
                endPosition = transform.forward * 0.3f;
                transform.position = Vector3.Lerp(transform.position, transform.position + endPosition, Time.time);
            }

            if (keepPlace)
            {
                transform.position = playerPos;
            }
        }
    }

    void GetInput()
    {

        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire1"))
        {
            ComboStarter();
        }

        if (Input.GetKeyUp(KeyCode.T))
        {
            if (canAttack)
            {
                SpecialAttack();
            }
        }

        if (Input.GetKeyUp(KeyCode.Y) || (Input.GetButtonDown("Fire2")))
        {
            if (canAttack)
            {
                
                SpecialAttack2();
                
            }
        }

        if (Input.GetKeyUp(KeyCode.J))
        {
            //storeweapons
            anim.SetInteger("condition", 85);
        }

        if ((Input.GetKeyUp(KeyCode.L)) || Input.GetButtonDown("Fire4"))
        {
            //diesBackward
            anim.SetInteger("condition", 66);
        }

        if (Input.GetKeyUp(KeyCode.X))
        {
            //diesForward
            anim.SetInteger("condition", 67);
        }

        if (Input.GetKeyUp(KeyCode.K))
        {
            //showweapons
            anim.SetInteger("condition", 86);
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            //eat mushroom
            anim.SetInteger("condition", 9);
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            //cactus
            anim.SetInteger("condition", 10);
        }

        if ((Input.GetKeyUp(KeyCode.N)) || Input.GetButtonDown("Fire3"))
        {
            //snowangel
            anim.SetInteger("condition", 50);
        }

        if (Input.GetKeyUp(KeyCode.B))
        {
            //changecamera
            GameObject cammy = GameObject.FindGameObjectWithTag("MainCamera");
            cammy.transform.RotateAround(cammy.transform.parent.position, cammy.transform.parent.up, 45f);
        }

        //Not in use(something wrong with the shader/material)
        /*
        if (Input.GetKeyUp(KeyCode.O))
        {
            //predator
            Predator();
        }
        */

    }


    void SpecialAttack()
    {
        if (canDash)
        {
            attRoutineOn = true;
            StartCoroutine(SpecialAttackRoutine());
        }
    }

    void SpecialAttack2()
    {
        if(canDash)
        {
            attRoutineOn = true;
            StartCoroutine(SpecialAttackRoutine2());
        }
        
    }

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
        //Not sure if this is needed(seem so work better without)
        //canClick = false;

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
            ////test
            //if (anim.GetBool("running") == true)
            //{
            //    anim.SetInteger("condition", 31);
            //    canClick = true;
            //}
            //else if (anim.GetBool("running") == false)
            //{
            //    anim.SetInteger("condition", 3);
            //    canClick = true;
            //}  

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
            ////test
            //if (anim.GetBool("running") == true)
            //{
            //    anim.SetInteger("condition", 31);
            //    canClick = true;
            //}
            //else if (anim.GetBool("running") == false)
            //{
            //    anim.SetInteger("condition", 3);
            //    canClick = true;
            //}  

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
        GetComponent<DashSmokeScripts>().smokeOn = true;
    }

    public void SmokeOff()
    {
        GetComponent<DashSmokeScripts>().smokeOn = false;
        noOfClicks = 0;
    }

    public void Charge()
    {

        dashing = true;



    }

    public void WeaponShow()
    {
        //simply enables the renderer
        shovel.enabled = true;
        shield.enabled = true;
    }

    public void WeaponHide()
    {
        //simply disables the renderer
        shovel.enabled = false;
        shield.enabled = false;
    }

    private void Move(float x, float y)
    {
        anim.SetFloat("VelX", x);
        anim.SetFloat("VelY", y);

        Vector3 moveDir = new Vector3(x, 0, y).normalized;
        Vector3 targetMoveAmount = moveDir * walkSpeed * (Mathf.Abs(x) + Mathf.Abs(y));
        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);
    }

    //Not in use
    //Invisibility trigger
    /*
    public void Predator()
    {
        tempColor = hideMaterial.material.color;
        tempColor.a = 0.0f;
        hideMaterial.material.color = tempColor;
    }
    */


    IEnumerator SpecialAttackRoutine()
    {
        canAttack = false;
        //anim.SetBool("specialAttack", true);
        anim.SetInteger("condition", 25);
        yield return new WaitForSeconds(1.067f);
        anim.SetInteger("condition", 98);
        attRoutineOn = false;
        canAttack = true;
        noOfClicks = 0;
        //Temporarily disabled since it gave errors
        //weaponDamage.hitOnce = false;
    }

    IEnumerator SpecialAttackRoutine2()
    {
        canAttack = false;
        //anim.SetBool("specialAttack2", true);
        anim.SetInteger("condition", 26);
        yield return new WaitForSeconds(1.8f);
        anim.SetInteger("condition", 98);
        attRoutineOn = false;
        canAttack = true;
        noOfClicks = 0;
        //Temporarily disabled since it gave errors
        //weaponDamage.hitOnce = false;
    }
}