using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        private AudioSource audioSource;
        private Slider healthBar;

        public int damage;

        private bool bossIsDead;

        private void Start()
        {
            anim = GetComponent<Animator>();
            audioSource = GetComponentInChildren<AudioSource>();
            healthBar = FindObjectOfType<Slider>();
            SetHealthBarValues();

            halfHealth = health / 2;
        }

        private void SetHealthBarValues()
        {
            healthBar.maxValue = health;
            healthBar.value = health;
        }

        public void TakeDamage(int amount)
        {
            health -= amount;
            healthBar.value = health;

            if (health <= 0)
            {
                BossDeathVFX();
            }

            if (health <= halfHealth)
            {
                SecondStage();
            }

            SpawnMinion();
        }

        private void SpawnMinion()
        {
            Enemy randomEnemy = enemies[Random.Range(0, enemies.Length)];
            Instantiate(randomEnemy, transform.position + new Vector3(spawnPointOffSet, spawnPointOffSet, 0), transform.rotation);
            audioSource.Play();
        }

        private void SecondStage()
        {
            anim.SetTrigger("stage2");
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
            healthBar.gameObject.SetActive(false);
        }
    }
}
