using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour {
    private const int GridWidth = 10;
    private const int GridHeight = 20;

    private static Transform[,] _grid = new Transform[GridWidth, GridHeight];

    private GameObject _nextBlocks;
    private GameObject _prevBlocks;
    public float pointDeleteLine;
    public Text scoresPoint;
    
    public bool CheckIsAboveGrid (Blocks blocks)
    {
        for (int x = 0; x < GridWidth; ++x)
        {
            foreach (Transform block in blocks.transform)
            {
                Vector2 pos = RoundVector(block.position);

                if (pos.y > GridHeight - 2)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private bool IsFullRowAt (int y)
    {
        for (var x = 0; x < GridWidth; ++x)
        {
            if(_grid[x,y] == null)
            {
                return false;
            }
        }
        return true;
    }
    
    public void DeleteBlocksAt (int y)
    {
        for (var x = 0; x<GridWidth; ++x)
        {
            Destroy(_grid[x, y].gameObject);
            _grid[x, y] = null;
        }
    }

    public void MoveRowDown (int y)
    {
        for (var x = 0; x < GridWidth; ++x)
        {
            if (_grid[x, y] == null) continue;
            _grid[x, y - 1] = _grid[x, y];
            _grid[x, y] = null;
            _grid[x, y - 1].position += new Vector3 (0, -1, 0);
        }
    }

    public void MoveAllRowsDown (int y)
    {
        for (var i = y; i < GridHeight; ++i)
        {
            MoveRowDown(i);
        }
    }

    public void DeleteRow()
    {
        for (var y = 0; y < GridHeight; ++y)
        {
            if (!IsFullRowAt(y)) continue;
            DeleteBlocksAt(y);

            MoveAllRowsDown(y + 1);

            --y;
            pointDeleteLine++;
            scoresPoint.text = pointDeleteLine.ToString();
        }
    }
      

    public void UpdateGrid (Blocks blocks)
    {
        for (var y = 0; y < GridHeight; ++y) {
            for (int x = 0; x < GridWidth; ++x)
            {
                if (_grid[x, y] == null) continue;
                if (_grid[x,y].parent == blocks.transform)
                {
                    _grid[x, y] = null;
                }
            }
        }
        foreach (Transform block in blocks.transform)
        {
            var pos = RoundVector(block.position);
            if (pos.y <GridHeight)
            {
                _grid[(int)pos.x, (int)pos.y] = block;

            }
        }
    }

    public Transform GetTransformAtGridPosition (Vector2 pos)
    {
        return pos.y > GridHeight ? null : _grid[(int)pos.x, (int)pos.y];
    }

    public static bool IsInGrid (Vector2 pos)
    {
        return (int)pos.x >= 0 && (int)pos.x < GridWidth && (int)pos.y >= 0;
    }

    public Vector2 RoundVector(Vector2 pos) // Округляем положение кубов в int, чтобы он мог вписываться в массив
    {
        return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
    }

    public void GameOver()
    {
        SceneManager.LoadScene(1);
    }
        

}
