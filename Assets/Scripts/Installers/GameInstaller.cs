using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private Game game;
    [SerializeField] private Player player;
    [SerializeField] private Board board;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject bonusPrefab;

    public override void InstallBindings()
    {
        Container.BindInstance(game);
        Container.BindInstance(player);
        Container.BindInstance(board);
        Container.BindInstance(cameraController);

        Container.BindMemoryPool<Tile, Tile.Pool>()
            .FromComponentInNewPrefab(tilePrefab)
            .UnderTransformGroup("Board");

        Container.BindMemoryPool<Bonus, Bonus.Pool>()
            .FromComponentInNewPrefab(bonusPrefab)
            .UnderTransformGroup("Board");
    }
}
