using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ST
{
    public class FireBall : MonoBehaviour
    {
        public float speed;
        public float lifeTime;

        public int damage;

        public GameObject explosion;

        private void Start()
        {
            Invoke("DestroyProjectile", lifeTime);
        }

        private void Update()
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }

        private void DestroyProjectile()
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Enemy")
            {
                collision.GetComponent<Enemy>().TakeDamage(damage);
                DestroyProjectile();
            }

            if ( collision.tag == "Boss")
            {
                collision.GetComponent<BossEnemy>().TakeDamage(damage);
                DestroyProjectile();
            }
        }
    }
}
