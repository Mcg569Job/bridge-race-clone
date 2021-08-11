using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _blockPrefab;

    [SerializeField] private int _gridWitdh;
    [SerializeField] private int _gridHeight;

    [SerializeField] private float _spaceX;
    [SerializeField] private float _spaceZ;

    private BoxCollider _collider;

    void Start()
    {
        _collider = GetComponent<BoxCollider>();
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        float xScale = ((_blockPrefab.transform.localScale.x * _gridWitdh) + (_spaceX * (_gridWitdh - 1)) / 2);
        float zScale = ((_blockPrefab.transform.localScale.z * _gridHeight) + (_spaceZ * (_gridHeight - 1)) / 2);

        float xOffset = -.5f * xScale + (_blockPrefab.transform.localScale.x / 2);
        float zOffset = -.5f * zScale + (_blockPrefab.transform.localScale.z / 2);

        float posX, posZ;

        for (int i = 0; i < _gridWitdh; i++)
        {
            posX = transform.localPosition.x + xOffset + (i * _spaceX);
            for (int j = 0; j < _gridHeight; j++)
            {
                posZ = transform.localPosition.z + zOffset + (j * _spaceZ);

                PlayerBag playerBag = LevelManager.Instance.currentLevelSettings.GetRandomBag();
                Block b = Instantiate(_blockPrefab, new Vector3(posX, transform.localPosition.y, posZ), Quaternion.identity).GetComponent<Block>();
                b.transform.parent = this.transform;
                b.SetBlock(playerBag.playerId, playerBag.color);
                yield return new WaitForSeconds(.001f);
            }
        }

        _collider.size = new Vector3(30, 1, zScale);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 scale = new Vector3(((_blockPrefab.transform.localScale.x * _gridWitdh) + (_spaceX * (_gridWitdh - 1)) / 2),
        transform.position.y,
         (_blockPrefab.transform.localScale.z * _gridHeight) + (_spaceZ * (_gridHeight - 1)) / 2);
        Gizmos.DrawWireCube(transform.position, scale);
    }
}
