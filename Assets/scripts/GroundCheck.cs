using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{ 
    PlayerController _controller;

    private void Awake()
    {
        _controller = GetComponentInParent<PlayerController>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _controller.gameObject)
            return;
                
        _controller.SetGroundedState(true);
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _controller.gameObject)
            return;
        
        _controller.SetGroundedState(false); 
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == _controller.gameObject)
            return;
        
        _controller.SetGroundedState(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == _controller.gameObject)
            return;
                
        _controller.SetGroundedState(true);   
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject == _controller.gameObject)
            return;
                
        _controller.SetGroundedState(false);       
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject == _controller.gameObject)
            return;
                
        _controller.SetGroundedState(true);
        
    }
}
