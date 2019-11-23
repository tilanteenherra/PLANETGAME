using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float mouseSensitivityX = 250f;
    //public float mouseSensitivityY = 250f;
    public float walkSpeed = 8f;

    bool canAttack = true;

    int noOfClicks;
    bool canClick;

    //Wasn't in use
    //Transform cameraT;

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

        if (Input.GetKey(KeyCode.A))
        {
            anim.SetBool("running", true);
            anim.SetInteger("condition", 44);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetBool("running", false);
            anim.SetInteger("condition", 98);
            noOfClicks = 0;
        }

        if (Input.GetKey(KeyCode.D))
        {
            anim.SetBool("running", true);
            anim.SetInteger("condition", 44);
        }
        if (Input.GetKeyUp(KeyCode.D))
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

        GetInput();

        
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);

        if (dashing)
        {
            endPosition = transform.forward * 0.3f;
            transform.position = Vector3.Lerp(transform.position, transform.position + endPosition, Time.time);
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
            //dies
            anim.SetInteger("condition", 66);
        }

        if (Input.GetKeyUp(KeyCode.K))
        {
            //showweapons
            anim.SetInteger("condition", 86);
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

        if(noOfClicks >= 1)
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
        noOfClicks = 0;
    }

    public void SetChargeFalse()
    {
        dashing = false;
    }

    public void SmokeOn()
    {
        GetComponent<DashSmokeScripts>().smokeOn = true;
    }

    public void SmokeOff()
    {
        GetComponent<DashSmokeScripts>().smokeOn = false;
    }

    public void Charge()
    {
        //float t = 1.5f;
        //rb.velocity = new Vector3(0, 0, 0);

        //while (t > 0)
        //{         
        //    rb.AddForce(transform.forward * 20f, ForceMode.Acceleration);
        //    t -= Time.deltaTime;
        //}

        //while (!(Input.GetKeyDown(KeyCode.H)))
        //{
        //    if (speed < maxSpeed)
        //    {
        //        speed = speed - acceleration * Time.deltaTime;
        //    }
        //    else if (speed > -maxSpeed)
        //    {
        //        speed = speed + acceleration * Time.deltaTime;
        //    }
        //    else
        //    {
        //        if (speed > deceleration * Time.deltaTime)
        //        {
        //            speed = speed - deceleration * Time.deltaTime;
        //        }
        //        else if (speed < -deceleration * Time.deltaTime)
        //        {
        //            speed = speed + deceleration * Time.deltaTime;
        //        }
        //        else
        //        {
        //            speed = 0;
        //        }
        //    }

        //    Vector3 tempPos = transform.position;
        //    tempPos.x = transform.position.x;
        //    tempPos.y = transform.position.y;
        //    tempPos.z = transform.position.z;



        //    transform.position = new Vector3(tempPos.x + speed * Time.deltaTime, tempPos.y, tempPos.z);
        //}

        dashing = true;



    }

    public void WeaponShow()
    {
        shovel.enabled = true;
        shield.enabled = true;
    }

    public void WeaponHide()
    {
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
        anim.SetBool("specialAttack", true);
        anim.SetInteger("condition", 25);
        yield return new WaitForSeconds(1.067f);
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
        yield return new WaitForSeconds(1.8f);
        anim.SetInteger("condition", 98);
        anim.SetBool("specialAttack2", false);
        attRoutineOn = false;
        canAttack = true;
        //Temporarily disabled since it gave errors
        //weaponDamage.hitOnce = false;
    }
}