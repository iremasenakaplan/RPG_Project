using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Controller
{
    public class PatrolPath : MonoBehaviour
    {
        const float wayPointRadius = 0.4f;
        private void OnDrawGizmos()
        {
            for(int i = 0; i< transform.childCount; i++)
            {
                int j = GetNextIndex(i);
                Gizmos.DrawSphere(GetWayPointPosition(i), wayPointRadius);
                Gizmos.DrawLine(GetWayPointPosition(i), GetWayPointPosition(j));
            }


        }

        private int GetNextIndex(int i)
        {
            if(i + 1 == transform.childCount)
            {
                return 0;
            }
            return i + 1;
        }

        private Vector3 GetWayPointPosition(int i)
        {
            return transform.GetChild(i).position;
        }
    }
}

