using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ST
{
    public class Player : MonoBehaviour
    {
        public float speed;

        public int health;

        public int weaponsInInventory;
        GameObject[] weaponCount;

        private Rigidbody2D rigidBody;
        private Animator anim;
        public Animator hurtAnim;
        private AudioSource audioSource;

        private Vector2 moveAmount;

        Weapon weapon;

        public Image[] hearts;
        public Sprite fullHearts;
        public Sprite emptyHearts;

        public GameObject playerDeathParticles;

        private SceneTransitions sceneTransitions;

        private void Start()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            weapon = GetComponentInChildren<Weapon>();
            audioSource = GetComponent<AudioSource>();
            sceneTransitions = FindObjectOfType<SceneTransitions>();
        }

        private void Update()
        {
            HandleMovementInput();
        }

        private void FixedUpdate()
        {
            HandleFixedMoveAmount();
        }

        private void HandleMovementInput()
        {
            Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            moveAmount = moveInput.normalized * speed;

            if (moveInput != Vector2.zero)
            {
                anim.SetBool("isRunning", true);
            }
            else
            {
                anim.SetBool("isRunning", false);
            }
        }

        private void HandleFixedMoveAmount()
        {
            rigidBody.MovePosition(rigidBody.position + moveAmount * Time.fixedDeltaTime);
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            hurtAnim.SetTrigger("hurt");
            UpdateHealthUI(health);
            if (health <= 0)
            {                
                Destroy(this.gameObject);
                Instantiate(playerDeathParticles, transform.position, transform.rotation);
                sceneTransitions.LoadScene("LoseScene");
            }
        }

        public void ChangeWeapon(GameObject weaponToEquip)
        {
            if (weaponToEquip != null)
            {
                Destroy(GameObject.FindGameObjectWithTag("Weapon"));
                Instantiate(weaponToEquip, transform.position, transform.rotation, transform);                
            }
        }

        private void UpdateHealthUI(int currentHealth)
        {
            for (int i = 0; i < hearts.Length; i++)
            {
                if (i < currentHealth)
                {
                    hearts[i].sprite = fullHearts;
                }
                else
                {
                    hearts[i].sprite = emptyHearts;
                }
            }
        }

        public void OnHealthPickUp(int healAmount)
        {
            if (health >= 5)
            {
                health = 5;
            }
            else
            {
                health += healAmount;
            }

            UpdateHealthUI(health);
        }
    }
}
