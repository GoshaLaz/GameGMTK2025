using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FanceManager : MonoBehaviour
{
    [SerializeField] private int countOfSheeps;
    [Space]
    [SerializeField] private GameObject crossObject;
    [SerializeField] private Vector2 offset;
    [SerializeField] private float timeToDelete;

    PolygonCollider2D checkArea;

    private LevelChanger levelChanger;

    void Start()
    {
        checkArea = GetComponent<PolygonCollider2D>();

        GameObject gm = GameObject.Find("Main Camera");
        levelChanger = gm.GetComponent<LevelChanger>();
        

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

        foreach (Collider2D hit in results)
        {
            if (hit.CompareTag("Wolf"))
            {
                GameObject crossAboveWolf = Instantiate(crossObject, hit.gameObject.transform.position + (Vector3)offset, Quaternion.identity);
                Destroy(crossAboveWolf, timeToDelete);

                return;
            }
        }

        if (count >= countOfSheeps)
        {
            levelChanger.WinGame();
        }
    }
}
