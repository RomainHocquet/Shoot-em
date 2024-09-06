using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerActionsSC : MonoBehaviour
{
    private CharacterController controller;
    private Camera mainCamera; // Assign your main camera in the Inspector.
    public PlayerStats myPlayerStats;
    private GunStats myGunStats;

    //Movement
    public float moveSpeed = 5f;        // Movement speed
    public float jumpSpeed = 500f;        // Jump speed
    public float gravity = 9.81f;       // Gravity force
    [SerializeField]
    private Animator animator;
    private Vector3 moveDirection;      // The direction the character is moving in
                                        // private PlayerInputActions playerInputActions; // Reference to the input actions
    private Vector2 moveInputVector;
    private Vector2 lookInputVector;
    public InputAction fireAction;


    //Shooting
    private GameObject muzzle;
    private GameObject bullet;
    private float fireRate; // The rate of fire (in seconds per shot)
    private float lastShotTime = 0; // When was the last shot shooted
    private Coroutine firingCoroutine; // Reference to the firing coroutine




    void Start()
    {
        // Get the CharacterController component
        controller = GetComponent<CharacterController>();
        mainCamera = Camera.main;

        myGunStats = GetComponentInChildren<GunStats>();

        muzzle = myGunStats.muzzle;
        bullet = myGunStats.bullet;
        fireRate = myGunStats.fireRate;
    }

    public void OnMove(CallbackContext context)
    {
        moveInputVector = context.ReadValue<Vector2>();
    }

    public void OnLook(CallbackContext context)
    {
        if (context.control.device is Mouse)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Ray ray = mainCamera.ScreenPointToRay(mousePosition);
            // Cast a ray from the mouse position to the ground plane
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity))
            {
                Vector3 targetPoint = hitInfo.point;
                lookInputVector = new Vector2(targetPoint.x - transform.position.x, targetPoint.z - transform.position.z); // Calculate look direction
            }
        }
        else
        {
            lookInputVector = context.ReadValue<Vector2>();
        }
    }
    public void OnFire(CallbackContext context)
    {
        // Check if the input is performed
        if (context.started)
        {
            // Start the firing coroutine if it's not already running
            if (firingCoroutine == null)
            {
                firingCoroutine = StartCoroutine(FireContinuously());
            }
        }
        else if (context.canceled)
        {
            // Stop the firing coroutine when the input is canceled
            if (firingCoroutine != null)
            {
                StopCoroutine(firingCoroutine);
                firingCoroutine = null;
            }
        }

    }
    // Coroutine to continuously fire bullets at the specified rate
    private IEnumerator FireContinuously()
    {
        while (true)
        {
            if (isShotReady())
            {
                fireBullet(); // Call the method to fire a bullet
                lastShotTime = Time.time;
            }
            yield return new WaitForSeconds(fireRate); // Wait for the specified fire rate
        }
    }

    public bool isShotReady()
    {
        return Time.time > lastShotTime + fireRate;
    }


    public void fireBullet()
    {
        Instantiate(bullet, muzzle.transform.position, muzzle.transform.rotation);
        bullet.GetComponent<BulletSC>().owner = myPlayerStats;
    }


    void FixedUpdate()
    {
        // // Read the input vector from the new input system
        // Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        // Check if the character is grounded
        if (controller.isGrounded)
        {
            // Convert 2D input to 3D movement
            moveDirection = new Vector3(moveInputVector.x, 0, moveInputVector.y);
            moveDirection = moveDirection * moveSpeed;

            //Character look in a specific direction
            Vector3 lookDirection = Vector3.zero;
            if ((lookInputVector.x != 0) || (lookInputVector.y != 0))
            {
                lookDirection = new Vector3(lookInputVector.x, 0, lookInputVector.y);
            }
            else
            {
                lookDirection = moveDirection;
            }


            gameObject.transform.rotation = Quaternion.LookRotation(lookDirection);


            if ((moveDirection.x != 0) || (moveDirection.z != 0))
            {
                animator.SetBool("IsRunning", true);
            }
            else
            {
                animator.SetBool("IsRunning", false);
            }


            // // Check for jump input
            // if (playerInputActions.Player.Jump.triggered)
            // {
            //     moveDirection.y = jumpSpeed;
            // }
        }

        // Apply gravity to the movement direction
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the character
        controller.Move(moveDirection * Time.deltaTime);
    }



}


