using System.Collections.Generic;
using UnityEngine;

public static class SpriteExtensions 
{
    public static void RefreshCollider(this GameObject obj)
    {
        PolygonCollider2D collider = obj.GetComponent<PolygonCollider2D>();
        Sprite sprite = obj.GetComponent<SpriteRenderer>().sprite;

        if (collider == null || sprite == null) return;

        // Set old paths to null
        for (int i = 0; i < collider.pathCount; i++) collider.SetPath(i, new Vector2[0]);

        List<Vector2> path = new List<Vector2>();
        // Set path count to path count of sprite's physics shape
        collider.pathCount = sprite.GetPhysicsShapeCount();
        // Copy over each path in sprite's physics shape
        for (int i = 0; i < collider.pathCount; i++)
        {
            path.Clear();
            sprite.GetPhysicsShape(i, path);
            collider.SetPath(i, path.ToArray());
        }
    }

    public static void ChangeSprite(this GameObject obj, Sprite sprite)
    {
        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null) return;

        spriteRenderer.sprite = sprite;
        obj.RefreshCollider();
    }
}
