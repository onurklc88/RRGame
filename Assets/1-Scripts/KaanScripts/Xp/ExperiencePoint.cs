using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ExperiencePoint : MonoBehaviour
{
    public GameObject _character;


    [SerializeField] private float _speed;

    private void Start()
    {
        _character = GameObject.Find("Character");
    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _character.transform.position, Time.deltaTime * _speed);

        if (Vector3.Distance(transform.position, _character.transform.position) < .5f)
        {
            XPFunction();
        }
    }

    private void XPFunction()
    {
        Debug.Log("xp alindi");
        //xp alinca neler olcagi
        EconomyManager.instance.Money+=10;
        EventLibrary.ResetPooledObject?.Invoke(gameObject);
    }
}
