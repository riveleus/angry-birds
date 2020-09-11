using UnityEngine;

public static class Rigidbody2DExt
{
    public static void AddExplosionForce(this Rigidbody2D rb, float force, Vector2 position, float upwardsModifier = 0.0f, ForceMode2D mode = ForceMode2D.Impulse)
    {
        Vector2 explosionDirection = rb.position - position;
        float explosionDistance = explosionDirection.magnitude;

        if(upwardsModifier == 0)
        {
            explosionDirection /= explosionDistance;
        }
        else 
        {
            explosionDirection.y += upwardsModifier;
            explosionDirection.Normalize();
        }

        float forceToAdd = force - (explosionDistance * 2);
        rb.AddForce(explosionDirection * forceToAdd, mode);
    }
}
