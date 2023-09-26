using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    

    public GameObject player;
    public MeleeWeapons currentWeapon;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        SaveInfo.Init();
    }
    void Start()
    {

        SaveInfo.Player.SelectedWeapon = currentWeapon;
    }

    
}
