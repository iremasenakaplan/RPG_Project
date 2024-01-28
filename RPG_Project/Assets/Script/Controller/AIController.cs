using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Controller
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        GameObject player;

        void Start()
        {
            player = GameObject.FindWithTag("Player");
        }


        void Update()
        {
            if(DistanceToPlayer() < chaseDistance)
            {
                print(player.name + "Takip edilmeli");
            }
        }

        private float DistanceToPlayer()
        {
            return Vector3.Distance(player.transform.position, transform.position);
        }
    }

}
