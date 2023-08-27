using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DepencyInstaller : MonoInstaller
{
    [SerializeField] private MouseTarget _mouseTarget;
    [SerializeField] private CharacterCollisions _characterCollisions;
    [SerializeField] private EnemyStateFactory _enemyStateFactory;
    public override void InstallBindings()
    {
        Container.Bind<MouseTarget>().FromComponentInHierarchy(_mouseTarget).AsSingle();
        Container.Bind<CharacterCollisions>().FromComponentInHierarchy(_characterCollisions).AsSingle();
        Container.Bind<EnemyStateFactory>().AsSingle();
        Container.Bind<CharacterStateFactory>().AsSingle();
    }
}
