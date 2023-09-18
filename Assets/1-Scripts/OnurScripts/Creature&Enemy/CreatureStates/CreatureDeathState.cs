using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureDeathState : IState
{
   public void SetupState(Creature creature)
    {
        //Debug.Log("DeathState");
        var obj = ObjectPool.GetPooledObject(11);
        obj.transform.position = creature.transform.position;
        obj.SetActive(true);
        creature.GetComponent<Animator>().speed = 1;
        creature.NavMeshAgent.speed = 0;
        creature.CreatureAnimationController.PlayCreatureAnimation("Death", false);
        creature.StartCoroutine(DelayState(creature));

    }

   public IEnumerator DelayState(Creature creature)
    {
        yield return new WaitForSeconds(0.5f);
        creature.GetComponent<Animator>().enabled = false;
        creature.DissolveHandler.Dissolve();
    }

   
}
