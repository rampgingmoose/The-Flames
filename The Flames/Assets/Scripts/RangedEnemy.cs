﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ST
{
    public class RangedEnemy : Enemy
    {
        public float stoppingDistance;
        public float attackTime;

        public Transform shotPoint;

        public GameObject projectile;

        private AudioSource audioSource;

        Animator anim;

        public override void Start()
        {
            base.Start();
            anim = GetComponent<Animator>();
            audioSource = GetComponentInChildren<AudioSource>();
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            if (player != null)
            {
                if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                }
            }

            if (Time.time >= attackTime)
            {
                attackTime = Time.time + timeBetweenAttacks;
                anim.SetTrigger("attack");
            }
        }

        public void RangedAttack()
        {
            Vector2 direction = player.position - shotPoint.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            shotPoint.rotation = rotation;

            audioSource.Play();
            Instantiate(projectile, shotPoint.position, shotPoint.rotation);
        }
    }
}
