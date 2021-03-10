using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ST
{
    public class HealthPickUp : MonoBehaviour
    {
        Player player;

        public int healAmount;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                player.GetComponent<Player>().OnHealthPickUp(healAmount);
            }

            Destroy(gameObject);
        }
    }
}
