using System.Collections;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    public enum WeaponSlot
    {
        Primary = 0,
        Secondary = 1
    }

    #region VARIABLE DECLARATION
    [Header("Aiming References")]
    public Transform crossHairTarget;
    public Transform[] weaponSlots;
    public CharacterAiming characterAiming;

    [Header("Animator")]
    public Animator rigController;

    public AmmoWidget ammoWidget;
    public bool isChangingWeapon;


    RaycastWeapon[] equipped_weapons = new RaycastWeapon[2];
    public int activeWeaponIndex;
    bool isHolstered = true;

    bool weaponEquiped;

    #endregion

    void Start()
    {
        
        RaycastWeapon existingWeapon = GetComponentInChildren<RaycastWeapon>();

        if (existingWeapon)
        {
            Equip(existingWeapon);
        }
    }
    
    public bool IsFiring()
    {
        RaycastWeapon currentWeapon = GetActiveWeapon();
        if (!currentWeapon)
        {
            return false;
        }
        return currentWeapon.isFiring;
    }

    public RaycastWeapon GetActiveWeapon()
    {
        return GetWeapon(activeWeaponIndex);
    }

    RaycastWeapon GetWeapon(int index)
    {
        if(index < 0 || index >= equipped_weapons.Length)
        {
            return null;
        }
        return equipped_weapons[index];
    }


    void Update()
    {
        
        var weapon = GetWeapon(activeWeaponIndex);
        bool notSprinting = rigController.GetCurrentAnimatorStateInfo(2).shortNameHash == Animator.StringToHash("notSprinting");
        
        
        if (weapon && !isHolstered && notSprinting)        {
            weapon.UpdateBullets(Time.deltaTime);       
        }

        if (Input.GetKeyDown(KeyCode.X))            {
            ToggleActiveWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))        {
            SetActiveWeapon(WeaponSlot.Primary);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))        {
            SetActiveWeapon(WeaponSlot.Secondary);
            
        }
    }

    #region OLD CODE FOR SOLVING FIRING PROBLEM
    //WEAPON ON BACK SHOOTING PROBLEM SOLVED
    //void ActiveWeaponFiringOff()
    //{

    //    if (activeWeaponIndex == 1 && GameObject.FindWithTag("Gun_P").activeInHierarchy)
    //    {

    //        GameObject.FindWithTag("Gun_P").GetComponent<RaycastWeapon>().enabled = true;
    //        if (WeaponPickup.PickupCount == 2)
    //        {
    //            GameObject.FindWithTag("Gun_R").GetComponent<RaycastWeapon>().enabled = false;
    //        }
    //        else { return; }

    //    }
    //    else if (activeWeaponIndex == 0 && GameObject.FindWithTag("Gun_R").activeInHierarchy)
    //    {
    //        GameObject.FindWithTag("Gun_R").GetComponent<RaycastWeapon>().enabled = true;
    //        if (WeaponPickup.PickupCount == 2)
    //        {
    //            //Debug.Log(WeaponPickup.PickupCount);
    //            GameObject.FindWithTag("Gun_P").GetComponent<RaycastWeapon>().enabled = false;
    //        }
    //        else { return; }
    //    }
    //    else
    //    {
    //        return;
    //    }

    //    #region Testing Failed
    //    //WeaponPickup wp;
    //    //GameObject pickUp = GameObject.FindWithTag("Gun_ref_P");

    //    //if (pickUp != null)
    //    //{
    //    //    wp = pickUp.GetComponent<WeaponPickup>();

    //    #region TESTING CODE 2 WORKING
    //    //    //if ((activeWeaponIndex == 1 || activeWeaponIndex == 0) && WeaponPickup.pistolPicked)
    //    //    //{
    //    //    //    Debug.Log("inside loooooooooooooooooooooooooooooooop");
    //    //    //    Debug.Log(WeaponPickup.p_index+"pistooooooooooooooooooooooooooooooolll indexxxxxxxxxxxxxxxxxxxx");

    //    //    //}
    //    //    //else
    //    //    //{
    //    //    //    return;
    //    //    //}
    //    #endregion

    //    //    GameObject pistol = GameObject.FindWithTag("Gun_P");
    //    //    if (pistol != null)
    //    //    {
    //    //        //if ((activeWeaponIndex == 1 || activeWeaponIndex == 0) && WeaponPickup.pistolPicked)
    //    //        //{
    //    //            if (activeWeaponIndex == 1 && GameObject.FindWithTag("Gun_P").activeInHierarchy)
    //    //            {

    //    //                GameObject.FindWithTag("Gun_P").GetComponent<RaycastWeapon>().enabled = true;
    //    //                if (WeaponPickup.PickupCount == 2)
    //    //                {
    //    //                    Debug.Log(WeaponPickup.PickupCount);
    //    //                    GameObject.FindWithTag("Gun_R").GetComponent<RaycastWeapon>().enabled = false;
    //    //                }
    //    //                else { return; }

    //    //            }

    //    //        //}
    //    //    }

    //    //    GameObject rifle = GameObject.FindWithTag("Gun_R");
    //    //    if (rifle != null)
    //    //    {
    //    //        GameObject.FindWithTag("Gun_R").GetComponent<RaycastWeapon>().enabled = true;
    //    //        if (activeWeaponIndex == 0 && GameObject.FindWithTag("Gun_P").activeInHierarchy)
    //    //        {
    //    //            //Debug.Log("pickup should be 2 -----------------> Rifle"+WeaponPickup.PickupCount);
    //    //            Debug.Log("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
    //    //        }

    //    //    }
    //    //    else { return; }
    //    //}


    //    //else if (activeWeaponIndex == 0 && GameObject.FindWithTag("Gun_R").activeInHierarchy)
    //    //{
    //    //    GameObject.FindWithTag("Gun_R").GetComponent<RaycastWeapon>().enabled = true;
    //    //    if (WeaponPickup.PickupCount == 2)
    //    //    {

    //    //        Debug.Log("pickup should be 2 -----------------> Rifle" + WeaponPickup.PickupCount);
    //    //        GameObject.FindWithTag("Gun_P").GetComponent<RaycastWeapon>().enabled = false;

    //    //    }
    //    //    else { return; }
    //    //}
    //    //else { return; }
    //    #endregion

    //    #region TESTING CODE
    //    //if (activeWeaponIndex == 1 && GameObject.FindWithTag("Gun_P").activeInHierarchy)
    //    //{

    //    //    GameObject.FindWithTag("Gun_P").GetComponent<RaycastWeapon>().enabled = true;
    //    //    if (WeaponPickup.PickupCount == 2) 
    //    //    {
    //    //        Debug.Log(WeaponPickup.PickupCount);
    //    //        GameObject.FindWithTag("Gun_R").GetComponent<RaycastWeapon>().enabled = false;
    //    //    }
    //    //    else { return; }

    //    //}
    //    //else if (activeWeaponIndex == 0 && GameObject.FindWithTag("Gun_R").activeInHierarchy)
    //    //{
    //    //    GameObject.FindWithTag("Gun_R").GetComponent<RaycastWeapon>().enabled = true;
    //    //    if (WeaponPickup.PickupCount == 2)
    //    //    {

    //    //        Debug.Log(WeaponPickup.PickupCount);
    //    //        GameObject.FindWithTag("Gun_P").GetComponent<RaycastWeapon>().enabled = false;
    //    //    }
    //    //    else { return; }
    //    //}
    //    //else
    //    //{
    //    //    return;
    //    //}
    //    #endregion
    //}
    #endregion

    #region FIRING PROBLEM SOLVED FINALLY !!!!!
    void FiringOffForHolsteredWeapon(int currentWeaponIndex)
    {
        if (weaponEquiped)
        {

            if (currentWeaponIndex == 1)            {

                GameObject go2 = GameObject.FindWithTag("Gun_P");
                if (go2 != null)                {
                    GameObject.FindWithTag("Gun_P").GetComponent<RaycastWeapon>().enabled = true;
                    Debug.Log("Current Weapon Index Custom " + currentWeaponIndex);
                }else { return; }
                
                GameObject go = GameObject.FindWithTag("Gun_R");
                if (go != null) { 
                    GameObject.FindWithTag("Gun_R").GetComponent<RaycastWeapon>().enabled = false;
                }  else { return; }

            }  
            else if (currentWeaponIndex == 0)    {
                
                GameObject go2 = GameObject.FindWithTag("Gun_R");
                if (go2 != null)
                {
                    GameObject.FindWithTag("Gun_R").GetComponent<RaycastWeapon>().enabled = true;
                    Debug.Log("Current Weapon Index Custom " + currentWeaponIndex);
                }   else { return; }
                

                GameObject go = GameObject.FindWithTag("Gun_P");
                if (go != null)                {
                    GameObject.FindWithTag("Gun_P").GetComponent<RaycastWeapon>().enabled = false;
                } else { return; }
            }            else { return; }
        }
        else { return; }
    }
    #endregion


    public void Equip(RaycastWeapon newWeapon)
    {
        int weaponSlotIndex = (int)newWeapon.weaponSlot;
        var weapon = GetWeapon(weaponSlotIndex);
        if (weapon)        {
            Destroy(weapon.gameObject);
        }
        weapon = newWeapon;
        weapon.raycastDestination = crossHairTarget;
        weapon.recoil.characterAiming = characterAiming;
        weapon.recoil.rigController = rigController;
        weapon.transform.SetParent (weaponSlots[weaponSlotIndex],false);
        equipped_weapons[weaponSlotIndex] = weapon;

        SetActiveWeapon(newWeapon.weaponSlot);       
        weaponEquiped = true;
        ammoWidget.Refresh(weapon.ammoCount);
    }

    void SetActiveWeapon(WeaponSlot weaponSlot)
    {

        int holsterIndex = activeWeaponIndex;
        int activateIndex = (int)weaponSlot;

        if(holsterIndex == activateIndex)
        {
            holsterIndex = -1;
        }
        StartCoroutine(SwitchWeapon(holsterIndex, activateIndex));
    }

    void ToggleActiveWeapon()
    {
        bool isHolstered = rigController.GetBool("holster_weapon");
        if (isHolstered)        {
            StartCoroutine(ActivateWeapon(activeWeaponIndex));
        } else        {
            StartCoroutine(HolsterWeapon(activeWeaponIndex));   
        }
    }

    IEnumerator SwitchWeapon (int holsterIndex, int activateIndex)
    {
        rigController.SetInteger("weapon_index", activateIndex);
        yield return StartCoroutine(HolsterWeapon(holsterIndex));
        yield return StartCoroutine(ActivateWeapon(activateIndex));

        activeWeaponIndex = activateIndex;
    }

    IEnumerator HolsterWeapon (int index)
    {
        isChangingWeapon = true;
        isHolstered = true;


        var weapon = GetWeapon(index);
        if(weapon)
        {
            rigController.SetBool("holster_weapon", true);
            do
            {
                yield return new WaitForEndOfFrame();
            }
            while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        }
        isChangingWeapon = false;
    }

    IEnumerator ActivateWeapon (int index)
    {
        isChangingWeapon = true;

        FiringOffForHolsteredWeapon(index);

        var weapon = GetWeapon(index);
        if(weapon)
        {
            rigController.SetBool("holster_weapon", false);
            rigController.Play("equip_" + weapon.weaponName);
            do
            {
                yield return new WaitForEndOfFrame();
            } 
            while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
            isHolstered = false;

        }
        isChangingWeapon = false;
    }
}
