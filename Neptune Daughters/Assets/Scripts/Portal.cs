using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Portal : MonoBehaviour
{

     [SerializeField] private PlayerMovement playerMovement;
     
     public GameObject SpawnPoint;
     public CinemachineVirtualCamera activeCamera;
     public CinemachineVirtualCamera firstCamera;
     public CinemachineVirtualCamera secondCamera;
     private void OnTriggerEnter2D(Collider2D col)
     {
          if (col.CompareTag("Player"))
          {
               col.transform.position = SpawnPoint.transform.position;
               playerMovement.AddOxygen(2000000);
               firstCamera.gameObject.SetActive(false);
               secondCamera.gameObject.SetActive(false);
               
               activeCamera.gameObject.SetActive(true);
             
          }
     }
     
}
