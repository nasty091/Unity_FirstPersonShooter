using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public string theGun;

    public bool collected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !collected)
        {
            PlayerController.instance.AddGun(theGun);

            Destroy(gameObject);

            collected = true;
        }
    }
}
