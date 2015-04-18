using UnityEngine;
using System;

public class ParticleSystemAutoDestruct : MonoBehaviour
{
     private ParticleSystem particleSystem;
 
 
     public void Start() 
     {
         particleSystem = GetComponent<ParticleSystem>();
     }
 
     public void Update() 
     {
         if(particleSystem != null)
         {
             if(!particleSystem.IsAlive())
             {
                 Destroy(gameObject);
             }
         }
     }
}