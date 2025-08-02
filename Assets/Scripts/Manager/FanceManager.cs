using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class FanceManager : MonoBehaviour
{
    [SerializeField] private int countOfSheeps;
    [Space]
    [SerializeField] private GameObject crossObject;
    [SerializeField] private Vector2 offset;
    [SerializeField] private float timeToDelete;
    [Space]
    [SerializeField] private GameObject wrongSFX;

    PolygonCollider2D checkArea;

    List<GameObject> fancesOnScene = new List<GameObject>();

    void Start()
    {
        checkArea = GetComponent<PolygonCollider2D>();
    }

    public void Check(List<List<Vector2>> pointsOfFances)
    {
        checkArea.pathCount = pointsOfFances.Count;

        for (int indexOfCurrentPath = 0; indexOfCurrentPath < pointsOfFances.Count; indexOfCurrentPath++)
        {
            List<Vector2> points = pointsOfFances[indexOfCurrentPath];
            List<Vector2> pathPoints = new List<Vector2>();

            foreach (Vector2 point in points) 
            {
                pathPoints.Add(point);
            }

            checkArea.SetPath(indexOfCurrentPath, pathPoints);
        }

        List<Collider2D> results = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(LayerMask.GetMask("Wolf","Sheep"));
        filter.useLayerMask = true;

        int count = checkArea.OverlapCollider(filter, results);
        bool foundWolf = false;

        foreach (Collider2D hit in results)
        {
            if (hit.CompareTag("Wolf"))
            {
                GameObject crossAboveWolf = Instantiate(crossObject, hit.gameObject.transform.position + (Vector3)offset, Quaternion.identity);
                Destroy(crossAboveWolf, timeToDelete);
                
                foundWolf = true;
            }
        }

        if (foundWolf)
        {
            GameObject wrongSFXOnWolf = Instantiate(wrongSFX, Vector3.zero, Quaternion.identity);
            Destroy(wrongSFXOnWolf, timeToDelete);

            return;
        }

        if (count >= countOfSheeps)
        {
            Debug.Log("Next lvl");
            //Next Lvl
        }
    }

    public void AddNewFance(GameObject newFance)
    {
        fancesOnScene.Add(newFance);
    }

    public bool CanUndo()
    {
        return fancesOnScene.Count > 0;
    }

    public GameObject GetPrevFance()
    {
        if (fancesOnScene.Count > 1) {
            GameObject lastFance = fancesOnScene[fancesOnScene.Count - 1];
            fancesOnScene.Remove(lastFance);
            Destroy(lastFance);

            return fancesOnScene[fancesOnScene.Count - 1];
        } else
        {
            GameObject lastFance = fancesOnScene[fancesOnScene.Count - 1];
            fancesOnScene.Remove(lastFance);
            Destroy(lastFance);

            return null;
        }
    }
}
