using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MinigamePuzzle : MonoBehaviour
{
    [SerializeField] private Transform gameTransform;
    [SerializeField] private Transform piecePrefab;

    private List<Transform> pieces;
    private int emptyLocation;
    private int size;
    [SerializeField]private bool isShuffling = false;
    public float waitTime = 0f;
    [Header("Game Event")]
    public GameEvent puzzleStart;
    public GameEvent puzzleCompletedEvent;
    public GameEvent goToMainGameEvent;

    private void Start()
    {
        pieces = new List<Transform>();
        size = 3;
        CreateGamePiece(0.01f);
        if (!isShuffling)
        {
            isShuffling = true;
            StartCoroutine(WaitShuffle(waitTime));
        }


    }
    private void Update()
    {
       
    

        if (Input.GetMouseButton(0) && !isShuffling)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                for (int i = 0; i < pieces.Count; i++)
                {
                    if (pieces[i] == hit.transform)
                    {
                        if(SwapIfValid(i,-size,size)) { break; }
                        if(SwapIfValid(i, +size, size)) { break; }
                        if(SwapIfValid(i, -1 ,0)) { break; }
                        if(SwapIfValid(i, +1, size - 1 )) { break; }
                    }
                }
            }
        }


    }

    private bool CheckCompletion()
    {
        
        for(int i = 0; i < pieces.Count; i++)
        {
            if (pieces[i].name != $"{i}")
                return false;
            Debug.Log("Not Completed Yet");
            
        }
        puzzleCompletedEvent.Raise(this, 1);
        Debug.Log("Completed");
        return true;
 
    }

    IEnumerator WaitShuffle(float duration)
    {
        yield return new WaitForSeconds(duration);
        Shuffle();
        puzzleStart.Raise(this,true);
        isShuffling = false;
    }

    private void Shuffle()
    {
        int count = 0;
        int last = 0;

        while (count < (size * size * size))
        {
            //
            int rnd = UnityEngine.Random.Range(0, size * size);

            if (rnd == last) { continue; }
            last = emptyLocation;
            if (SwapIfValid(rnd, -size, size)) { count++; }
            else if (SwapIfValid(rnd, +size, size)) { count++; }
            else if(SwapIfValid(rnd, -1, 0)) { count++; }
            else if(SwapIfValid(rnd, +1, size - 1)) {  count++; }
        }
    }

    private void CreateGamePiece(float gapthickness)
    {
        //Width of each tile
        float width =1 /(float)size;
        for (int row = 0; row < size ; row++)
        {
            for (int col = 0; col < size; col++)
            {
                Transform piece = Instantiate(piecePrefab, gameTransform);
                pieces.Add(piece);
                piece.localPosition = new Vector3(-1 + (2 * width * col) + width, +1 - (2 * width * row) - width, 0);

                piece.localScale = ((2 * width) - gapthickness) * Vector3.one;
                piece.name = $"{(row * size) + col}";

                if ((row == size - 1) && (col == size - 1))
                {
                    emptyLocation = (size * size) - 1;
                    piece.gameObject.SetActive(false);
                }
                else
                {
                    float gap = gapthickness / 2;
                    Mesh mesh = piece.GetComponent<MeshFilter>().mesh;
                    Vector2[] uv = new Vector2[4];
                    //UV coord order: (0,1), (1,1) , (0,0), (1,0)
                    uv[0] = new Vector2((width * col) + gap, 1 - ((width * (row + 1)) - gap));
                    uv[1] = new Vector2((width * (col +1)) - gap , 1 - ((width * (row +1)) - gap));
                    uv[2] = new Vector2((width * col) + gap, 1 - ((width * row) + gap));
                    uv[3] = new Vector2((width * (col+1)) - gap, 1 - ((width * row )  + gap));
                    mesh.uv = uv;

                }
            }
        }
    }

    private bool SwapIfValid(int i, int offset,int colCheck)
    {
        if(((i%size ) != colCheck) && ((i + offset) == emptyLocation))
        {
            //Swap them in game state;
            (pieces[i], pieces[i + offset]) = (pieces[i + offset], pieces[i]);
            //Swap their transforms;
            (pieces[i].localPosition, pieces[i + offset].localPosition) = ((pieces[i + offset].localPosition, pieces[i].localPosition));
            //Update empty location
            emptyLocation = i;

            CheckCompletion();  

            return true;

        }
        return false;
    }

    public void GoToMainGame()
    {
        goToMainGameEvent.Raise(this, "MainGame");
    }



}
