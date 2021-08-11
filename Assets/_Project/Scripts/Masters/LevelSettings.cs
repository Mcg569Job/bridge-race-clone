using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettings : MonoBehaviour
{

    public Transform SpawnPoint;
    private List<CharacterBase> _players;
    private Stair[] _stairs;


    public void GetObjects()
    {
        _players = new List<CharacterBase>(GetComponentsInChildren<CharacterBase>());
        _players.Add(GameManager.Instance.Player);

        _stairs = GetComponentsInChildren<Stair>();

        for (int i = 0; i < _players.Count; i++)
            _players[i].bag.playerId = i;
    }
    public PlayerBag GetRandomBag()
    {
        return _players[Random.Range(0, _players.Count)].bag;
    }

    public Stair GetStair(int score)
    {
        Stair s = null;

        for (int i = 0; i < _stairs.Length; i++)
        {
            if (!_stairs[i].IsUsed  && _stairs[i].stairLevel == score)
            {
                s = _stairs[i];
                s.IsUsed = true;
            }
        }

        if (s == null)
        {
            bool b = false;
            for (int i = 0; i < 20; i++)
            {
                s = _stairs[Random.Range(0, _stairs.Length)];
                if (s.stairLevel == score && !s.IsFull)
                {
                    b = true;
                    break;
                }
            }
            if (!b) s = null;
        }

        return s;
    }

    public bool CheckStairs (int score)
    {
        bool b = false;
          for (int i = 0; i < _stairs.Length; i++)
        {
            if ( _stairs[i].stairLevel == score && !_stairs[i].IsFull)
            {
               b=true;
               break;
            }
        }
return b;
    }
}
