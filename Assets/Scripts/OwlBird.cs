using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlBird : Bird
{
    public float explosionStrength;
    public float explosionRadius;

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.GetComponent<Rigidbody2D>() == null)
		return;

        string tag = other.gameObject.tag;
        if(tag == "Obstacle" || tag == "Enemy")
        {
            foreach(Collider2D col in ExplosiveObject())
            {
                col.attachedRigidbody.AddExplosionForce(explosionStrength, this.transform.position);
            }

            Destroy(gameObject);
        }
    }

    List<Collider2D> ExplosiveObject()
    {
        List<Collider2D> explosive = new List<Collider2D>();
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach(Collider2D obj in objects)
        {
            if(obj.tag == "Obstacle" || obj.tag == "Enemy")
            {
                explosive.Add(obj);
            }
        }

        return explosive;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
