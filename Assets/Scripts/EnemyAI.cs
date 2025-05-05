using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public float movespeed;
    public float nextWaypointDistance;

    private SpriteRenderer characterSR;

    public Seeker seeker;
    public Transform target;
    Path path;

    Coroutine moveCoroutine;

    void CalculatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(transform.position, target.position, OnPathCallBack);
        }
    }

    void OnPathCallBack(Path p)
    {
        if (p.error)
        {
            return;
        }
        path = p;
    }
    void Start()
    {
        target = FindObjectOfType<Player>().gameObject.transform;
        InvokeRepeating("CalculatePath", 0f, 0.5f);
    }

    void MovetoTarget()
    {
        if(moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
        moveCoroutine = StartCoroutine(MoveToTargetCoroutine());
    }

    IEnumerator MoveToTargetCoroutine()
    {
        int currentWaypoint = 0;

        while (currentWaypoint < path.vectorPath.Count)
        {
            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - (Vector2)transform.position).normalized;
            Vector2 force = direction * movespeed * Time.deltaTime;
            transform.position = (Vector2)transform.position + force;

            float distance = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);
            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }

            if (force.x != 0)
            {
                if(force.x > 0)
                {
                    characterSR.transform.localScale = new Vector3(1, 1, 0);
                }
                else
                {
                    characterSR.transform.localScale = new Vector3(-1, 1, 0);
                }
            }

            yield return null;
        }
    }

    void Update()
    {
        
    }
}
