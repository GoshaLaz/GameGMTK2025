using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove: MonoBehaviour
{
    [SerializeField] public float moveTime = 0.2f;
    [SerializeField] public LayerMask collisionLayer; 
    [HideInInspector] public bool isMoving = false;
    private Vector2 input;
    [SerializeField] public float step;

    void Update()
    {
        if (!isMoving)
        {
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (Mathf.Abs(input.x) > 0)
                input.y = 0;

            if (input != Vector2.zero)
            {
                Vector2 targetPos = (Vector2)transform.position + (input*step);

                if (Physics2D.OverlapCircle(targetPos, 0.1f, collisionLayer) == null)
                {
                    StartCoroutine(Move(targetPos));
                }
            }
        }
    }

    IEnumerator Move(Vector2 target)
    {
        isMoving = true;

        Vector2 start = transform.position;
        float elapsedTime = 0;

        while (elapsedTime < moveTime)
        {
            transform.position = Vector2.Lerp(start, target, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = target;
        isMoving = false;
    }
}
