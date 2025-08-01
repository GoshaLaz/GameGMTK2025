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
    [SerializeField] private Material ropeMaterial;
    [Space(5)]
    [SerializeField] private LayerMask badLayer;
    [SerializeField] private Color badColorRope;
    [SerializeField] private float animationTime;

    List<List<Vector2>> pointsOfFances = new List<List<Vector2>>();

    PlayerMove playerMovementScript;
    bool firstFance;
    LineRenderer ropeRender;
    GameObject firstFanceObject;

    bool canPlaceFance;
    bool isAnimatingColor = false;

    void Start()
    {
        playerMovementScript = GetComponent<PlayerMove>();
        firstFance = true;
    }

    void Update()
    {
        if (ropeRender != null)
        {
            ropeRender.SetPosition(1, transform.position);
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

                countOfFances--;

                pointsOfFances.Add(new List<Vector2>());
                pointsOfFances[pointsOfFances.Count].Add(transform.position + new Vector3(0, 0.25f, 0));
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

                if (hasStartFance)
                {
                    ropeRender.SetPosition(1, firstFanceObject.transform.position);

                    ropeRender = null;
                    firstFanceObject = null;
                    firstFance = true;
                }
                else if (countOfFances > 0)
                {
                    ropeRender.SetPosition(1, transform.position + new Vector3(0, 0.25f, 0));

                    GameObject newFance = Instantiate(fanceObject, transform.position + new Vector3(0, 0.25f, 0), Quaternion.identity);
                    newFance.AddComponent<LineRenderer>();
                    ropeRender = newFance.GetComponent<LineRenderer>();

                    SetUpLineRender(ropeRender);

                    ropeRender.SetPosition(0, transform.position + new Vector3(0, 0.25f, 0));

                    countOfFances--;

                    pointsOfFances[pointsOfFances.Count].Add(transform.position + new Vector3(0, 0.25f, 0));
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

        ropeRender.startColor = endColor;
        ropeRender.endColor = endColor;

        isAnimatingColor = false;
    }

    bool CanPlaceFance()
    {
        if (ropeRender == null) return true;

        return !Physics2D.Linecast(ropeRender.GetPosition(0), ropeRender.GetPosition(1), badLayer);
    }

    void SetUpLineRender(LineRenderer lineRenderer)
    {
        lineRenderer.material = ropeMaterial;
        lineRenderer.positionCount = 2;
        lineRenderer.textureMode = LineTextureMode.Static;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}
