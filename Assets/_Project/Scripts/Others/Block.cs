using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int Id;
    private MeshRenderer _renderer;
    private Collider _collider;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider>();
    }
    public void SetBlock(int id, Color color)
    {
        Id = id;
        _renderer.material.color = color;
    }

    public void CollectMe()
    {
        _renderer.enabled = false;
        _collider.enabled = false;
        StartCoroutine(ShowMe());
    }
    
    private IEnumerator ShowMe()
    {
        float time = Random.Range(5, 10);
        yield return new WaitForSeconds(time);
        _renderer.enabled = true;
        _collider.enabled = true;
    }
}
