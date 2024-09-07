using UnityEngine;

namespace DVUnityUtilities.Other.Particles
{
    public class ParticleFunctions : MonoBehaviour
    {
        [SerializeField] private bool _emitFromLoad = false;

        private void Awake()
        {
            if (!_emitFromLoad)
            {
                SetEmissionStatus(false);
            }
        }

        public void PlayParticlePrefabOnTransform(ParticleSystem prefab)
        {
            ParticleUtils.PlayParticleFromPrefab(prefab, transform.position, true);
        }

        public void PlayParticlePrefabOnTransformAsParent(ParticleSystem prefab)
        {
            var ps = ParticleUtils.PlayParticleFromPrefab(prefab, transform.position, true);
            ps.transform.SetParent(transform, true);
        }

        public void SetEmissionStatus(bool isEmit)
        {
            var particles = transform.GetComponentsInChildren<ParticleSystem>(true);
            foreach (var p in particles)
            {
                var emi = p.emission;
                emi.enabled = isEmit;
            }
        }
    }
}