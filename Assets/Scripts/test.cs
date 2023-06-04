using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public GameObject target;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 targetDir = target.transform.position - transform.position;
        //float angle = Vector3.SignedAngle(targetDir, transform.forward, transform.up);
        //Debug.Log("transform.forward: " + transform.forward);
        //Debug.Log("transform.right: " + transform.right);
        //Debug.Log("transform.right: " + transform.up);
        //Debug.Log("Horizontal: " +Input.GetAxis("Horizontal"));
        //Debug.Log("Vertical: " + Input.GetAxis("Vertical"));
    }

    private void OnTriggerEnter(Collider other)
    {
   
    }
}
