using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    
    public void DragFactor(Vector3 origin,Vector3 dir, float dis)
    {
        transform.position = origin;
        dir.Normalize();
        float angle = 180 - Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(90, angle,0);

        spriteRenderer.size = new Vector2(dis, spriteRenderer.size.y);
    }

    public void Reset()
    {
        
    }
}
