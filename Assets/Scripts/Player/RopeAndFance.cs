using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RopeAndFance : MonoBehaviour
{
    [Header("Fance")]
    [SerializeField] private int countOfFances;
    [Space(5)]
    [SerializeField] private GameObject fanceObject;
    [SerializeField] private float interactRadius;
    [Space(5)]
    [SerializeField] private LayerMask fanceLayer;

    [Header("Rope")]
    [SerializeField] private float ropeLenght;
    [Space(5)]
    [SerializeField] private Material ropeMaterial;
    [Space(5)]
    [SerializeField] private LayerMask badLayer;
    [SerializeField] private Color badColorRope;
    [SerializeField] private float animationTime;

    List<List<Vector2>> pointsOfFances = new List<List<Vector2>>();

    FanceManager fanceManager;

    PlayerMove playerMovementScript;
    bool firstFance;
    LineRenderer ropeRender;
    GameObject firstFanceObject;

    bool canPlaceFance;
    bool isAnimatingColor = false;

    void Start()
    {
        fanceManager = GameObject.FindWithTag("FanceManager").GetComponent<FanceManager>();
        playerMovementScript = GetComponent<PlayerMove>();
        firstFance = true;
    }

    void Update()
    {
        if (ropeRender != null)
        {
            ropeRender.SetPosition(1, transform.position + new Vector3(0, 0.25f, 0));
            if (!isAnimatingColor)
            {
                if (!canPlaceFance) StartCoroutine(ChangeColor(badColorRope));
                else StartCoroutine(ChangeColor(Color.white));
            }
        }

        canPlaceFance = CanPlaceFance();

        if (Input.GetKeyDown(KeyCode.E) && !playerMovementScript.isMoving && canPlaceFance)
        {
            if (firstFance && countOfFances >= 3)
            {
                firstFanceObject = Instantiate(fanceObject, transform.position + new Vector3(0, 0.25f, 0), Quaternion.identity);
                firstFanceObject.AddComponent<LineRenderer>();
                ropeRender = firstFanceObject.GetComponent<LineRenderer>();

                SetUpLineRender(ropeRender);

                ropeRender.SetPosition(0, transform.position + new Vector3(0, 0.25f, 0));

                firstFance = false;
                isAnimatingColor = false;

                countOfFances--;

                pointsOfFances.Add(new List<Vector2>());
                pointsOfFances[pointsOfFances.Count - 1].Add(transform.position + new Vector3(0, 0.25f, 0));
            }
            else if (firstFanceObject != null)
            {
                Collider2D[] fancesAround = Physics2D.OverlapCircleAll(transform.position, interactRadius, fanceLayer);
                bool hasStartFance = false;

                foreach (Collider2D currencFance in fancesAround)
                {
                    if (currencFance.gameObject == firstFanceObject)
                    {
                        hasStartFance = true;
                        break;
                    }
                }

                if (hasStartFance && pointsOfFances[pointsOfFances.Count - 1].Count > 2)
                {
                    ropeRender.SetPosition(1, firstFanceObject.transform.position);

                    ropeRender.startColor = Color.white;
                    ropeRender.endColor = Color.white;

                    isAnimatingColor = false;

                    ropeRender = null;
                    firstFanceObject = null;
                    firstFance = true;

                    fanceManager.Check(pointsOfFances);
                }
                else if (countOfFances > 0)
                {
                    ropeRender.SetPosition(1, transform.position + new Vector3(0, 0.25f, 0));

                    ropeRender.startColor = Color.white;
                    ropeRender.endColor = Color.white;

                    isAnimatingColor = false;

                    GameObject newFance = Instantiate(fanceObject, transform.position + new Vector3(0, 0.25f, 0), Quaternion.identity);
                    newFance.AddComponent<LineRenderer>();
                    ropeRender = newFance.GetComponent<LineRenderer>();

                    SetUpLineRender(ropeRender);

                    ropeRender.SetPosition(0, transform.position + new Vector3(0, 0.25f, 0));

                    countOfFances--;

                    pointsOfFances[pointsOfFances.Count - 1].Add(transform.position + new Vector3(0, 0.25f, 0));
                }
            }
        }
    }

    IEnumerator ChangeColor(Color endColor)
    {
        isAnimatingColor = true;

        Color start = ropeRender.startColor;
        float elapsedTime = 0;

        while (elapsedTime < animationTime)
        {
            if (ropeRender == null) yield break;

            Color newColor = Color.Lerp(start, endColor, elapsedTime / animationTime);
            ropeRender.startColor = newColor;
            ropeRender.endColor = newColor;
            yield return null;
            elapsedTime += Time.deltaTime;
        }

        if (ropeRender == null) yield break;

        ropeRender.startColor = endColor;
        ropeRender.endColor = endColor;

        isAnimatingColor = false;
    }

    bool CanPlaceFance()
    {
        if (ropeRender == null) return true;

        bool tochThing = Physics2D.Linecast(ropeRender.GetPosition(0), ropeRender.GetPosition(1), badLayer);
        if (tochThing) return false;

        float currentDistance = 0;
        foreach (List<Vector2> path in pointsOfFances)
        {
            for (int i = 1; i< path.Count; i++)
            {
                currentDistance += Vector2.Distance(path[i - 1], path[i]);
            }
        }

        currentDistance += Vector2.Distance(ropeRender.GetPosition(0), ropeRender.GetPosition(1));

        return ropeLenght >= currentDistance;
    }

    void SetUpLineRender(LineRenderer lineRenderer)
    {
        lineRenderer.sortingOrder = 3;
        lineRenderer.material = ropeMaterial;
        lineRenderer.positionCount = 2;
        lineRenderer.textureMode = LineTextureMode.Static;
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}
