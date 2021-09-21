using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPoint : MonoBehaviour
{
    // donne un point ou un rayon autour de lui

    [Range(0.1f,10f)] 
    public float radius = 1f;

    public Vector3 GivePoint()
    {
        Vector3 point = Random.insideUnitCircle * radius;
        point.z = point.y;
        point.y = 0;

        point += transform.position; // point par rapport au local et non au monde
        return point;
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = new Color(0f,0.5f,0.9f,0.4f);
        Gizmos.DrawSphere(transform.position, radius); // => (position, longueur)
    }
}
