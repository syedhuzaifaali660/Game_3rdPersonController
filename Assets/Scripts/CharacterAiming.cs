using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterAiming : MonoBehaviour
{

    [Header("Adujustment Variables")]
    public float turnSpeed = 15;
    public float aimDuration = 0.3f;

    public Transform cameraLookAt;
    public bool isAiming;

    [Header("Cinemachine Variables")]
    public Cinemachine.AxisState xAxis;
    public Cinemachine.AxisState yAxis;

    


    //Private Variables
    Camera mainCamera;
    RaycastWeapon weapon;
    Animator animator;
    ActiveWeapon activeWeapon;

    int isAimingParam = Animator.StringToHash("isAiming");


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponent<Animator>();
        activeWeapon = GetComponent<ActiveWeapon>();
    }

    void FixedUpdate()
    {
       
    }



    private void Update()
    {
        isAiming = Input.GetMouseButton(1);
        animator.SetBool(isAimingParam, isAiming);

        var weapon = activeWeapon.GetActiveWeapon();
        if (weapon)
        {
            weapon.recoil.recoilModifier = isAiming ? 0.3f : 1.0f;
        }

        xAxis.Update(Time.fixedDeltaTime);
        yAxis.Update(Time.fixedDeltaTime);
        
        float max = yAxis.m_MaxValue = 90;
        float min = yAxis.m_MinValue = -90;

        cameraLookAt.eulerAngles = new Vector3(Mathf.Clamp(yAxis.Value, min, max), xAxis.Value, 0);

        //Rotation of camera in Y axis
        float yawCamera = mainCamera.transform.rotation.eulerAngles.y;
        //Blend from current rotation towards the cams rotatiion only in y axis
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.fixedDeltaTime);

    }
    //private void FixedUpdate()
    //{
    //}

    //private void LateUpdate()
    //{

    //    //if (weapon)
    //    //{
    //    //    if (Input.GetButtonDown("Fire1"))
    //    //    {
    //    //        weapon.StartFiring();
    //    //    }
    //    //    if (weapon.isFiring)
    //    //    {
    //    //        weapon.UpdateFiring(Time.deltaTime);
    //    //    }

    //    //    weapon.UpdateBullets(Time.deltaTime);

    //    //    if (Input.GetButtonUp("Fire1"))
    //    //    {
    //    //        weapon.StopFiring();
    //    //    }
    //    //}

    //}
}

