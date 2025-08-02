using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinityMusic : MonoBehaviour
{
    public static InfinityMusic instance;

    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
