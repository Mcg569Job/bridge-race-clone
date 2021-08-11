
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class CharacterBase : MonoBehaviour
{
    public delegate void Action();

    [Header("Character Collision Raycast")]
    [SerializeField] private LayerMask _layerMaskCollision;

    [Header("Ground Raycast")]
    [SerializeField] private LayerMask _layerMaskGround;
    [SerializeField] private Transform _rayPoint;

    [Header("Speed")]
    [SerializeField] [Range(1, 20)] private float _speed;

    [Header("Player Bag")]
    public PlayerBag bag;
    [HideInInspector]
    public Stair myStair;

    [Header("Variables")]
    protected bool IsBot;
    protected bool IsGround;
    protected int score;

    [Header("Components")]
    protected Rigidbody _rigidbody;
    private MeshRenderer _mesh;

    [Header("Actions")]
    protected Action collectAction;
    protected Action dropAction;
    protected Action collisionAction;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _mesh = GetComponent<MeshRenderer>();
        _mesh.material.color = bag.color;
        bag.SpawnBlocks();
    }


    #region  Collision
    protected void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Block")
        {
            if (bag.BagIsFull()) return;

            Block b = other.GetComponent<Block>();
            if (b.Id == bag.playerId || b.Id == -1)
            {
                bag.AddBlock();
                b.CollectMe();

                if (collectAction != null)
                    collectAction.Invoke();

                if (b.Id == -1) Destroy(b.gameObject);
            }
        }
        else if (other.tag == "Stair")
        {
            if (bag.blockCount > 0)
            {
                Stair s = other.GetComponentInParent<Stair>();

                if (IsBot)
                {
                    if (myStair != s)
                        return;
                }
                else
                {
                    myStair = s;
                }

                s.AddStep(bag.color);

                RemoveBlockInBag();
            }
        }
        else if (other.tag == "Finish")
        {
            if (IsBot)
               GameManager.Instance.GameOver();
            else
               GameManager.Instance.Win();
        }      
        
    }

    protected void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Step")
        {
            if (bag.blockCount > 0)
            {
                MeshRenderer renderer = other.transform.GetComponent<MeshRenderer>();
                if (renderer.material.color == bag.color)
                    return;

                renderer.material.color = bag.color;

                if (!IsBot)
                {
                    Stair s = other.transform.parent.GetComponent<Stair>();
                    myStair = s;
                }

                RemoveBlockInBag();
            }
        }

    }
    #endregion

    #region Blocks & Bag
    private void RemoveBlockInBag()
    {
        bag.RemoveBlock();

        if (dropAction != null)
            dropAction.Invoke();
    }
    public void DropAllBlocks()
    {
        if (collisionAction != null)
            collisionAction.Invoke();

        bag.DropAllBlocks(transform);
        _rigidbody.AddForce(transform.up * 3000);
    }

    private void CollisionDetectionWithCharacter()
    {
        Debug.DrawRay(transform.position, transform.forward * .6f, Color.blue);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, .6f, _layerMaskCollision))
        {
            if (hit.collider != null)
            {
                hit.transform.GetComponent<CharacterBase>().DropAllBlocks();
            }
        }
    }
    #endregion

    #region Physics
   
    protected void Move(float horizontal, float vertical)
    {
         Debug.DrawRay(_rayPoint.position, -_rayPoint.up, Color.red);
        IsGround=Physics.Raycast(_rayPoint.position, -_rayPoint.up, _layerMaskGround);

        if (vertical != 0 || horizontal != 0)
        {
            Quaternion newRot = Quaternion.Euler(0, (Mathf.Atan2(horizontal, vertical) * 180 / Mathf.PI), 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRot, Time.deltaTime * 4);

            if (IsGround)
            {
                Vector3 velocity = transform.forward * _speed * Time.deltaTime * 10;
                velocity.y = _rigidbody.velocity.y;
                _rigidbody.velocity = velocity;
            }
            else
            {
                ResetVelocity();               
            }
        }
        else
            ResetVelocity();

        CollisionDetectionWithCharacter();
    }

    protected void ResetVelocity()
    {
        _rigidbody.velocity = new Vector3(0, _rigidbody.velocity.y, 0);
        _rigidbody.angularVelocity *= 0;
    }
    #endregion
}
