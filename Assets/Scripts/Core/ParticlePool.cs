using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Core
{
    public class ParticlePool : MonoBehaviour
    {
        [SerializeField] private ParticleSystem prefab;
        [SerializeField] private int initialSize = 5;

        private Queue<ParticleSystem> pool = new Queue<ParticleSystem>();

        private void Awake()
        {
            for (int i = 0; i < initialSize; i++)
            {
                CreateNew();
            }
        }

        private ParticleSystem CreateNew()
        {
            var ps = Instantiate(prefab, transform);
            ps.gameObject.SetActive(false);
            pool.Enqueue(ps);
            return ps;
        }

        public ParticleSystem Get()
        {
            if (pool.Count == 0)
                CreateNew();

            var ps = pool.Dequeue();
            ps.gameObject.SetActive(true);
            return ps;
        }

        public void Return(ParticleSystem ps)
        {
            ps.gameObject.SetActive(false);
            pool.Enqueue(ps);
        }

        public async UniTask PlayAsync(Vector3 position)
        {
            var ps = Get();
            ps.transform.position = position;
            ps.Play();

            await UniTask.WaitUntil(() => !ps.isPlaying);

            Return(ps);
        }
    }
}