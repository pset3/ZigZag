using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class Tile : MonoBehaviour
{
    private const float FallTime = 5f;

    [Inject] Tile.Pool tilePool;

    public enum StateType
    {
        Start,
        Fall
    }

    StateType state;

    public StateType State
    {
        get { return state; }
    }

    Rigidbody rigidbody;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void OnSpawned()
    {
        state = StateType.Start;
        rigidbody.isKinematic = true;
    }

    void OnDespawned()
    {
    }

    public void Fall()
    {
        state = StateType.Fall;
        rigidbody.isKinematic = false;
        StartCoroutine(FallCoroutine());
    }

    IEnumerator FallCoroutine()
    {
        yield return new WaitForSeconds(FallTime);
        Finish();
    }

    void Finish()
    {
        tilePool.Despawn(this);
    }

    public class Pool : MonoMemoryPool<Tile>
    {
        HashSet<Tile> tiles = new HashSet<Tile>();

        protected override void OnSpawned(Tile item)
        {
            base.OnSpawned(item);
            tiles.Add(item);
            item.OnSpawned();
        }

        protected override void OnDespawned(Tile item)
        {
            base.OnDespawned(item);
            tiles.Remove(item);
            item.OnDespawned();
        }

        public void Clean()
        {
            foreach (Tile tile in tiles.ToList())
            {
                Despawn(tile);
            }
        }
    }

}
