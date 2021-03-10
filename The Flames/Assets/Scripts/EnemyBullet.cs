﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ST
{
    public class EnemyBullet : MonoBehaviour
    {
        private Player playerScript;
        private Vector2 targetPosition;

        public GameObject explosion;

        public float speed;
        public int damage;

        private void Start()
        {
            playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            targetPosition = playerScript.transform.position;
        }

        private void Update()
        {
            if (Vector2.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            }
            else
            {
                DestroyProjectile();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                playerScript.TakeDamage(damage);
                DestroyProjectile();
            }
        }

        private void DestroyProjectile()
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
