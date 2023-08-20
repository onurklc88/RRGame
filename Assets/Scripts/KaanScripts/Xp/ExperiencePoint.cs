using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperiencePoint : MonoBehaviour
{
    private Transform _player;
    [SerializeField] private float _speed;


    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,_player.position, Time.deltaTime * _speed);  

        if(Vector3.Distance(transform.position, _player.position) < .5f)
        {
            XPFunction();  
        }
    }

    private void XPFunction()
    {
        Debug.Log("xp alindi");
        //xp alinca neler olcagi

        Destroy(gameObject);
    }
}
