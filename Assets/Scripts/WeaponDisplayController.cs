using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponDisplayController : MonoBehaviour
{
    public static WeaponDisplayController weaponDisplay;
    public GameObject pea;
    public GameObject grape;
    public GameObject peanut;

    private void Start()
    {
        if (weaponDisplay != null)
            Destroy(weaponDisplay);
        weaponDisplay = this;

        if (weaponUnlockData.shotgunUnlocked)
        {
            ShowPea();
            ShowGrape();
        }
        if (weaponUnlockData.sniperUnlocked)
        {
            ShowPeanut();
        }
    }

    public void ShowPea() => pea.SetActive(true);

    public void ShowGrape() => grape.SetActive(true);

    public void ShowPeanut() => peanut.SetActive(true);

    public void SwitchToPeanut()
    {
        GameObject.Find("Player").GetComponent<PlayerController>().gun = "Sniper";
        GameObject.Find("Player").GetComponent<PlayerController>().ChangeState(2);
    }

    public void SwitchToPea()
    {
        GameObject.Find("Player").GetComponent<PlayerController>().gun = "Pistol";
        GameObject.Find("Player").GetComponent<PlayerController>().ChangeState(0);
    }

    public void SwitchToGrape()
    {
        GameObject.Find("Player").GetComponent<PlayerController>().gun = "Shotgun";
        GameObject.Find("Player").GetComponent<PlayerController>().ChangeState(1);
    }

    public void DisableShoot()
    {
        GameObject.Find("Player").GetComponent<PlayerController>().shootDisabled = true;
    }

    public void EnableShoot()
    {
        GameObject.Find("Player").GetComponent<PlayerController>().shootDisabled = false;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
