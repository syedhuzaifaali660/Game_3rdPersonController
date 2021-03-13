using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public RaycastWeapon weaponFab;
    
    #region code2

   // public static int PickupCount = 0;
    //public static bool riflePicked = false;
    //public static bool pistolPicked = false;

    //public static int p_index = 0;
    //public static int r_index = 0;
    //int mistake = 0;

    //private void Awake()
    //{
    //    //PickupCount = 0;
    //    //riflePicked = false;
    //    //pistolPicked = false;
    //}

    //int PistolChecker(int temp)
    //{
    //    p_index += temp;
    //    return p_index;
    //}    

    //int RifleChecker(int temp)
    //{
    //    p_index += temp;
    //    return p_index;
    //}
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        ActiveWeapon activeWeapon = other.gameObject.GetComponent<ActiveWeapon>();
        if (activeWeapon)
        {
            RaycastWeapon newWeapon = Instantiate(weaponFab);
            activeWeapon.Equip(newWeapon);
            
            #region Test 1
            //if (PickupCount == 0) { PickupCount = 1; }
            //var currentWeaponName = weaponFab.weaponName;
            //Debug.Log(currentWeaponName + "<------->" + weaponFab.weaponName);

            //Debug.Log(PickupCount);
            //if (activeWeapon.activeWeaponIndex == 0)
            //{
            //    Debug.Log(PickupCount + "<------------");
            //    if (PickupCount < 2) { PickupCount += 1; }
            //    else { return; }
            //}
            //else if (activeWeapon.activeWeaponIndex == 1)
            //{
            //    if (PickupCount < 2) { PickupCount += 1; }
            //    else { return; }
            //}
            //else { return; }
            #endregion
        }
        //PISTOL IS 1 
        //RIFLE IS 0 
        #region Code4
        //Debug.Log(activeWeapon.activeWeaponIndex);
        //if (activeWeapon.activeWeaponIndex == 1 && pistolPicked && mistake == 1)
        //{
        //    if (p_index == 1)
        //    {
        //        Debug.Log("mistake Corrected" + PickupCount + " pistol " + p_index);
        //        //Debug.Log(p_index);
        //    }
        //}
        //else if (activeWeapon.activeWeaponIndex == 0 && pistolPicked && PickupCount == 1)
        //{
        //    mistake += 1;
        //    Debug.Log(PickupCount);
        //    Debug.Log("Mistake pistole " + mistake);

        //    int pistolCount = PistolChecker(1);

        //    p_index += 1;

        //    #region CHECKING CODE 2

        //    //GameObject pistol = GameObject.FindWithTag("Gun_P");

        //    //if (pistol != null && mistake == 1)
        //    //{

        //    //    int pistolCount = PistolChecker(1);

        //    //    //Debug.Log(PickupCount + "<------------ P Before " + pistolPicked);
        //    //    if (PickupCount <= 2) 
        //    //    {
        //    //        if (pistolCount <= 1)
        //    //        {
        //    //            PickupCount += 1;
        //    //        }
        //    //        else 
        //    //        { 
        //    //            pistolCount -= 1;
        //    //            Debug.Log("Pistol in WeaponPickUp Already Exists");
        //    //        }
        //    //    }
        //    //    Debug.Log(PickupCount + "<------------ P " + pistolPicked);


        //    //} else { return; }

        //    #endregion
        //}
        #endregion
        #region Testing
        //if (activeWeapon.activeWeaponIndex == 1 && riflePicked && r_index != 1)
        //{
        //    mistake += 1;
        //    #region CHECKING 1
        //    //int rifleCount = RifleChecker(1);
        //    //Debug.Log("--->>>>Rifle" + activeWeapon.activeWeaponIndex);

        //    //Debug.Log(PickupCount + "<------------ RRRR Before " + riflePicked);
        //    //if (PickupCount <= 2)
        //    //{
        //    //    if (rifleCount <= 1)
        //    //    {
        //    //        PickupCount += 1;
        //    //    }
        //    //    else
        //    //    {
        //    //        rifleCount -= 1;
        //    //        Debug.Log("Rifle in WeaponPickUp Already Exists");
        //    //    }
        //    //}
        //    //Debug.Log(PickupCount + "<------------ RRRR " + riflePicked);
        //    #endregion
        //}
        //else if (activeWeapon.activeWeaponIndex == 0 && riflePicked && r_index != 1 && mistake == 1)
        //{
        //    Debug.Log("Mistake rifle ");
        //}
        #endregion
        #region TESTING CODE
        //if ((pistolPicked == true))
        //{

        //    if (p_index == 1)
        //    {
        //        //GlobalVariable.singleton.p_index += 1;
        //        Debug.Log("-->>>>>>"+ p_index);

        //    }
        //    else { return; }

        //        Debug.Log(PickupCount + "<------------ P Before " + pistolPicked);
        //        if (PickupCount <= 2) { PickupCount += 1; }
        //        else { return; }
        //        Debug.Log(PickupCount + "<------------ P " + pistolPicked);

        //}


        //if ((riflePicked == true))
        //{
        //    if (r_index == 1)
        //    {
        //        //GlobalVariable.singleton.r_index += 1;
        //        Debug.Log("-->>>>>>" + r_index);

        //    }            
        //else { return; }

        //        Debug.Log(PickupCount + "<------------ R Before" + riflePicked);
        //        if (PickupCount <= 2) { PickupCount += 1; }
        //        else { return; }
        //        Debug.Log(PickupCount + "<------------ R " + riflePicked);
        //}
        //else { return; }
        #endregion
    }

    //public void PickedWeaponCounAdder(int temp)
    //{
    //    PickupCount += temp;
    //}
    

}
