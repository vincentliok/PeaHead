using UnityEngine;

public class PowerupController : MonoBehaviour
{
    private void Start()
    {
        if (gameObject.name == "ShotgunPowerup")
        {
            if (weaponUnlockData.shotgunUnlocked)
                Destroy(gameObject);
        }

        else if (gameObject.name == "SniperPowerup")
        {
            if (weaponUnlockData.sniperUnlocked)
                Destroy(gameObject);
        }
    }
}
