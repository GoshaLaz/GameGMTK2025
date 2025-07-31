using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeAndFance : MonoBehaviour
{
    [Header("Fance")]
    [SerializeField] private int countOfFances;
    [Space(5)]
    [SerializeField] private GameObject fanceObject;
    [SerializeField] private float interactRadius;

    [Header("Rope")]
    [SerializeField] private Material ropeMaterial;

    PlayerMove playerMovementScript;
    bool firstFance;
    GameObject ropeObject;
    LineRenderer ropeRender;

    void Start()
    {
        playerMovementScript = GetComponent<PlayerMove>();
        firstFance = true;

        ropeObject = new GameObject();
        ropeObject.AddComponent<LineRenderer>();
        ropeRender = ropeObject.GetComponent<LineRenderer>();
        ropeRender.material = ropeMaterial;
        ropeRender.positionCount = 0;
        ropeRender.numCornerVertices = 2;
        ropeRender.textureMode = LineTextureMode.Static;
    }

    void Update()
    {
        if (ropeRender.positionCount > 0) ropeRender.SetPosition(1, transform.position);

        if (Input.GetKeyDown(KeyCode.E) && !playerMovementScript.isMoving )
        {
            if ()
            {

            }
            else if (countOfFances > 0)
            {
                if (firstFance)
                {
                    ropeRender.positionCount = 2;
                    ropeRender.SetPosition(0, transform.position + new Vector3(0, 0.25f, 0));
                }

                countOfFances--;
                firstFance = false;
                Instantiate(fanceObject, transform.position + new Vector3(0, 0.25f, 0), Quaternion.identity);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}
