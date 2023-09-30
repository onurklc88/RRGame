using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollisions : MonoBehaviour
{
    [HideInInspector] public GameObject TemporaryObject;
  

    private void OnTriggerEnter(Collider interactableObject)
    {
        

        switch (interactableObject.gameObject.layer)
        {
            case 8:
                //
                TemporaryObject = interactableObject.gameObject;
                break;
            case 9:
              
                break;
            case 12:
                
                //PanelOpened = true;
                //_playerInput.Disable();
                UpgradeManager.instance.Market.SetActive(true);
             
                break;
        }


    }

    private void OnTriggerExit(Collider other)
    {
        TemporaryObject = null;
    }

}
