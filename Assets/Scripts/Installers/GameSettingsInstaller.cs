using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    public Game.GameSettings Game;
    public Player.PlayerSettings Player;

    public override void InstallBindings()
    {
        Container.BindInstance(Game);
        Container.BindInstance(Player);
    }
}
