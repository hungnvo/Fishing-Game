using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodGizmo : MonoBehaviour
{
    public float gizmoSize = 0.75f;
    public Color gizmoColor = Color.yellow;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, gizmoSize);
    }
}
