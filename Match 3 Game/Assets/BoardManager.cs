using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
 public static BoardManager instance;    
    public List<Sprite> characters = new List<Sprite>();    
    public GameObject tile;      
    public int xSize, ySize;

    private Vector2 offset;
    private GameObject[,] tiles;
    private GameObject[] totalTiles;

    public bool IsShifting { get; set; } 

    void Awake()
    {
        instance = GetComponent<BoardManager>(); 

        offset = tile.GetComponent<SpriteRenderer>().bounds.size + new Vector3(1, 1, 1);
        CreateBoard(offset.x, offset.y); 
    }

    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            NewBoard();
        }
    }

    public void NewBoard()
    {
        Stats.instance.Reset();
        foreach (GameObject tile in totalTiles)
        {
            Destroy(tile);
        }
        CreateBoard(offset.x, offset.y);
    }

    private void CreateBoard(float xOffset, float yOffset)
    {
        tiles = new GameObject[xSize, ySize];

        float startX = transform.position.x;
        float startY = transform.position.y;

        Sprite[] previousLeft = new Sprite[ySize];
        Sprite previousBelow = null;

        // creates the tiles based on the size of the board
        for (int x = 0; x < xSize; x++)
        {    
            for (int y = 0; y < ySize; y++)
            {
                GameObject newTile = Instantiate(tile, new Vector3(startX + (xOffset * x), startY + (yOffset * y), 0), tile.transform.rotation);
                tiles[x, y] = newTile;

                // randomizes the tile from a sprite in the <characters> List;
                newTile.transform.parent = transform;

                List<Sprite> possibleCharacters = new List<Sprite>(); // 1
                possibleCharacters.AddRange(characters); // 2

                possibleCharacters.Remove(previousLeft[y]); // 3
                possibleCharacters.Remove(previousBelow);

                Sprite newSprite = possibleCharacters[Random.Range(0, possibleCharacters.Count)];
                newTile.GetComponent<SpriteRenderer>().sprite = newSprite;
                previousLeft[y] = newSprite;
                previousBelow = newSprite;
            }
        }

        totalTiles = GameObject.FindGameObjectsWithTag("Tile");
    }

    public IEnumerator FindNullTiles()
    {
        for (int x = 0; x < xSize; x++)
        {
            for(int y = 0; y < ySize; y++)
            {
                if (tiles[x, y].GetComponent<SpriteRenderer>().sprite == null)
                {
                    yield return StartCoroutine(ShiftTilesDown(x, y));
                    break; 
                }
            }
        }
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                tiles[x, y].GetComponent<Tile>().ClearAllMatches();
            }
        }

    }

    private IEnumerator ShiftTilesDown(int x, int yStart, float shiftDelay = .03f)
    {
        IsShifting = true;
        List<SpriteRenderer> renders = new List<SpriteRenderer>();
        int nullCount = 0; 

        for (int y = yStart; y < ySize; y++)
        {
            SpriteRenderer render = tiles[x, y].GetComponent<SpriteRenderer>();
            if (render.sprite == null)
            {
                nullCount++;
            }
            renders.Add(render);
        }
        for (int i = 0; i < nullCount; i++)
        {
            yield return new WaitForSeconds(shiftDelay);
            for( int k = 0;  k < renders.Count - 1; k++ )
            {
                renders[k].sprite = renders[k + 1].sprite;
                renders[k + 1].sprite = GetNewSprite(x, ySize - 1);
            }
        }
        IsShifting = false;
    }

    private Sprite GetNewSprite(int x, int y) 
    {
        List<Sprite> possibleCharacters = new List<Sprite>();
        possibleCharacters.AddRange(characters); 

        if (x > 0)
        {
            possibleCharacters.Remove(tiles[x - 1, y].GetComponent<SpriteRenderer>().sprite);
        }
        if (x < xSize - 1)
        {
            possibleCharacters.Remove(tiles[x + 1, y].GetComponent<SpriteRenderer>().sprite);
        }
        if (y > 0)
        {
            possibleCharacters.Remove(tiles[x,y - 1].GetComponent<SpriteRenderer>().sprite);
        }

        return possibleCharacters[Random.Range(0, possibleCharacters.Count)];
    }
}
