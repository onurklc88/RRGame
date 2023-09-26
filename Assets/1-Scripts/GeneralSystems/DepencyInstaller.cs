using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DepencyInstaller : MonoInstaller
{
    [SerializeField] private CharacterStateManager _character;
    [SerializeField] private MouseTarget _mouseTarget;
    [SerializeField] private CharacterCollisions _characterCollisions;
    [SerializeField] private EnemyStateFactory _enemyStateFactory;
    [SerializeField] private WeaponHandler _weaponHandler;
    //[SerializeField] private GameObject _grimMort;
   
    public override void InstallBindings()
    {
        Container.Bind<MouseTarget>().FromComponentInHierarchy(_mouseTarget).AsSingle();
        Container.Bind<CharacterCollisions>().FromComponentInHierarchy(_characterCollisions).AsSingle();
        Container.Bind<EnemyStateFactory>().AsSingle();
        Container.Bind<CharacterStateFactory>().AsSingle();
        Container.Bind<WeaponHandler>().FromComponentInHierarchy(_weaponHandler).AsSingle();
        Container.Bind<CharacterStateManager>().FromComponentInHierarchy(_character).AsSingle();
        Container.Bind<Weapons>().AsSingle();
        //Container.BindFactory<Creature, EnemyFactory>().FromComponentInNewPrefab(_grimMort);
    }
}
