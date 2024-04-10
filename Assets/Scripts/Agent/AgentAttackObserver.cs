using System.Collections.Generic;
using UnityEngine;


public class AgentAttackObserver : MonoBehaviour
{

    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private float _raycastDistance = 0.25f;

    private Vector2 direction;
    private Vector2 origin;

    public List<IDamageable> CheckTargets(Vector2 castDirection)
    {
        List<IDamageable> targets = new List<IDamageable>();

        origin = transform.position;
        direction = castDirection;

        RaycastHit2D[] hits = Physics2D.RaycastAll(origin, direction, _raycastDistance, _targetLayer);

        foreach (RaycastHit2D hit in hits)
        {
            IDamageable damageable = hit.collider.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                targets.Add(damageable);
            }

            Debug.DrawRay(origin, direction * _raycastDistance, Color.red);
        }

        return targets;
    }

    void Update()
    {
        Debug.DrawRay(origin, direction * _raycastDistance, Color.green);
    }
}
