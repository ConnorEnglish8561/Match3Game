using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class Tile : MonoBehaviour
{
    private static Color selectedColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
    private static Tile previousSelected = null;

    private SpriteRenderer render;
    private bool isSelected = false;

    private Vector2[] adjacentDirections = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

    private bool matchFound = false;

    private AudioSource audioSource;

    public ParticleSystem scoreParticle;
    private ParticleSystem updatedScoreParticle;

    void Awake()
    {
        render = GetComponent<SpriteRenderer>();

    }

    private void Select()
    {
        isSelected = true;
        render.color = selectedColor;
        previousSelected = gameObject.GetComponent<Tile>();
    }

    private void Deselect()
    {
        isSelected = false;
        render.color = Color.white;
        previousSelected = null;
    }

    void OnMouseDown()
    {

        if (render.sprite == null || BoardManager.instance.IsShifting)
        {
            return;
        }

        if (isSelected)
        { // Is it already selected?
            Deselect();
        }
        else
        {
            if (previousSelected == null)
            { // Is it the first tile selected?
                Select();
            }
            else
            {
                if (GetAllAdjacentTiles().Contains(previousSelected.gameObject))
                {
                    SwapSprite(previousSelected.render);
                    previousSelected.ClearAllMatches();
                    previousSelected.Deselect();
                    ClearAllMatches();
                }
                else
                {
                    previousSelected.GetComponent<Tile>().Deselect();
                    Select();
                }
            }
        }
    }

    public void SwapSprite(SpriteRenderer render2)
    {
        if (render.sprite == render2.sprite) // If the sprites are the same, do nothing
        {
            return;
        }

        Sprite tempSprite = render2.sprite;
        render2.sprite = render.sprite;
        render.sprite = tempSprite;
    }

    private GameObject GetAdjacent(Vector2 castDir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, castDir);
        if (hit.collider != null)
        {
            return hit.collider.gameObject;
        }
        return null;
    }


    private List<GameObject> GetAllAdjacentTiles()
    {
        List<GameObject> adjacentTiles = new List<GameObject>();
        for (int i = 0; i < adjacentDirections.Length; i++)
        {
            adjacentTiles.Add(GetAdjacent(adjacentDirections[i]));
        }
        return adjacentTiles;
    }

    private List<GameObject> FindMatch(Vector2 castDir)
    {

        List<GameObject> matchingTiles = new List<GameObject>();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, castDir);
        while (hit.collider != null && hit.collider.GetComponent<SpriteRenderer>().sprite == render.sprite)
        {
            matchingTiles.Add(hit.collider.gameObject);
            hit = Physics2D.Raycast(hit.collider.transform.position, castDir);

        }

        return matchingTiles;
    }

    private void ClearMatch(Vector2[] paths)
    {

        List<GameObject> matchingTiles = new List<GameObject>();
        for (int i = 0; i < paths.Length; i++)
        {
            matchingTiles.AddRange(FindMatch(paths[i]));


        }
        if (matchingTiles.Count >= 2)
        {
            float newScore = (matchingTiles.Count + 1) * Stats.instance.multiplier;
            Stats.instance.score += (matchingTiles.Count + 1) * Stats.instance.multiplier;
            GameObject[] scoreParticleAMT = GameObject.FindGameObjectsWithTag("ScoreParticle");
            for (int i = 0; i < scoreParticleAMT.Length; i++)
            {
                Destroy(scoreParticleAMT[i]);
            }
            updatedScoreParticle = Instantiate(scoreParticle, new Vector3(50f, 18f, -2.6f), new Quaternion(-90f, 0, 0, 100));
            updatedScoreParticle.GetComponent<updateParticleScore>().updateText("+" + ((matchingTiles.Count + 1) * Stats.instance.multiplier).ToString("F1"));
            Stats.instance.comboTime += (matchingTiles.Count + 1) / 0.5f;
            Stats.instance.multiplier += (matchingTiles.Count + 1) * 0.1f;

            for (int i = 0; i < matchingTiles.Count; i++)
            {
                matchingTiles[i].GetComponent<SpriteRenderer>().sprite = null;
            }
            audioSource = GetComponent<AudioSource>();
            audioSource.Play();
            matchFound = true;

        }

    }

    public void ClearAllMatches()
    {
        if (render.sprite == null)
         return;

        ClearMatch(new Vector2[2] { Vector2.left, Vector2.right });
        ClearMatch(new Vector2[2] { Vector2.up, Vector2.down });
        if (matchFound)
        {
            render.sprite = null;
            matchFound = false;
            StopCoroutine(BoardManager.instance.FindNullTiles());
            StartCoroutine(BoardManager.instance.FindNullTiles());
            Stats.instance.matches++;
        }
    }
}