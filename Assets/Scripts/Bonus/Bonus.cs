using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class Bonus : MonoBehaviour
{
    [Inject] private Pool bonusPool;

    void Start()
    {
    }

    public void Despawn()
    {
        bonusPool.Despawn(this);
    }

    public class Pool : MonoMemoryPool<Bonus>
    {
        HashSet<Bonus> items = new HashSet<Bonus>();

        protected override void OnSpawned(Bonus item)
        {
            base.OnSpawned(item);
            items.Add(item);
        }

        protected override void OnDespawned(Bonus item)
        {
            base.OnDespawned(item);
            items.Remove(item);
        }

        public void Clean()
        {
            foreach (var item in items.ToList())
            {
                Despawn(item);
            }
        }
    }
}
