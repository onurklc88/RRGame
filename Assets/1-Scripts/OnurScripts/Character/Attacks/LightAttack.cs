using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LightAttack : CharacterAttackState
{
    
    private float _firstAttacktime = 0f;
  
    private int _currentSwings;
    public bool CanCharacterSwing = true;
    private float _animationDelay = .3f;

    private float _lastDamage;

    public override void EnterState(CharacterStateManager character)
    {
        #region OldVersion
        /*
        if (_firstAttacktime == 0)
            _firstAttacktime = Time.time;
       else
          _firstAttacktime = ResetFirstAttackTime(Time.time);
        
        
        if(_currentSwings  == SaveInfo.Player.SelectedWeapon.SwingCount)
        {
           
            if (CheckTimeBetweenFirstAttack(Time.time))
            {
                //Debug.Log("Spam detected");
                CanCharacterSwing = false;
                 DOVirtual.DelayedCall(1f, () =>  {  CanCharacterSwing = true; });
                ExitState(character);
                _currentSwings = 0;
                _firstAttacktime = 0;
              

            }
            else
            {
                //Debug.Log("Spam is not detected");
                _currentSwings = 0;
                _firstAttacktime = 0;
                AttackBehaviour(character);
            }
        }
        else
        {
            _currentSwings++;
            
            AttackBehaviour(character);
        }
       // Debug.Log("CurrentSwing: " + _currentSwings);
        */
        #endregion
        character.CharacterContainer.Brush.SetActive(true);
        DoAttackBehaviour(character);
        EventLibrary.PlayAttackAnimation.Invoke(true);
    }

    public override void DoAttackBehaviour(CharacterStateManager character)
    {
        TrackCursorPosition(character);
         AttackDash(character);
        if (_collidedObject != null)
        {
            //buraya güncellenmiþ karakter strength giricez
            _lastDamage = character.CharacterProperties.LightAttackDamage + SaveInfo.UpgradeSave.StrengthDamage;

            character.ImpulseSource.GenerateImpulse(0.07f);
            EventLibrary.OnWeaponChargeUpdated.Invoke(true);
            _collidedObject.TakeDamage(_lastDamage);
            Debug.Log(_lastDamage);
        }
       
      
        character.StartCoroutine(DelayState(character));
    }
    /*
  private bool CheckTimeBetweenFirstAttack(float currentTime)
  {
          if ((currentTime - _firstAttacktime) < 2f)
              return true;
          else
              return false;
  }

  private float ResetFirstAttackTime(float intervalTime)
  {

      //Debug.Log("Time interval: " + (intervalTime - _firstAttacktime));
      if((intervalTime - _firstAttacktime) > 2f)
      {
        _currentSwings = 0;
        return 0f;
      }
      else
       return _firstAttacktime;

  }
  */

    
    public override IEnumerator DelayState(CharacterStateManager character)
    {
        yield return new WaitForSeconds(_animationDelay);
        EventLibrary.PlayAttackAnimation.Invoke(false);
        character.CharacterContainer.Brush.SetActive(false);
        _collidedObject = null;
        ExitState(character);

    }





}
