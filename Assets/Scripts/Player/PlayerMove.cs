using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove: MonoBehaviour
{
    [SerializeField] public float moveTime = 0.2f;
    [SerializeField] public LayerMask collisionLayer; 
    [HideInInspector] public bool isMoving = false;
    [SerializeField] public float step;
    [Space(5)]
    [SerializeField] private GameObject playerSprite;

    Vector2 input;
    bool isFacingRight = true;

    void Update()
    {
        if (!isMoving)
        {
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (Mathf.Abs(input.x) > 0)
                input.y = 0;

            if (input != Vector2.zero)
            {
                if (Mathf.Abs(input.x) > 0) isFacingRight = input.x > 0;

                Vector2 targetPos = (Vector2)transform.position + (input*step);

                if (!Physics2D.Linecast(transform.position, targetPos, collisionLayer))
                {
                    StartCoroutine(Move(targetPos));
                }
            }
        }
    }

    IEnumerator Move(Vector2 target)
    {
        isMoving = true;

        Vector2 startPos = transform.position;
        Quaternion startRotation = playerSprite.transform.rotation;
        Quaternion endRotation = (isFacingRight) ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0,180,0);

        float elapsedTime = 0;

        while (elapsedTime < moveTime)
        {
            transform.position = Vector2.Lerp(startPos, target, elapsedTime / moveTime);
            playerSprite.transform.rotation = Quaternion.Lerp(startRotation, endRotation, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = target;
        isMoving = false;
    }
}
