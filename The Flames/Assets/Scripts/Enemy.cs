using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ST
{
    public class Enemy : MonoBehaviour
    {
        public int health;
        public int damage;
        public int pickUpChance;
        public int healthPickUpChance;

        public GameObject[] pickUps;
        public GameObject healthPickUp;
        public GameObject enemyDeathParticles;

        [HideInInspector]
        public Transform player;

        public float speed;

        public float timeBetweenAttacks;
        public float summonTime;
        public float timeBetweenSummons;

        public virtual void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        public void TakeDamage(int damageAmount)
        {
            health -= damageAmount;

            if (health <= 0)
            {
                HandlePickUps();
                Destroy(gameObject);
                Instantiate(enemyDeathParticles, transform.position, transform.rotation);
            }
        }

        private void HandlePickUps()
        {
            int randomNumber = Random.Range(0, 101);

            if (randomNumber < pickUpChance)
            {
                GameObject randomPickUp = pickUps[Random.Range(0, pickUps.Length)];
                Instantiate(randomPickUp, transform.position, transform.rotation);
            }

            int randomHealth = Random.Range(0, 101);
            if (randomHealth < healthPickUpChance)
            {
                Instantiate(healthPickUp, transform.position, transform.rotation);
            }

            Destroy(this.gameObject);
        }
    }
}
