using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ST
{
    public class PickUpItem : MonoBehaviour
    {
        public Weapon weaponToEquip;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                collision.GetComponent<Player>().ChangeWeapon(weaponToEquip);
                Destroy(gameObject);
            }
        }
    }
}
