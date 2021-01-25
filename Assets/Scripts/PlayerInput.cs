using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float forceMultiplyer = 100;
    public float maxDragDistance = 5;
    //public Arrow arrow;
    public Camera cam;
    public LayerMask touchableMask;
    public SpriteRenderer circle;
    public SpriteRenderer arrow;

    Shooter currentSelectedShooter;
    bool mouseHit = false;

    Ray _ray() => cam.ScreenPointToRay(Input.mousePosition);

    Vector3 lastDragPoint;
    private float dragDistance;
    private float raduis;

    void Start()
    {
        CloseRenderers();
    }

    
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            lastDragPoint = GetMouseWorldPosition();
            mouseHit = CheckMouseHit(Input.mousePosition);
            
        }else if (Input.GetMouseButtonUp(0))
        {
            if (mouseHit)
            {
                Vector3 dragDirection = currentSelectedShooter.transform.position - GetMouseWorldPosition();
                if(dragDistance > 0)
                    currentSelectedShooter.MoveToward(dragDirection.normalized, dragDistance * forceMultiplyer);
                CloseRenderers();
            }
            mouseHit = false;
        }
        else if (Input.GetMouseButton(0))
        {
            if (mouseHit)
            {
                Vector3 dragDirection = currentSelectedShooter.transform.position - GetMouseWorldPosition();

                
                dragDistance = dragDirection.magnitude;
                dragDistance = Mathf.Clamp(dragDistance, raduis, maxDragDistance);
                dragDistance -= raduis;

                Vector3 arrowOrigin = currentSelectedShooter.transform.position + (dragDirection.normalized * (raduis + .2f));
                DragFactor(arrowOrigin, -dragDirection, dragDistance * 4f);
            }
        }

        
    }

    public void DragFactor(Vector3 origin, Vector3 dir, float dis)
    {

        if(dis <= 0)
        {
            CloseRenderers();
            return;
        }

        circle.enabled = true;
        arrow.enabled = true;
        arrow.transform.position = origin;
        dir.Normalize();
        float angle = 180 - Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
        arrow.transform.rotation = Quaternion.Euler(90, angle, 0);
        arrow.size = new Vector2(dis, arrow.size.y);

        circle.transform.position = currentSelectedShooter.transform.position;
        circle.transform.localScale = Vector3.one * dis * .5f;
    }

    void CloseRenderers()
    {
        circle.enabled = false;
        arrow.enabled = false;
    }

    public Vector3 GetMouseWorldPosition()
    {
        Ray ray = _ray();
        Plane ground = new Plane(Vector3.up, Vector3.zero);
        float disTance;
        ground.Raycast(ray, out disTance);
        Vector3 worldPos = ray.GetPoint(disTance);
        return new Vector3(worldPos.x, worldPos.y, worldPos.z);
    }


    bool CheckMouseHit(Vector3 screenSpacePosition)
    {
        Ray ray = cam.ScreenPointToRay(screenSpacePosition);
        if(Physics.Raycast(ray, out RaycastHit hit, 1000, touchableMask))
        {
            currentSelectedShooter = hit.collider.gameObject.GetComponentInParent<Shooter>();
            raduis = currentSelectedShooter.transform.lossyScale.x * .5f;
            if(currentSelectedShooter.isTurn)
                return true;
        }
        return false;
    }
}
