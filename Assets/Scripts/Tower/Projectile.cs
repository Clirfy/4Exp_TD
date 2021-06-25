using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [field: SerializeField]
    private float Speed { get; set; }
    private int Damage { get; set; }
    private EnemyController Target { get; set; }

    public void LaunchAtTarget(EnemyController target, int damage)
    {
        Target = target;
        Damage = damage;

        Target.OnEnemyDestroy.AddListener(OnEnemyDestroyedBeforeReached);
    }

    private void Update()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        Vector3 cachedThisPosition = transform.position;
        Vector3 cachedTargetPosition = Target.transform.position;

        float step = Speed * Time.deltaTime;
        Vector3 FaceTarget = (cachedTargetPosition - cachedThisPosition).normalized;

        transform.position = Vector3.MoveTowards(cachedThisPosition, cachedTargetPosition, step);
        transform.rotation = Quaternion.LookRotation(FaceTarget);
    }

    private void OnTriggerEnter(Collider other)
    {
        Target.TakeDamage(Damage);
        Destroy(gameObject);
    }

    private void OnEnemyDestroyedBeforeReached(EnemyController enemy)
    {
        Destroy(gameObject);
    }
}
