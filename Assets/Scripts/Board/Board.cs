using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Board : MonoBehaviour, IRestartable
{
    public const int BlockSize = 5;

    const int MapSize = 20;
    const int StartSize = 3;
    const int ScrollPosition = 3;

    public float TileSize = 1f;

    [Inject] Game game;
    [Inject] Tile.Pool tilePool;
    [Inject] Bonus.Pool bonusPool;

    Tile[,] map;

    int mapOffsetX;
    int mapOffsetY;

    private BonusGeneratorBase bonusGenerator;

    int pathX;
    int pathY;
    int pathWidth = 1;
    int pathHalfWidth;
    int tileIndex;

    private void Start()
    {
        map = new Tile[MapSize, MapSize];
        Restart();
    }

    public void Restart()
    {
        mapOffsetX = 0;
        mapOffsetY = 0;
        tileIndex = 0;
        pathHalfWidth = pathWidth / 2;
        Array.Clear(map, 0, MapSize * MapSize);
        bonusGenerator = BonusGeneratorFactory.Create(game.Settings.bonusSpawn, bonusPool);
        tilePool.Clean();
        bonusPool.Clean();
        InitStartPlane();
        InitPathWidth();
        InitPath();
    }

    void InitStartPlane()
    {
        for (int x = 0; x < StartSize; x++)
        {
            for (int y = 0; y < StartSize; y++)
            {
                AddTile(x, y);
            }
        }
    }

    void InitPathWidth()
    {
        switch (game.Settings.difficult)
        {
            case Game.GameSettings.Difficult.Easy:
                pathWidth = 3;
                break;
            case Game.GameSettings.Difficult.Medium:
                pathWidth = 2;
                break;
            case Game.GameSettings.Difficult.Hard:
                pathWidth = 1;
                break;
        }
    }

    void InitPath()
    {
        pathX = 1;
        pathY = 1;
        BuildPath();
    }

    void BuildPath()
    {
        while (pathX < MapSize + mapOffsetX && pathY < MapSize + mapOffsetY)
        {
            AddPathTiles(pathX, pathY);

            Tile tile = map[pathX - mapOffsetX, pathY - mapOffsetY];

            bonusGenerator.CheckToGenerate(tileIndex / BlockSize, tileIndex % BlockSize, new Vector3(pathX * TileSize, pathY * TileSize, -0.625f), tile.transform);

            tileIndex++;

            if (UnityEngine.Random.Range(0, 2) == 0)
                pathX++;
            else
                pathY++;
        }
    }

    void AddPathTiles(int cx, int cy)
    {
        for (int x = 0; x < pathWidth; x++)
        {
            for (int y = 0; y < pathWidth; y++)
            {
                AddTile(cx + x - pathHalfWidth, cy + y - pathHalfWidth);
            }
        }
    }

    void AddTile(int x, int y)
    {
        AddTileLocal(x - mapOffsetX, y - mapOffsetY);
    }

    void AddTileLocal(int x, int y)
    {
        if (map[x, y] != null)
            return;

        Tile tile = tilePool.Spawn();
        tile.transform.parent = transform;
        tile.transform.localPosition = new Vector3((x + mapOffsetX) * TileSize, (y + mapOffsetY) * TileSize);
        map[x, y] = tile;
    }

    void RemoveTile(int x, int y)
    {
        RemoveTileLocal(x - mapOffsetX, y - mapOffsetY);
    }

    void RemoveTileLocal(int x, int y)
    {
        Tile tile = map[x, y];
        tile.Fall();
        map[x, y] = null;
    }

    public Tile GetTile(Vector3 position)
    {
        int x = Mathf.RoundToInt(position.x / TileSize) - mapOffsetX;
        int y = Mathf.RoundToInt(position.y / TileSize) - mapOffsetY;

        if (x >= 0 && x < MapSize && y >= 0 && y < MapSize)
            return map[x, y];

        return null;
    }

    public void CheckToScroll(Vector3 position)
    {
        int x = Mathf.RoundToInt(position.x / TileSize) - mapOffsetX;
        int y = Mathf.RoundToInt(position.y / TileSize) - mapOffsetY;

        if (x >= ScrollPosition)
            ScrollMapX();

        if (y >= ScrollPosition)
            ScrollMapY();
    }

    void ScrollMapX()
    {
        for (int x = 0; x < MapSize; x++)
        {
            for (int y = 0; y < MapSize; y++)
            {
                if (x == 0 && map[x, y] != null)
                    RemoveTileLocal(x, y);

                int nx = x + 1;
                int ny = y;

                if (nx < MapSize)
                    map[x, y] = map[nx, ny];
                else
                    map[x, y] = null;
            }
        }

        mapOffsetX++;

        BuildPath();
    }

    void ScrollMapY()
    {
        for (int x = 0; x < MapSize; x++)
        {
            for (int y = 0; y < MapSize; y++)
            {
                if (y == 0 && map[x, y] != null)
                    RemoveTileLocal(x, y);

                int nx = x;
                int ny = y + 1;

                if (ny < MapSize)
                    map[x, y] = map[nx, ny];
                else
                    map[x, y] = null;
            }
        }

        mapOffsetY++;

        BuildPath();
    }

}
