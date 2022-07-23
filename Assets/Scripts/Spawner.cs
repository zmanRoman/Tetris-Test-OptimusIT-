using UnityEngine;

public class Spawner : MonoBehaviour {

    [SerializeField] private  Blocks[] blocks; //массив с блоками
    private Blocks _nextBlock,_currentBlock;

    [SerializeField] private Transform nextBlock;
    private void Start () 
    {
        SpawnBlocks();
    }

    private int RandomBlock()
    {
        return Random.Range(0, 7);
    }

    public void SpawnBlocks()
    {
        if  (_currentBlock == null)
        {
            _currentBlock = Instantiate(blocks[RandomBlock()], transform.position, Quaternion.identity);
        }
        else
        {
            _currentBlock = _nextBlock;
            _currentBlock.transform.position = transform.position;
        }
        
        _currentBlock.enabled = true;
        _nextBlock = Instantiate(blocks[RandomBlock()], nextBlock.position, Quaternion.identity);
        _nextBlock.enabled = false;
    }
}
