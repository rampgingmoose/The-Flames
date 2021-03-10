using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ST
{
    public class BossEnemy : MonoBehaviour
    {
        public int health;
        public int halfHealth;

        public Enemy[] enemies;
        public float spawnPointOffSet;

        public GameObject bossDeathParticles;
        public GameObject bloodSplatter;
        private Animator anim;

        public int damage;

        private void Start()
        {
            anim = GetComponent<Animator>();

            halfHealth = health / 2;
        }

        public void TakeDamage(int amount)
        {
            health -= amount;

            if (health <= 0)
            {
                BossDeathVFX();
            }

            SecondStage();

            Enemy randomEnemy = enemies[Random.Range(0, enemies.Length)];
            Instantiate(randomEnemy, transform.position + new Vector3(spawnPointOffSet, spawnPointOffSet, 0), transform.rotation);
        }

        private void SecondStage()
        {
            if (health <= halfHealth)
            {
                anim.SetTrigger("stage2");
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                collision.GetComponent<Player>().TakeDamage(damage);
            }
        }

        private void BossDeathVFX()
        {
            Instantiate(bloodSplatter, transform.position, transform.rotation);
            Instantiate(bossDeathParticles, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
