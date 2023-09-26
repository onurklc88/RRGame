using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelManager : MonoBehaviour
{

    [Inject]
    EnemyFactory _enemyFactory;

    public GameObject _creaturePosition;

    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }

    private void Start()
    {
        var enemy = _enemyFactory.Create();
        enemy.transform.position = _creaturePosition.transform.position;
    }

}
