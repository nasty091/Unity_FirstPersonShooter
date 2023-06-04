using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    //public float moveSpeed;
    //public Rigidbody theRB;

    private bool chasing;
    public float distanceToChase = 10f, distanceToLose = 15f, distanceToStop = 2f;

    private Vector3 targetPoint;

    public NavMeshAgent agent;
    private Vector3 startPosition;

    public float keepChasingTime = 5f;
    private float chaseCounter;

    public GameObject bullet;
    public Transform firePoint;

    public float fireRate, waitBetweenShots = 2f, timeToShoot = 1f;
    private float fireCount, shotWaitCounter, shootTimeCounter;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        targetPoint = PlayerController.instance.transform.position;
        targetPoint.y = transform.position.y;

        if (!chasing)
        {
            if (Vector3.Distance(transform.position, targetPoint) < distanceToChase)
            {
                chasing = true;

                shootTimeCounter = timeToShoot;
                shotWaitCounter = waitBetweenShots;
            }

            if(chaseCounter > 0)
            {
                chaseCounter -= Time.deltaTime;
                if (chaseCounter <= 0)
                {
                    agent.destination = startPosition;
                }
            }
        }
        else
        {
            //transform.LookAt(targetPoint);
            //theRB.velocity = transform.forward * moveSpeed;

            //Distance to stop in front of the player
            if (Vector3.Distance(transform.position, targetPoint) > 2)
            {
                //agent.SetDestination(targetPoint);
                agent.destination = targetPoint;
            }
            else
            {
                agent.destination = transform.position;
            }

            //Distance to lose the player
            if(Vector3.Distance(transform.position, targetPoint) > distanceToLose)
            {
                chasing = false;

                chaseCounter = keepChasingTime;
            }

            //Time to shoot and Time to wait between shots
            if (shotWaitCounter > 0)
            {
                shotWaitCounter -= Time.deltaTime;

                if(shotWaitCounter <= 0)
                {
                    shootTimeCounter = timeToShoot;
                }
            }
            else
            {
                shootTimeCounter -= Time.deltaTime;
                if (shootTimeCounter > 0)
                {
                    fireCount -= Time.deltaTime;
                    if (fireCount <= 0)
                    {
                        fireCount = fireRate;

                        firePoint.LookAt(PlayerController.instance.transform.position + new Vector3(0f, 1.5f, 0f));

                        //Check the angle to the player
                        Vector3 targetDir = PlayerController.instance.transform.position - transform.position;
                        float angle = Vector3.SignedAngle(targetDir, transform.forward, Vector3.up);
                        Debug.Log(angle);
                        if(Mathf.Abs(angle) < 30)
                        {
                            Instantiate(bullet, firePoint.position, firePoint.rotation);
                        }
                        else
                        {
                            shotWaitCounter = waitBetweenShots;
                        }
                    }
                    agent.destination = transform.position;
                }
                else
                {
                    shotWaitCounter = waitBetweenShots;
                }
            }

        }
    }
}
