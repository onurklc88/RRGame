using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public ObjectPool objectPool;

    public GameObject player;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {
        SaveInfo.Init();
        //SaveInfo._player.health = 10;
       // SaveInfo._player.Save();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
