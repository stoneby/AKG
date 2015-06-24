using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
	public TextAsset TileText;

    public List<string> TileList;

    public List<Sprite> TileUnitSpriteList;

    private readonly List<GameObject> tileGoList = new List<GameObject>();
    private GameObject lastSpriteGo;
    private Sprite lastSprite;

	private Transform tileRootTrans;
    private const float TuneOffset = -0.01f;

    [ContextMenu("Execute")]
    public void Generate()
    {
		Initialize();

        foreach (var tileName in TileList)
        {
            var sprite = TileUnitSpriteList.Find(tile => tile.name.Equals(tileName));
            if (sprite != null)
            {
                var go = new GameObject {name = tileName};
                var render = go.AddComponent<SpriteRenderer>();
                render.sprite = sprite;
				render.sortingLayerName = "Background";

                if (lastSpriteGo == null)
                {
                    go.transform.position = transform.position;
                }
                else
                {
                    go.transform.position = lastSpriteGo.transform.position + new Vector3(lastSprite.bounds.size.x + TuneOffset, 0);
                }
                lastSpriteGo = go;
                lastSprite = sprite;
                go.transform.parent = tileRootTrans;
				go.layer = LayerMask.NameToLayer("Ground");

                tileGoList.Add(go);
            }
            else
            {
                Debug.LogWarning("Tile with name: " + tileName + " does not exist from TileUnitSpriteList, please double check it out.");
            }
        }
    }

    [ContextMenu("Cleanup")]
    public void Cleanup()
    {
        tileGoList.ForEach(DestroyImmediate);
        tileGoList.Clear();
    }

	private void Initialize()
	{
		if (tileRootTrans == null)
		{
			tileRootTrans = transform.Find("Tile");
		}

		var tiles = TileText.text.Trim().Split(new [] {' '}, System.StringSplitOptions.RemoveEmptyEntries);
		TileList.Clear();
		TileList.AddRange(tiles);
	}

    void Awake()
    {
        
    }
}
