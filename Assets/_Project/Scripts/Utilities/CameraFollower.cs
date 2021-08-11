using UnityEngine;

public class CameraFollower : MonoBehaviour
{
  private Transform _target;
  [SerializeField]private Vector3 _offset;
    void Start()=>  _target= GameObject.FindGameObjectWithTag("Player").transform;
    

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,_target.position + _offset,Time.deltaTime* 4);
    }
}
