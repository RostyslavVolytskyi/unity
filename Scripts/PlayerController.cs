using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, Controls.IPlayerActions
{
    public Controls control;
    public Rigidbody playerRb;
    public float jumpHeight;

    // for shooting
    bool isShooting;
    public float fireRate;
    float nextFireTime;
    public GameObject bullet;
    public Transform shotSpawn;

    // for movement
    public float forwardSpeed;
    public float sideSpeed;

    bool isForward;
    bool isRight;
    bool isLeft;
    bool isBack;
    bool isJump;


    private void FixedUpdate()
    {
        if (isForward == true) {
            playerRb.AddForce(0, 0, forwardSpeed * Time.deltaTime, ForceMode.VelocityChange);
        }
        if (isBack == true)
        {
            playerRb.AddForce(0, 0, -forwardSpeed * Time.deltaTime, ForceMode.VelocityChange);
        }
        if (isRight == true)
        {
            playerRb.AddForce(sideSpeed * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }
        if (isLeft == true)
        {
            playerRb.AddForce(-sideSpeed * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }
        if (isJump == true)
        {
            playerRb.velocity = new Vector3(playerRb.velocity.x, jumpHeight * Time.fixedDeltaTime, playerRb.velocity.z);
        }
        if(playerRb.position.y < -1)
        {
            FindObjectOfType<GameManager>().EndGame();
        }
    }


    private void Update()
    {

        // another way to listen to key press
        Keyboard kb = InputSystem.GetDevice<Keyboard>();
        if (kb.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("space clicked");
        }
        // end


        if (isShooting == true && nextFireTime < Time.time)
        {
            Instantiate(bullet, shotSpawn.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Awake()
    {
        control = new Controls();
        control.Player.SetCallbacks(this); // use Player actions map
    }

    private void OnEnable()
    {
        control.Player.Enable();
    }

    private void OnDisable()
    {
        control.Player.Disable();
    }

    // ##### Actions #####
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isJump = true;
        }
        else if (context.canceled)
        {
            isJump = false;
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isShooting = true;
        
        }
        else if (context.canceled) {
            isShooting = false;
        }
    }

    public void OnMoveForward(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isForward = true;

        }
        else if (context.canceled)
        {
            isForward = false;
        }
    }

    public void OnMoveRight(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isRight = true;

        }
        else if (context.canceled)
        {
            isRight = false;
        }
    }

    public void OnMoveLeft(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isLeft = true;

        }
        else if (context.canceled)
        {
            isLeft = false;
        }
    }

    public void OnMoveBack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isBack = true;

        }
        else if (context.canceled)
        {
            isBack = false;
        }
    }
}
