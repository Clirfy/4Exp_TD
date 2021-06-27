using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpell : BaseSpellController
{
    public override void CastSpell()
    {
        EnemyColliders = Physics.OverlapSphere(gameObject.transform.position, Radius * 2f, LayerData.EnemyLayerMask);

        for (int i = 0; i < EnemyColliders.Length; i++)
        {
            EnemyColliders[i].GetComponent<EnemyController>().TakeDamage(Damage);
        }

        base.CastSpell();
    }
}
