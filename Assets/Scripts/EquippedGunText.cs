using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquippedGunText : MonoBehaviour
{

    public Text gunText;

    private string gun;

    // Start is called before the first frame update
    void Start()
    {
        gun = "Peashooter";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gun = "Peashooter";
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && weaponUnlockData.shotgunUnlocked)
        {
            gun = "Grapeshot";
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && weaponUnlockData.sniperUnlocked)
        {
            gun = "Peanutrator";
        }

        gunText.text = $"Equipped Gun: {gun}";
    }
}
