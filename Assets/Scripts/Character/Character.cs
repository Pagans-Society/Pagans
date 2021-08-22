using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float moveSpeed;

    public float OffsetY { get; private set; } = 0.3f;

    public bool onBoat = false;

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

        animator.MoveX = Mathf.Clamp(moveVec.x, -1f, 1f);
        animator.MoveY = Mathf.Clamp(moveVec.y, -1f, 1f);

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
