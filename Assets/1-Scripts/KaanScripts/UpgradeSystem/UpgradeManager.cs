using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;
    [Inject]
    PlayerInput _playerInput;

    public GameObject Market;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

 
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && Market.activeInHierarchy)
        {
            _playerInput.Enable();
            Market.SetActive(false);
        }
       
    }
}
