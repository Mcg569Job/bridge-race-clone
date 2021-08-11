using UnityEngine;

public class Player : CharacterBase
{
    [Header("Joystick")]
    [SerializeField] private Joystick _joystick;

    private void Start()
    {
        collectAction += CollectAction;
        dropAction += DropAction;
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.IsPlaying()) return;

        float vertical = _joystick.Vertical;
        float horizontal = _joystick.Horizontal;
        Move(horizontal, vertical);
    }

    private void DropAction()
    {
        if (myStair.IsFull)
            score++;
    }

    private void CollectAction()
    {
        if (LevelManager.Instance.currentLevelSettings.CheckStairs(score) ==false)
            GameManager.Instance.GameOver();

    }

    public void ResetPlayer()
    {
     bag.ResetBag();
     score=0;
    }
}
