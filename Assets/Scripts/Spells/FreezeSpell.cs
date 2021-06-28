using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FreezeSpell : BaseSpellController
{
    [field: SerializeField]
    public float FreezeTime { get; set; }
    private float[] BaseSpeed { get; set; }
    private bool IsTimerAbleToCountDown { get; set; }

    public override void Update()
    {
        if (IsTimerAbleToCountDown == true)
        {
            FreezeTime -= Time.deltaTime;

            Unfreeze();

            return;
        }

        base.Update();
    }

    public override void CastSpell()
    {
        EnemyColliders = Physics.OverlapSphere(gameObject.transform.position, Radius * 2f, LayerData.EnemyLayerMask);
        BaseSpeed = new float[EnemyColliders.Length];

        for (int i = 0; i < EnemyColliders.Length; i++)
        {
            BaseSpeed[i] = EnemyColliders[i].gameObject.GetComponent<NavMeshAgent>().speed;
            EnemyColliders[i].gameObject.GetComponent<NavMeshAgent>().speed = 0.0f;

            EnemyColliders[i].GetComponent<EnemyController>().TakeDamage(Damage);
        }

        IsTimerAbleToCountDown = true;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    private void Unfreeze()
    {
        if (FreezeTime < 0.0f)
        {
            ReturnToEnemyNormalMoveSpeed();

            Destroy(gameObject);
        }
    }

    private void ReturnToEnemyNormalMoveSpeed()
    {
        for (int i = 0; i < EnemyColliders.Length; i++)
        {
            if (EnemyColliders[i] != null)
            {
                EnemyColliders[i].gameObject.GetComponent<NavMeshAgent>().speed = BaseSpeed[i];
            }
        }
    }
}