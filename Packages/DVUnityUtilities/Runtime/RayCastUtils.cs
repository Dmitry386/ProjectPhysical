using UnityEngine;

namespace DVUnityUtilities
{
    public static class RayCastUtils
    {
        public static Texture2D GetRayHitTexture(RaycastHit hit)
        {
            var res = GetObjectHitTexture(hit);
            if (!res) res = GetTerrainHitTexture(hit);
            return res;
        }

        public static Texture2D GetTerrainHitTexture(RaycastHit hit)
        {
            Terrain terrain = hit.collider.GetComponent<Terrain>();
            if (terrain == null)
            {
                //Debug.LogError("The RaycastHit did not hit a terrain.");
                return null;
            }

            TerrainData terrainData = terrain.terrainData;
            Vector3 terrainPos = terrain.transform.position;

            // Calculate the hit position relative to the terrain
            Vector3 hitPos = hit.point - terrainPos;

            // Convert hit position to a normalized coordinate
            float normalizedX = hitPos.x / terrainData.size.x;
            float normalizedZ = hitPos.z / terrainData.size.z;

            // Convert normalized coordinate to the splatmap coordinate
            int mapX = Mathf.RoundToInt(normalizedX * terrainData.alphamapWidth);
            int mapZ = Mathf.RoundToInt(normalizedZ * terrainData.alphamapHeight);

            // Get the splatmap data
            float[,,] alphaMaps = terrainData.GetAlphamaps(mapX, mapZ, 1, 1);

            // Determine which texture is at the hit point by finding the highest value in the alpha maps
            int textureIndex = 0;
            float maxMix = 0f;
            for (int i = 0; i < alphaMaps.GetLength(2); i++)
            {
                if (alphaMaps[0, 0, i] > maxMix)
                {
                    maxMix = alphaMaps[0, 0, i];
                    textureIndex = i;
                }
            }

            return terrainData.terrainLayers[textureIndex].diffuseTexture;
        }

        public static Texture2D GetObjectHitTexture(RaycastHit hit)
        {
            var renderer = hit.collider.GetComponentInChildren<Renderer>();
            if (renderer)
            {
                if (renderer.material.HasTexture("_MainTex"))
                {
                    Texture2D texture2D = renderer.material.mainTexture as Texture2D;
                    return texture2D;
                }
            }

            return null;
        }
    }
}