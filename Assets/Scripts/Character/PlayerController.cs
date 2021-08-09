using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, ISavable
{
    [SerializeField] new string name;
    [SerializeField] Sprite sprite;
    [SerializeField] private float speed = 0.5f;

    //const float offsetY = 0.3f;

    private PlayerInput playerInput;
    private Rigidbody2D rb;
    private BoxCollider2D cl;

    public Action OnEncountered;

    private Character character;

         
    private void Awake() {
        character = GetComponent<Character>();
        playerInput = new PlayerInput();
        rb = GetComponent<Rigidbody2D>();
        cl = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    // Update is called once per frame
    public void HandleUpdate()
    {
        Vector2 moveInput = playerInput.Freeroam.Move.ReadValue<Vector2>();
        //rb.velocity = moveInput * speed;
        rb.

        if(!character.Animator.IsMoving)
        {
            StartCoroutine(character.Move(moveInput, OnMoveOver));
        }
    }

    void Interact()
    {
        var facingDir = new Vector3(character.Animator.MoveX, character.Animator.MoveY);
        var interactPos = transform.position + facingDir;

        //Debug.DrawLine(transform.position, interactPos, Color.green, 0.5f);
        //Debug.Log("Drawing Line.");

        var collider = Physics2D.OverlapCircle(interactPos, 0.3f, GameLayers.i.InteractableLayer);
        if(collider != null)
        {
            collider.GetComponent<Interactable>()?.Interact();
        }
    }

    private void CheckForEncounters() {
        if(Physics2D.OverlapCircle(transform.position, 0.2f, GameLayers.i.GrassLayer) != null) {
            if(UnityEngine.Random.Range(1, 101) <= 10) {
                OnEncountered();
            }
        }
    }

    void Fish()
    {
        var facingDir = new Vector3(character.Animator.MoveX, character.Animator.MoveY);
        var interactPos = transform.position + facingDir;

        //Debug.DrawLine(transform.position, interactPos, Color.green, 0.5f);
        //Debug.Log("Drawing Line.");

        var collider = Physics2D.OverlapCircle(interactPos, 0.3f, GameLayers.i.seaLayer);
        if (collider != null)
        {
            Debug.Log("Fishing.");
        }
    }

    private void OnMoveOver()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position - new Vector3(0, character.OffsetY), 0.2f, GameLayers.i.TriggerableLayers);
        
        foreach (var collider in colliders)
        {
            var triggerable = collider.GetComponent<IPlayerTriggerable>();
            if(triggerable!=null)
            {
                triggerable.OnPlayerTriggered(this);
                break;
            }
        }
    }

    public object CaptureState()
    {
        // return data that have to be saved
        // viene serializzata una classe non un vector3, quindi converti
        float[] position = new float[] { transform.position.x, transform.position.y }; // z doesnt matter
        return position; // object return value rappresenta ogni tipo di variabile
    }

    public void RestoreState(object state)
    {
        var position = (float[])state;
        transform.position = new Vector2(position[0], position[1]);
    }

    public Character Character => character;

    public Sprite Sprite
    {
        get => sprite;
    }
}