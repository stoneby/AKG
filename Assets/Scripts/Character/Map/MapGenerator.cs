using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public List<string> TileList;

    public Texture2D Texture;

    private List<Sprite> tileSpriteList = new List<Sprite>();

    [ContextMenu("Execute")]
    public void Generate()
    {
        foreach (var tileName in TileList)
        {
            var sprite = new Sprite();
            //sprite.texture = Texture;
            sprite.name = tileName;

            tileSpriteList.Add(sprite);
        }
    }

    [ContextMenu("Cleanup")]
    public void Cleanup()
    {
        tileSpriteList.ForEach(DestroyImmediate);
        tileSpriteList.Clear();
    }

    void Awake()
    {
        
    }
}
