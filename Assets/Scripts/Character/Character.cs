using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float moveSpeed;

    public float OffsetY { get; private set; } = 0.3f;

    public bool onBoat = false;

    private const float walkingSpeedMultiplier = 1f;
    private const float runningSpeedMultiplier = 2f;
    private const float ridingSpeedMultiplier = 2f;

    public float speedMultiplier = walkingSpeedMultiplier;

    public void ChangeMultiplier(float newVal)
    {
        speedMultiplier = newVal;
    }

    CharacterAnimator animator;
    private void Awake()
    {
        animator = GetComponent<CharacterAnimator>();
        SetPositionAndSnapToTile(transform.position);
    }

    public void SetPositionAndSnapToTile(Vector2 pos)
    {
        // floor -> approx
        pos.x = Mathf.Floor(pos.x) + 0.5f;
        pos.y = Mathf.Floor(pos.y) + 0.5f + OffsetY;

        transform.position = pos;
    }


    public IEnumerator Move(Vector2 moveVec, Action OnMoveOver=null)
    {
        //Debug.Log(moveVec);
        //Debug.Log($"using {speedMultiplier} multiplier");
        animator.MoveX = Mathf.Clamp(moveVec.x, -1f, 1f);
        animator.MoveY = Mathf.Clamp(moveVec.y, -1f, 1f);
        Debug.Log("raws: " + moveVec + " clamped: "+animator.MoveX+", "+animator.MoveY);

        var targetPos = transform.position;
        targetPos.x += moveVec.x;
        targetPos.y += moveVec.y;

        if (!isWalkable(targetPos))
            yield break;

        animator.IsMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        Debug.Log("actual: " + transform.position + " new: " + targetPos);
        transform.position = targetPos;

        animator.IsMoving = false;

        OnMoveOver?.Invoke();
    }

    private bool isWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.2f, GameLayers.i.SolidLayer | GameLayers.i.InteractableLayer) != null)
        {
            return false;
        }
        if ((Physics2D.OverlapCircle(targetPos, 0.2f, GameLayers.i.seaLayer) != null) && !onBoat)
        {
            return false;
        }
            return true;
    }

    public CharacterAnimator Animator
    {
        get => animator;
    }
}
