using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        
    }

    private void Start()
    {
        transform.position = GameManager.instance.player.transform.position;
        transform.rotation = GameManager.instance.player.transform.rotation;
        StartCoroutine(Close());
    }
    private void Update()
    {
        

        rb.AddRelativeForce(Vector3.forward * 100 * Time.deltaTime,ForceMode.Impulse);

    }

    IEnumerator Close()
    {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }
}
