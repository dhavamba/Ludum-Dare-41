using UnityEngine;
using DynamicLight2D;

[RequireComponent(typeof(DynamicLight))]

public class VisualCone : MonoBehaviour
{
    public LayerMask maskObstacle;
    [Range(0, 90)]
    public float angleCone;
    [Range(0, 100)]
    public float distance;
    public Transform enemy;

    private DynamicLight componentLight;
    private float angle;
    private Vector2 direction;

    private void Awake()
    {
        componentLight = GetComponent<DynamicLight>();
        transform.GetComponentInParent<Move>().OnChangeDirection += SetDirection;
    }


    // Update is called once per frame
    private void Update ()
    {
        SeeEnemy();
    }

    private void SetDirection(Vector2 dir)
    {
        direction = dir;
        DrawCone();
    }


    private void DrawCone()
    {
        angle = Mathf.Abs(90 - ExtendVector2.ReturnAngleToDir(direction));
        if (angle < 90 && direction.x > 0)
        {
            angle = 360 - angle;
        }
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);

        componentLight.RangeAngle = angleCone;
        componentLight.LightRadius = distance;
    }


    //Metodo per vedere Laika
    private void SeeEnemy()
    {
        Vector2 auxDifference = enemy.position - transform.position;

        //laika è nel cono visuale?
        if (Vector3.Angle(transform.GetComponentInParent<Move>().direction, auxDifference) < angleCone / 2 && Mathf.Abs(auxDifference.x) < distance)
        {
            float dist = Vector2.Distance(enemy.position, transform.position);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, auxDifference.normalized, dist, maskObstacle);

            if (hit.collider == null)
            {
                Debug.Log("hit");
            }
        }
    }
}
