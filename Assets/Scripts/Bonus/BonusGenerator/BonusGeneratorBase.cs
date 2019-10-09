using UnityEngine;
using Zenject;

public class BonusGeneratorBase
{
    protected Bonus.Pool bonusPool;

    public BonusGeneratorBase(Bonus.Pool bonusPool)
    {
        this.bonusPool = bonusPool;
    }

    public virtual void CheckToGenerate(int blockIndex, int tileIndex, Vector3 position, Transform transform)
    {
    }

    protected virtual void Generate(Vector3 position, Transform transform)
    {
        Bonus bonus = bonusPool.Spawn();
        bonus.transform.position = position;
        bonus.transform.parent = transform;
    }
    
}
