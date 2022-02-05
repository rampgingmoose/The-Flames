using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicInstancer : MonoBehaviour
{
    private static MusicInstancer instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
