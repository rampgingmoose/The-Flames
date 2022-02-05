using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ST
{
    public class PickUpItem : MonoBehaviour
    {
        public GameObject weaponToEquip;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.GetComponent<Player>().ChangeWeapon(weaponToEquip);
                Destroy(gameObject);
            }
        }
    }
}
