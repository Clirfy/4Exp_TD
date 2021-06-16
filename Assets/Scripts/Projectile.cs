using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [field: SerializeField]
    private float Speed { get; set; }
    private int Damage { get; set; }
    private GameObject Target { get; set; }

    public void LaunchAtTarget(GameObject target, int damage)
    {
        Target = target.gameObject;
        Damage = damage;

        Target.GetComponent<EnemyController>().OnEnemyDestroy.AddListener(UnregisterEnemy);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, Speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Projectile hit Enemy");
        other.GetComponent<EnemyController>().TakeDamage(Damage);
        Destroy(gameObject);
    }

    private void UnregisterEnemy(EnemyController enemy)
    {
        enemy.OnEnemyDestroy.RemoveListener(UnregisterEnemy);
        Destroy(gameObject);
    }
}
