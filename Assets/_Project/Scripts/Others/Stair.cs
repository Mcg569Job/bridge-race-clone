using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    [Header("Transforms ")]
    [SerializeField] private GameObject _door;
    [SerializeField] private Collider _box;
    [SerializeField] private Transform[] _sticks;

    [Header("Step ")]
   [SerializeField] private GameObject _stepPrefab;
    [SerializeField] private int _stepCount;
    public int stairLevel;
    private List<MeshRenderer> _steps = new List<MeshRenderer>();
    private int _stepId = 0;

    [Header("Bools")]
    [HideInInspector] public bool IsUsed;
    [HideInInspector] public bool IsFull;

    private void Start()
    {
        CreateStair();
    }

    private void CreateStair()
    {      

        for (int i = 0; i < _stepCount; i++)
        {
            Vector3 pos = _stepPrefab.transform.localScale * i;
            pos.x = 0;
            GameObject step = Instantiate(_stepPrefab);
            step.transform.parent = this.transform;
            step.transform.localPosition = pos;
            step.SetActive(false);
            _steps.Add(step.GetComponent<MeshRenderer>());
        }

        foreach (Transform t in _sticks)
            t.localScale = new Vector3(1, 1, _stepCount);

        _box.transform.localPosition = new Vector3(0, _stepPrefab.transform.localScale.y, -(_stepPrefab.transform.localScale.z / 2));

    }

    public void AddStep(Color color)
    {
        MeshRenderer m = _steps[_stepId];
        m.material.color = color;
        m.gameObject.SetActive(true);

        _stepId++;
        _box.transform.localPosition = new Vector3(0, _stepPrefab.transform.localScale.y * _stepId, (_stepPrefab.transform.localScale.z * _stepId) - (_stepPrefab.transform.localScale.z / 2) - .2f);

        if (!IsUsed)
            IsUsed = true;

        if (_stepId > _stepCount - 1)
        {
            _stepId = _stepCount - 1;
            _box.isTrigger = false;
            _box.transform.localPosition = new Vector3(0, _stepPrefab.transform.localScale.y, -(_stepPrefab.transform.localScale.z / 2));
            _door.SetActive(true);
            IsFull = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
      
        Vector3 pos = _stepPrefab.transform.localScale * _stepCount;
        pos.x = 0;
        pos.z -= (_stepPrefab.transform.localScale.z / 2);

        Gizmos.DrawLine(transform.localPosition, transform.localPosition + pos);


    }
}
