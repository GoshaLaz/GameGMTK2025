using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityRnd : MonoBehaviour
{
    [Header("SFX")]
    [SerializeField] private GameObject SFXObject;
    [Space(5)]
    [SerializeField] private float radiusOfUse;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float minTimeBtwSFX;
    [SerializeField] private float maxTimeBtwSFX;

    [Header("Animation")]
    [SerializeField] private float minTimeBtwAnimation;
    [SerializeField] private float maxTimeBtwAnimation;

    Animator anim;
    float currentTimeBtwAnimation;
    float currentTimeToWaitAnimation;
    float currentTimeBtwSFX;
    float currentTimeToWaitSFX;

    void Start()
    {
        anim = GetComponent<Animator>();
        currentTimeToWaitAnimation = Random.Range(minTimeBtwAnimation, maxTimeBtwAnimation);
        currentTimeToWaitSFX = Random.Range(minTimeBtwSFX, maxTimeBtwSFX);
    }

    void Update()
    {
        if (currentTimeBtwAnimation >= currentTimeToWaitAnimation)
        {
            currentTimeBtwAnimation = 0;
            currentTimeToWaitAnimation = Random.Range(minTimeBtwAnimation, maxTimeBtwAnimation);

            anim.SetTrigger("Rnd");
        }
        else currentTimeBtwAnimation += Time.deltaTime;


        Collider2D collider2D = Physics2D.OverlapCircle(transform.position, radiusOfUse, playerLayer);

        if (collider2D)
        {
            if (currentTimeBtwSFX >= currentTimeToWaitSFX)
            {
                currentTimeBtwSFX = 0;
                currentTimeToWaitSFX = Random.Range(minTimeBtwSFX, maxTimeBtwSFX);

                GameObject sound = Instantiate(SFXObject, Vector3.zero, Quaternion.identity);
                Destroy(sound, 10);
            }
            else currentTimeBtwSFX += Time.deltaTime;
        }
    }
}
