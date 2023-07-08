using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAttack : CharacterAttackState
{
    private int _spamCount;
    private int _maxSpamCount = 2;
    private float _firstAttacktime = 0;
    private bool _spammed;
    
    public override void EnterState(CharacterStateManager character)
    {

        /*
        if(_firstAttacktime == 0)
        {
            _firstAttacktime = Time.time;
           // Debug.Log("_first Attack time: " + _firstAttacktime);
        }
        
        if(_spamCount == _maxSpamCount)
        {
            _spamCount = 0;
            if (!CheckTimeBetweenFirstAttack(Time.time))
            {
                _firstAttacktime = 0;
                AttackBehaviour(character);
            }
            else
            {
                Debug.Log("SpamDetected");
              //  character.StartCoroutine(DelayState(character));
                ExitState(character);
            }
          
        }
        else
        {
            _spamCount++;
            AttackBehaviour(character);
        }
        */
        AttackBehaviour(character);
    }

    public override void AttackBehaviour(CharacterStateManager character)
    {
        Debug.Log("Ligt Attack");
        AttackRange(character);
        TrackCursorPosition(character);
        if (CollidedObject != null) { CollidedObject.TakeDamage(character.CharacterProperties.LightAttackDamage); }
        AttackDash(character);
        character.StartCoroutine(DelayState(character));
    }

    private bool CheckTimeBetweenFirstAttack(float lastTime)
    {

        //Debug.Log("First attack time: "+ _firstAttacktime+ " Last Attack Time: " +lastTime);
        Debug.Log("Difference: " + (lastTime - _firstAttacktime));
       

            if ((lastTime - _firstAttacktime) < 1.2f)
            {
            _spammed = true;
           // character.StartCoroutine(DelayState(character));
            return true;
            }
             else
            return false;
    }

   

}
