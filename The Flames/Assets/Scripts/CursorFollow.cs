using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ST
{
    public class CursorFollow : MonoBehaviour
    {
        private void Start()
        {
            Cursor.visible = false;
        }
        private void Update()
        {
            transform.position = Input.mousePosition;
        }
    }
}