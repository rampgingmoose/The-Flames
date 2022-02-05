using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ST
{
    public class Summoner : Enemy
    {
        public float minX = -103f;
        public float maxX = 107f;
        public float minY = -63f;
        public float maxY = 53f;
        public float stoppingDistance = 10f;
        public float attackSpeed;
        private float timer;

        Vector2 targetPosition;
        Animator anim;

        public Enemy enemyToSummon;

        private AudioSource audioSource;

        private void Awake()
        {
            anim = GetComponent<Animator>();
            audioSource = GetComponentInChildren<AudioSource>();
        }

        public override void Start()
        {
            base.Start();
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);
            targetPosition = new Vector2(randomX, randomY);
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            if (player != null)
            {
                if (Vector2.Distance(transform.position, targetPosition) > 0.5f)
                {
                    transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                    anim.SetBool("isRunning", true);
                }
                else
                {
                    anim.SetBool("isRunning", false);

                    if (Time.time >= summonTime)
                    {
                        summonTime = Time.time + timeBetweenSummons;
                        anim.SetTrigger("summon");
                    }
                }

                if (Vector2.Distance(transform.position, player.position) < stoppingDistance)
                {
                    if (Time.time > timer)
                    {
                        timer = Time.time + timeBetweenAttacks;
                        StartCoroutine(MeleeAttack());
                    }
                }
            }
        }

        public void Summon()
        {
            if (player != null)
            {
                Instantiate(enemyToSummon, transform.position, transform.rotation);
                audioSource.Play();
            }
        }

        IEnumerator MeleeAttack()
        {
            player.GetComponent<Player>().TakeDamage(damage);

            Vector2 originalPosition = transform.position;
            Vector2 targetPosition = player.position;

            float percent = 0;
            while (percent <= 1)
            {
                percent += Time.deltaTime * attackSpeed;
                float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
                transform.position = Vector2.Lerp(transform.position, player.position, formula);
                yield return null;
            }
        }
    }
}
