using System;
using UnityEngine;

public class Blocks : MonoBehaviour {

    private float _fall, _fallSpeed = 1.5f,_speed, _scores;
    public bool falling;
    private Game _gameplayHandler;
    private Spawner _spawner;

    private void Start()
    {
        _gameplayHandler = FindObjectOfType<Game>();
        _spawner =  FindObjectOfType<Spawner>();
    }

    private void Update ()
    {
        UserTransformPositions();

        if (falling == false)
        {
            SpeedLevel();
        }
            
    }

    private void SpeedLevel ()
    {
        _fallSpeed = _scores switch
        {
            <= 5 => 1.5f,
            <= 10 => 0.7f,
            >= 11 => 0.3f,
            _ => _fallSpeed
        };
    }


    private void UserTransformPositions ()
    {
        if  (Time.time - _fall >=_fallSpeed)
        {
            MoveDown();
        }
        if (Input.GetKeyDown("down"))
        {
            falling = true;
            _fallSpeed = 0f;
        }
        if (Input.GetKeyDown("left"))
        {
            transform.position += new Vector3(-1, 0, 0); //меняем нашу позицию

            if (CheckIsValidPosition()) //проверка захода за границу
            {
                _gameplayHandler.UpdateGrid(this);      
            }
            else
            {
                transform.position += new Vector3(1, 0, 0); //возвращаем позицию  
            }
        }
        if (Input.GetKeyDown("right"))
        {
            transform.position += new Vector3(1, 0, 0);

            if (CheckIsValidPosition()) //проверка захода за границу
            {
                _gameplayHandler.UpdateGrid(this);
            }
            else
            {
                transform.position += new Vector3(-1, 0, 0);
            }
        }
       
        if (Input.GetKeyDown("z")) //вращение фигуры влево
        {
            transform.Rotate(0, 0, 90);

            if (CheckIsValidPosition()) //проверка захода за границу
            {
                _gameplayHandler.UpdateGrid(this);
            }
            else
            {
                transform.Rotate(0, 0, -90);
            }
        }

        if (Input.GetKeyDown("x")) //вращение фигуры вправо
        {
            transform.Rotate(0, 0, -90);

            if (CheckIsValidPosition()) //проверка захода за границу
            {
                _gameplayHandler.UpdateGrid(this);                            
            }
            else
            {
                transform.Rotate(0, 0, 90);
            }
        }
        ///Проверка на скорость
       _scores =  _gameplayHandler.pointDeleteLine;
    }

    private void MoveDown() // движение вниз
    {
        transform.position += new Vector3(0, -1, 0);      

        if (CheckIsValidPosition()) //проверка захода за границу
        {
            _gameplayHandler.UpdateGrid(this);
        }
        else
        {
            transform.position += new Vector3(0, 1, 0);
            _gameplayHandler.DeleteRow();//проверка на заполняемость линии
            if ( _gameplayHandler.CheckIsAboveGrid(this))
            {
                _gameplayHandler.GameOver();
            }
            falling = false;
            enabled = false;//выключаем скрипт на блоке
            _spawner.SpawnBlocks();
        }
        _fall = Time.time;
    }

    private bool CheckIsValidPosition () //проверка правельности позиции
    {
        foreach (Transform block in transform)
        {
            var pos =  _gameplayHandler.RoundVector(block.position);
            if ( Game.IsInGrid (pos) == false)
            {
                return false;
            }

            if ( _gameplayHandler.GetTransformAtGridPosition(pos) != null &&  _gameplayHandler.GetTransformAtGridPosition(pos).parent != transform)
            {
                return false;
            }
        }
        return true;
    }

}
