using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace DVUnityUtilities
{
    public static class NavMeshUtils
    {
        public static bool GetNavMeshPosition(Vector3 position, int areaMask, float maxDistance, out Vector3 navMeshPosition)
        {
            NavMeshHit hit;

            // Sample points around the input position to find the nearest valid NavMesh point
            if (NavMesh.SamplePosition(position, out hit, maxDistance, areaMask))
            {
                navMeshPosition = hit.position;
                return true;
            }

            navMeshPosition = Vector3.zero; // Or some default value
            return false;
        }

        public static bool RandomPositionInRadius(Vector3 position, float radius, int areaMask, out NavMeshHit hit)
        {
            Vector3 randomPoint = position + Random.insideUnitSphere * Random.Range(0, radius); 

            return NavMesh.SamplePosition(randomPoint, out hit, radius, areaMask);
        }
    }
}