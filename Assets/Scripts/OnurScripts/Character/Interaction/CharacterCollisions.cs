using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollisions : MonoBehaviour
{

    private void OnTriggerEnter(Collider interactableObject)
    {
        switch (interactableObject.gameObject.layer)
        {
            case 8:
                
                break;
        }
    }
}
