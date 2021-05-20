using UnityEngine;

public class SniperBulletController : BulletController
{
    private bool passedOnce = false;
 
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        //these conditionals might need to be changed when converting to tilemap
        if (collision.gameObject.name.Contains("World") ||
            collision.gameObject.CompareTag("Enemy") ||
            collision.gameObject.name.Contains("Lock"))
        {
            if (passedOnce)
            {
                hitEffect.GetComponent<SpriteRenderer>().color = Color.yellow;
                GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
                Destroy(effect, 0.3f);
                Destroy(gameObject);
            }
            else
            {
                passedOnce = true;
            }
        }
    }
}
