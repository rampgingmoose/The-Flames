using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ST
{
    public class Weapon : MonoBehaviour
    {
        public GameObject projectile;
        public Transform shootPoint;
        Animator cameraAnim;

        public float shotCoolDown; //Time needed to pass between shots
        private float shotTime;
        private float angleOffSet = 90;

        private void Start()
        {
            cameraAnim = Camera.main.GetComponent<Animator>();
        }
        private void Update()
        {
            HandleWeaponRotation();
            HandleProjectile();
        }

        private void HandleWeaponRotation()
        {
            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle - angleOffSet, Vector3.forward);
            transform.rotation = rotation;
        }

        private void HandleProjectile()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Time.time >= shotTime)
                {
                    Instantiate(projectile, shootPoint.position, transform.rotation);
                    shotTime = Time.time + shotCoolDown;
                    cameraAnim.SetTrigger("shake");
                }
            }
        }
    }
}
