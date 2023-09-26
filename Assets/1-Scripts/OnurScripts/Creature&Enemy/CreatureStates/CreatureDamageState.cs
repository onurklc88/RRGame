using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CreatureDamageState : IState
{
    public DamageType.Damage CurrentDamageType;
    public void SetupState(Creature creature)
    {
      creature.CreatureAnimationController.PlayCreatureAnimation("Damage", true);
      HandleDamage(creature);
    }

    private IEnumerator DelayState(Creature creature)
    {
        yield return new WaitForSeconds(1f);
        if (creature.CurrentCreatureState != creature.EnemyStateFactory.Death)
        {
           creature.SwitchState(creature.EnemyStateFactory.Chase);
        }
          
    }

    private void HandleDamage(Creature creature)
    {
        if(CurrentDamageType == DamageType.Damage.Bomb)
        {
            Debug.Log("bomb");
            creature.transform.DOJump(creature.transform.position + -creature.transform.forward * 0.5f, 0.5f, 1, 0.5f).OnComplete(() =>
            {
                creature.StartCoroutine(DelayState(creature));
            });
        }
        else
        {
           creature.transform.DOJump(creature.transform.position + -creature.transform.forward * 7f, 1f, 1, 0.5f).OnComplete(() =>
            {
                creature.StartCoroutine(DelayState(creature));
            });
        }

       
    }


}
