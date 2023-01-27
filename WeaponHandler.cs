using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{

    [SerializeField]private GameObject weaponLogic;


    public void enableweapon()
    {
        weaponLogic.SetActive(true);
    }

    public void disableweapon()
    {
        weaponLogic.SetActive(false);
    }

}
