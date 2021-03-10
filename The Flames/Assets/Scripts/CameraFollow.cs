using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ST
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform playerTransform;

        public float followSpeed;
        public float minX;
        public float maxX;
        public float minY;
        public float maxY;

        private void Start()
        {
            transform.position = playerTransform.position;
        }

        private void Update()
        {
            HandleFollowPlayer();
        }

        private void HandleFollowPlayer()
        {
            if (playerTransform != null)
            {
                float clampedX = Mathf.Clamp(playerTransform.position.x, minX, maxX);
                float clampedY = Mathf.Clamp(playerTransform.position.y, minY, maxY);

                transform.position = Vector2.Lerp(transform.position, new Vector2(clampedX, clampedY), followSpeed);
            }
        }
    }
}