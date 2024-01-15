using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Projectile projectile;

    public GameObject projectilePrefab;
    public GameObject deadCrabPrefab;
    public float projectileSpeed = 5f;
    private GameObject ınstantiateProjectile;
    private GameObject ınstantiateCrab;
    private bool Collide;

    public void Start()
    {
        projectile = projectilePrefab.GetComponent<Projectile>();
    }

    void Update()
    {
        if ((playerMovement.isHoldingCrab) && Input.GetKeyDown(KeyCode.E))
        {
            SpawnCrab(deadCrabPrefab);
            playerMovement.isHoldingCrab = false;
        }

        else
        {
            if (Input.GetKeyDown(KeyCode.E) && (ınstantiateProjectile == null || !ınstantiateProjectile.activeSelf))
            {
                SpawnProjectile(projectilePrefab);
            }
        }
    }

    public void Shoot()
    {
        if ((playerMovement.isHoldingCrab))
        {
            SpawnCrab(deadCrabPrefab);
            playerMovement.isHoldingCrab = false;
        }

        else
        {
            if  (ınstantiateProjectile == null || !ınstantiateProjectile.activeSelf)
            {
                SpawnProjectile(projectilePrefab);
            }
        }
    }

   public void SpawnProjectile(GameObject gameObject)
    {
        
        SoundManager.Instance.PlaySound("Shoot");
        ınstantiateProjectile = Instantiate(gameObject, transform.position, transform.rotation);
        Rigidbody2D rb = ınstantiateProjectile.GetComponent<Rigidbody2D>();

        rb.velocity = transform.right * projectileSpeed;
    }

    void SpawnCrab(GameObject gameObject)
    {
        ınstantiateCrab = Instantiate(gameObject, transform.position, transform.rotation);
    }
}