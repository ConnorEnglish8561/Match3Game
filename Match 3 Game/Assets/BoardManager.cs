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
            CreateBoard(offset.x, offset.y);
        }
    }

    private void CreateBoard(float xOffset, float yOffset)
    {
        tiles = new GameObject[xSize, ySize];

        float startX = transform.position.x;
        float startY = transform.position.y;


        // creates the tiles based on the size of the board
        for (int x = 0; x < xSize; x++)
        {    
            for (int y = 0; y < ySize; y++)
            {
                GameObject newTile = Instantiate(tile, new Vector3(startX + (xOffset * x), startY + (yOffset * y), 0), tile.transform.rotation);
                tiles[x, y] = newTile;

                // randomizes the tile from a sprite in the <characters> List;
                newTile.transform.parent = transform;
                Sprite newSprite = characters[Random.Range(0, characters.Count)];
                newTile.GetComponent<SpriteRenderer>().sprite = newSprite;
            }
        }
    }
}
