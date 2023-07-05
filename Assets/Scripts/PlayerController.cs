using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed, gravityModifier, jumpPower, runSpeed;
    public CharacterController charCon;

    private Vector3 moveInput;

    public Transform camTrans;

    public float mouseSensitivity;
    public bool invertX;
    public bool invertY;

    private bool canJump, canDoubleJump;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;

    public Animator anim;

    public GameObject bullet;
    public Transform firePoint;

    public Gun activeGun;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UIController.instance.ammoText.text = "Ammo: " + activeGun.currentAmmo;
    }

    void Update()
    {
        //Set up movement but can't change direction
        //moveInput.x = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        //moveInput.z = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        //Store y velocity
        float yStore = moveInput.y;
       
        //Set up movement and can change direction
        Vector3 vertMove = transform.forward * Input.GetAxis("Vertical");
        Vector3 horiMove = transform.right * Input.GetAxis("Horizontal");

        moveInput = vertMove + horiMove;//moveInput.y = 0 Because vertMove and horiMove just change the x and z
        moveInput.Normalize(); // Set the value of moveInput = 1 or smaller 

        //Running
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveInput = moveInput * runSpeed;
            anim.SetBool("isRun", true);
        }
        else//Movement
        {
            moveInput = moveInput * moveSpeed;
            anim.SetBool("isRun", false);
        }

        //Set up gravity for player
        moveInput.y = yStore;

        moveInput.y += Physics.gravity.y * gravityModifier * Time.deltaTime;

        if (charCon.isGrounded)
        {
            moveInput.y = Physics.gravity.y * gravityModifier * Time.deltaTime;
        }

        //Handle Jumping
        canJump = Physics.OverlapSphere(groundCheckPoint.position, .25f, whatIsGround).Length > 0;

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            moveInput.y = jumpPower;

            canDoubleJump = true;
        }else if(canDoubleJump && Input.GetKeyDown(KeyCode.Space))
        {
            moveInput.y = jumpPower;
            canDoubleJump = false;
        }
        //Debug.Log(moveInput);
        charCon.Move(moveInput * Time.deltaTime);

        //Control camera rotation
        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;

        if (invertX)
        {
            mouseInput.x = -mouseInput.x;
        }
        if (invertY)
        {
            mouseInput.y = -mouseInput.y;   
        }

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);
        camTrans.rotation = Quaternion.Euler(camTrans.rotation.eulerAngles + new Vector3(-mouseInput.y, 0f, 0f));

        //Handle Shooting
        //single shots
        if(Input.GetMouseButtonDown(0) && activeGun.fireCounter <= 0)
        {
            RaycastHit hit;
            if(Physics.Raycast(camTrans.position, camTrans.forward, out hit, 50f))
            {
                if(Vector3.Distance(camTrans.position, hit.point) > 2f)
                {
                    firePoint.LookAt(hit.point);
                }
            }
            else
            {
                firePoint.LookAt(camTrans.position + (camTrans.forward * 30f));
            }

            //Instantiate(bullet, firePoint.position, firePoint.rotation);
            FireShot();
        }

        //repeats shots
        if (Input.GetMouseButton(0) && activeGun.canAutoFire)
        {
            if(activeGun.fireCounter <= 0)
            {
                FireShot();
            }
        }

        //Movement Bobbing Use Animation
        //Debug.Log(moveInput.magnitude); 
        anim.SetBool("onGround", canJump);
        anim.SetFloat("moveSpeed", moveInput.magnitude);
    }

    public void FireShot()
    {
        if (activeGun.currentAmmo > 0)
        {
            activeGun.currentAmmo--;

            Instantiate(activeGun.bullet, firePoint.position, firePoint.rotation);

            activeGun.fireCounter = activeGun.fireRate;

            UIController.instance.ammoText.text = "Ammo: " + activeGun.currentAmmo;
        }
    }
   
}
