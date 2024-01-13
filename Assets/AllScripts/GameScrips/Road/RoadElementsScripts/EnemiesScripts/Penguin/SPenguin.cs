using System;
using System.Collections;
using UnityEngine;

public class SPenguin : MonoBehaviour
{
    private Animator animator;
    private SPlayerMovement playerMovement;
    private Vector3 playerPosition;
    private const int offset = 20;

    [SerializeField] private GameObject snowBallPrefab;
    [SerializeField] private ParticleSystem snowBallEffect;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = FindObjectOfType<SPlayerMovement>();
        StartCoroutine(MakeThrowCoroutine());
    }

    private IEnumerator MakeThrowCoroutine()
    {
        const float delay = 1f;
        while (true)
        {
            yield return new WaitForSeconds(delay);
            playerPosition = playerMovement.transform.position;
            MakeThrow();
        }
    }

    private void MakeThrow()
    {
        if (playerPosition.x > transform.position.x - offset)
        {
            Vector3 newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            GameObject snowBall = Instantiate(snowBallPrefab, newPos, Quaternion.identity, transform);
            SSnowBallCollider snowBallColl = snowBall.GetComponent<SSnowBallCollider>();
            ChekSnowBallColl(snowBall, snowBallColl);
            StartCoroutine(SnowBallMovement(snowBall));
            animator.SetTrigger("isThrow");
        }
    }

    void ChekSnowBallColl(GameObject snowBall, SSnowBallCollider snowBallColl)
    {
        Action onCollidedAction = null;
        onCollidedAction = () =>
        {
            Instantiate(snowBallEffect, snowBall.transform.position, Quaternion.identity);
            Destroy(snowBall);
            snowBallColl.onCollided -= onCollidedAction;
        };

        snowBallColl.onCollided += onCollidedAction;
    }

    private IEnumerator SnowBallMovement(GameObject snowBall)
    {
        const int speed = 5;
        while (snowBall != null && snowBall.transform.position.x > playerPosition.x)
        {
            snowBall.transform.Translate(Vector3.left * speed * Time.deltaTime);
            yield return null;
        }
        if (snowBall != null)
            Destroy(snowBall);
    }
}
