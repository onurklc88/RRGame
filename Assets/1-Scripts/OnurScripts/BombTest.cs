using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTest : MonoBehaviour
{
    Rigidbody _rb;
    public float power;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        
     
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            _rb.isKinematic = false;
            // Vector3 test = transform.forward + Vector3.up * 2f;
            Quaternion firlatmaRotasyonu = Quaternion.Euler(new Vector3(43f, -52f, 28f));
            _rb.rotation = firlatmaRotasyonu;

            _rb.AddForce(transform.forward * power, ForceMode.Impulse);
        }
    }

    // Update is called once per frame

}
