using UnityEngine;
using Zenject;

public class OrderlyBonusGenerator : BonusGeneratorBase
{
    public OrderlyBonusGenerator(Bonus.Pool bonusPool) : base(bonusPool)
    {
    }

    public override void CheckToGenerate(int blockIndex, int tileIndex, Vector3 position, Transform transform)
    {
        int targetTileIndex = blockIndex % Board.BlockSize;

        if (tileIndex == targetTileIndex)
        {
            Generate(position, transform);
        }
    }
}
