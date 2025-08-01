using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanceManager : MonoBehaviour
{
    [SerializeField] private int countOfSheeps;

    Collider2D collider2D;

    void Start()
    {
        collider2D = GetComponent<Collider2D>();
    }

    public void Check()
    {
        List<Collider2D> results = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(LayerMask.GetMask("Wolf","Sheep"));
        filter.useLayerMask = true;

        int count = collider2D.OverlapCollider(filter, results);

        foreach (Collider2D hit in results)
        {
            if (hit.CompareTag("Wolf"))
            {
                Debug.Log("WOLF!");
            }
        }
    }
}
