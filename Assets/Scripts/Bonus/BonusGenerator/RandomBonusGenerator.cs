using UnityEngine;
using Zenject;

public class RandomBonusGenerator : BonusGeneratorBase
{
    int targetTileIndex;
    int prevBlockIndex = -1;

    public RandomBonusGenerator(Bonus.Pool bonusPool) : base(bonusPool)
    {
    }

    public override void CheckToGenerate(int blockIndex, int tileIndex, Vector3 position, Transform transform)
    {
        if (blockIndex != prevBlockIndex)
        {
            targetTileIndex = Random.Range(0, Board.BlockSize);
            prevBlockIndex = blockIndex;
        }

        if (tileIndex == targetTileIndex)
        {
            Generate(position, transform);
        }
    }
}
