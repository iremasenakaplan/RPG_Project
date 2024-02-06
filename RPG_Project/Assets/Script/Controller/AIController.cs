using RPG.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.Movement;
using System;

namespace RPG.Controller
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 5f;
        [SerializeField] float wayPointTolerence = 1f;
        [SerializeField] PatrolPath patrolPath;

        GameObject player;
        Fighter fighter;
        Health health;
        Mover mover;

        Vector3 enemyLocation;
        float timeSinceLastSawPlayer;
        int currentWayPointIndex = 0;

        void Start()
        {
            player = GameObject.FindWithTag("Player");
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();

            enemyLocation = transform.position;
        }


        void Update()
        {
            if(health.IsDead()) // == true demek 
            {
                return;
            }

            if(DistanceToPlayer() < chaseDistance && fighter.CanAttack(player))
            {
                timeSinceLastSawPlayer = 0;
                fighter.Attack(player);
            }
            else if(timeSinceLastSawPlayer<suspicionTime)
            {
                GetComponent<ActionScheduler>().CancelCurrentAction();
            }
            else
            {
                Vector3 nextPosition = enemyLocation;
                if(patrolPath != null)
                {
                    if(AtWayPoint())
                    {
                        CycleWayPoint();
                    }

                    nextPosition = GetNextWayPoint();
                }
                mover.StartMoveAction(nextPosition);
            }
            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private Vector3 GetNextWayPoint()
        {
            return patrolPath.GetWayPointPosition(currentWayPointIndex);
        }

        private void CycleWayPoint()
        {
            currentWayPointIndex = patrolPath.GetNextIndex(currentWayPointIndex);
        }

        private bool AtWayPoint()
        {
            float distanceWayPoint = Vector3.Distance(transform.position, GetNextWayPoint());
            return distanceWayPoint < wayPointTolerence;
        }

        private float DistanceToPlayer()
        {
            return Vector3.Distance(player.transform.position, transform.position);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }

}
