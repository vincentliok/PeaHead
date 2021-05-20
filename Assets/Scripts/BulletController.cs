using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float gunRange;
    protected float rangeTracker; //this is used to keep track of time
    public GameObject hitEffect;

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("World") ||
            //collision.gameObject.name.Contains("Hazards") ||
            collision.gameObject.CompareTag("Enemy") ||
            collision.gameObject.name.Contains("Lock") ||
            collision.gameObject.name.Contains("Key"))
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("World") ||
            //collision.gameObject.name.Contains("Hazards") ||
            collision.gameObject.CompareTag("Enemy") ||
            collision.gameObject.name.Contains("Lock") ||
            collision.gameObject.name.Contains("Key"))
        {
            if (gameObject.name.Contains("PistolBullet"))
            {
                hitEffect.GetComponent<SpriteRenderer>().color = Color.green;
            }
            else if (gameObject.name.Contains("ShotgunBullet"))
            {
                hitEffect.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0f, 0.5f);
            }
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.3f);
            Destroy(gameObject);
        }
    }

    protected void Start()
    {
        rangeTracker = 0;
    }

    protected void Update()
    {
        if (rangeTracker > gunRange)
        {
            Destroy(gameObject);
        }
        rangeTracker += Time.deltaTime;
    }
}
