using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ST
{
    public class MeleeEnemy : Enemy
    {
        public float stoppingDistance;

        private float attackTime;
        public float attackSpeed;

        private AudioSource audioSource;

        private void Awake()
        {
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
                else
                {
                    if (Time.time >= attackTime)
                    {
                        audioSource.Play();

                        StartCoroutine(Attack());
                        attackTime = Time.time + timeBetweenAttacks;
                    }
                }
            }
        }

        IEnumerator Attack()
        {
            player.GetComponent<Player>().TakeDamage(damage);

            Vector2 originalPosition = transform.position;
            Vector2 targetPosition = player.position;

            float percent = 0;
            while (percent <= 1)
            {
                percent += Time.deltaTime * attackSpeed;
                float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
                transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);
                yield return null;
            }
        }
    }
}
