using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float scrollSpeed = 0.5f;

    Renderer renderArea;

    void Start()
    {
        renderArea = GetComponent<Renderer>();
    }

    void Update()
    {
        renderArea.material.mainTextureOffset = new Vector2(scrollSpeed * Time.time, scrollSpeed * Time.time);
    }
}
