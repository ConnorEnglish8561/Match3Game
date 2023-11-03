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
            foreach (GameObject tile in totalTiles)
            {
                Destroy(tile);
            }
            CreateBoard(offset.x, offset.y);
        }
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
}
