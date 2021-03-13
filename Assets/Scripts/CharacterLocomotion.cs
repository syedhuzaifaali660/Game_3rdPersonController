using UnityEngine;

public class CharacterLocomotion : MonoBehaviour
{
    public Animator rigController;
    
    public float jumpHeight;
    public float gravity;
    public float stepDown;
    public float airControl;
    public float jumpDamp;
    public float groundSpeed;
    public float pushPower = 2.0F;

    CharacterAiming characterAiming;
    CharacterController cc;
    ActiveWeapon activeWeapon;
    ReloadWeapon reloadWeapon;
    Vector3 velocity;
    bool isJumping;
    int isSprintingParam = Animator.StringToHash("isSprinting");

    Animator animator;
    Vector2 input;
    Vector3 rootMotion;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
        activeWeapon = GetComponent<ActiveWeapon>();
        reloadWeapon = GetComponent<ReloadWeapon>();
        characterAiming = GetComponent<CharacterAiming>();
    }

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");


        animator.SetFloat("InputX", input.x);
        animator.SetFloat("InputY", input.y);

        UpdateIsSprinting();

        if (Input.GetKeyDown(KeyCode.Space))        {
            Jump();
        }
    }

    bool IsSprinting()
    {
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        bool isFiring = activeWeapon.IsFiring();
        bool isReloading = reloadWeapon.isReloading;
        bool isChangingWeapon = activeWeapon.isChangingWeapon;
        bool isAiming = characterAiming.isAiming;
        return isSprinting && !isFiring && !isReloading && !isChangingWeapon && !isAiming;
    }
    private void UpdateIsSprinting()
    {
        bool isSprinting = IsSprinting();
        animator.SetBool(isSprintingParam, isSprinting);
        rigController.SetBool(isSprintingParam, isSprinting);
    }


    private void FixedUpdate()
    {
        if (isJumping) //is in air state
        {
            UpdaeInAir();
        }
        else
        {
            //is grounded state
            UpdateOnGround();

        }
        cc.Move(rootMotion);
        rootMotion = Vector3.zero;
    }

    private void UpdateOnGround()
    {
        Vector3 stepForwardAmount = rootMotion * groundSpeed;
        Vector3 stepDownAmount = Vector3.down * stepDown;

        cc.Move(stepForwardAmount + stepDownAmount);
        rootMotion = Vector3.zero;

        if (!cc.isGrounded)
        {
            SetInAir(0);
        }
    }

    private void UpdaeInAir()
    {
        velocity.y -= gravity * Time.fixedDeltaTime;
        Vector3 displacement = velocity * Time.fixedDeltaTime;
        displacement += CalculateAircontrol();
        cc.Move(displacement);
        isJumping = !cc.isGrounded;
        rootMotion = Vector3.zero;
        animator.SetBool("isJumping", isJumping);
    }

    public void OnAnimatorMove()
    {
        rootMotion += animator.deltaPosition;
    }

   
    void Jump()
    {
        if (!isJumping)
        {
            float jumpVelocity = Mathf.Sqrt(2 * gravity * jumpHeight);
            SetInAir(jumpVelocity);
        }
    }

    private void SetInAir(float jumpVelocity)
    {
        isJumping = true;
        velocity = animator.velocity * jumpDamp * groundSpeed;
        velocity.y = jumpVelocity;
        animator.SetBool("isJumping", true);
    }

    Vector3 CalculateAircontrol()
    {
        return ((transform.forward * input.y) + (transform.right * input.x)) * (airControl/100);
    }



    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (body == null || body.isKinematic)
            return;

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3f)
            return;

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.

        // Apply the push
        body.velocity = pushDir * pushPower;
    }
}
