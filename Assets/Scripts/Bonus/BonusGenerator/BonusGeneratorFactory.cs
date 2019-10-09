public class BonusGeneratorFactory 
{
    public static BonusGeneratorBase Create(Game.GameSettings.BonusSpawn type, Bonus.Pool bonusPool)
    {
        switch (type)
        {
            case Game.GameSettings.BonusSpawn.Orderly:
                return new OrderlyBonusGenerator(bonusPool);

            case Game.GameSettings.BonusSpawn.Random:
                return new RandomBonusGenerator(bonusPool);
        }

        return null;
    }
}
