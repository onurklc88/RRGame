using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clawmarcher : Creature
{
    private bool _playerDetection;
    

    private void Start()
    {
       SetCreatureProperties();
       
    }

    private void Update()
    {
        
    }


    public override void ExecuteState()
    {
        
    }

    public override void SetCreatureProperties()
    {
        gameObject.GetComponent<SphereCollider>().radius = EnemyProperties.ChaseArea;

    }
   
   

   
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            CheckDistanceBetweenPlayer(other.transform.position);
        }
       
    }

    private void CheckDistanceBetweenPlayer(Vector3 playerPosition)
    {
         float distanceBetweenPlayer = Vector3.Distance(playerPosition, transform.position);
        Debug.Log("Distance Between Player: " + distanceBetweenPlayer);
         

    }
    


}
