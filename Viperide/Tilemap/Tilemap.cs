using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

public class Tilemap
{
    public Vector2 position = Vector2.Zero;
    public int tileSize { get; private set; }
    public int columns { get; private set; }
    public int rows { get; private set; }
    private Dictionary<string, TilemapLayer> layers = new Dictionary<string, TilemapLayer>();
    private List<string> layersOrder = new List<string>();
    private IAssetsManager assets = Services.Get<IAssetsManager>();

    public Tilemap(int columns = 10, int rows = 10, int tilesize=64)
    {
        this.columns = columns;
        this.rows = rows;
        this.tileSize = tilesize;
    }

    public void Draw()
    {
        foreach (var layerName in layersOrder)
        {
            var layer = layers[layerName];
            for (int column = 0; column < columns; column++)
            {
                for (int row = 0; row < rows; row++)
                {
                    Tile tile = layer.tiles[column, row];
                    if (tile.textureId >= 0 )
                    {
                        Texture2D texture = assets.GetTextureFromSet(layer.textureSetName, tile.textureId);
                        Raylib.DrawTexture(texture, (int)position.X + column*tileSize,(int)position.Y + row*tileSize, layer.tint);
                    }
                }
            }
        }
    }

    public void AutoTiling(string layerName, Coordinates[] neighborhood)
    {
        TilemapLayer layer = layers[layerName];

        for(int column = 0; column < columns;column++)
        {
            for(int row = 0; row < rows; row++)
            {
                if (layer.tiles[column, row].isSolid)
                {
                    layer.tiles[column, row].textureId = ComputeTileBitmask(layer, new Coordinates(column, row), neighborhood); // Texture vide
                }
                else
                {
                    layer.tiles[column, row].textureId = -1; // Texture vide
                }
            }
        }
    }

    private int ComputeTileBitmask(TilemapLayer layer, Coordinates coordinates, Coordinates[] neighborhood)
    {
        int mask = 0;
        
        for(int i = 0; i<neighborhood.Length;i++)
        {
            Coordinates direction = neighborhood[i];
            Coordinates neighbor = coordinates + direction;
            if (!IsInBounds(neighbor) || layer.tiles[neighbor.column, neighbor.row].isSolid)
                mask += 1 << i;
        }

        return mask;
    }

    public void AddLayer(string textureSetName, string name, Color tint)
    {
        TilemapLayer layer = new TilemapLayer
        {
            textureSetName = textureSetName,
            tint = tint,
            tiles = new Tile[columns, rows]
        };
        layers.Add(name, layer);
        layersOrder.Add(name);
    }

    public void SetTile(Coordinates coordinates, string layerName, int textureId)
    {
        layers[layerName].tiles[coordinates.column, coordinates.row] = new Tile
        {
            textureId = textureId
        };
    }
    public void SetTile(Coordinates coordinates, string layerName, bool isSolid)
    {
        layers[layerName].tiles[coordinates.column, coordinates.row] = new Tile
        {
            isSolid = isSolid
        };
    }
        
    public void SetTile(Coordinates coordinates, int textureId, string layerName)
    {
        if (!layers.ContainsKey(layerName)) return;
        if (layersOrder.IndexOf(layerName) == -1) return;

        if (!IsInBounds(coordinates)) return;
        layers[layerName].tiles[coordinates.column, coordinates.row] = new Tile
        {
            textureId = textureId,
            isSolid = false,
        };
    }

    public bool IsInBounds(Coordinates coordinates)
    {
        return coordinates.column < columns && coordinates.row < rows && coordinates.column >= 0 && coordinates.row >=0;
    }

    public bool IsSolid(Coordinates coordinates, string layerName)
    {
        return layers[layerName].tiles[coordinates.column, coordinates.row].isSolid;
    }

    public bool IsSolid(Coordinates coordinates)
    {
        foreach(var layerName in layersOrder)
        {
            if (IsSolid(coordinates,layerName)) return true;
        }
        return false;
    }

    public Coordinates WorldToMap(Vector2 pos)
    {
        pos -= position;
        pos /= tileSize;
        return new Coordinates((int)pos.X, (int)pos.Y);
    }

    public Vector2 MapToWorld(Coordinates coordinates)
    {
        coordinates *= tileSize;
        return coordinates.ToVector2 + position;
    }

    private struct Tile
    {
        public int textureId;
        public bool isSolid;
    }

    private struct TilemapLayer
    {
        public Tile[,] tiles;
        public string textureSetName;
        public Color tint;
    }
}

