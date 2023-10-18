using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponIcon : MonoBehaviour
{
    [SerializeField]
    private GameObject ballIcon;
    
    [SerializeField]
    private GameObject swordIcon;

    [SerializeField]
    private bool iconChange = false;
    
    public void weaponChange()
    {
        if (iconChange == false)
        {
            showSword();
        }
        else
        {
            showBall();
        }
    }

    public void showSword()
    {
        ballIcon.SetActive(false);
        swordIcon.SetActive(true);
        iconChange = true;
    }

    private void showBall()
    {
        swordIcon.SetActive(false);
        ballIcon.SetActive(true);
        iconChange = false;
    }

}
