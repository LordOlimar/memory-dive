using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioClipTriggers : MonoBehaviour
{
  
       public AudioSource source; // Assign in Inspector
         public AudioClip collisionSound; // enter sound
         public AudioClip leaveCollisionSound; // exit sound
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) //assign player?
        {
            source.PlayOneShot(collisionSound);
        }
    }
    void OnCollisionExit (Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            source.PlayOneShot(leaveCollisionSound);
        }
    }
}