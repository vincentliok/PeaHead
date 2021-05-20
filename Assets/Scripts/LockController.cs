using UnityEngine;

public class LockController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (!player.hasKey) return;

            // Play unlock sound
            SoundManager.soundManager.PlaySound(SoundManager.Sfx.DoorUnlock);

            player.hasKey = false;
            Destroy(gameObject);
        }
    }
}
