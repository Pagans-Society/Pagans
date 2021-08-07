using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, ISavable
{
    [SerializeField] new string name;
    [SerializeField] Sprite sprite;

    //const float offsetY = 0.3f;

    private Vector2 input;

    public Action OnEncountered;

    private Character character;

    List<ItemBase> fishingObjs = new List<ItemBase>();

    int firstInputAxis = 0; // 0=x, 1=y
         
    private void Awake() {
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    public void HandleUpdate()
    {
        //Vector2 mousePos = Mouse.current.position.ReadValue
        /*if (Input.GetKeyDown(KeyCode.LeftShift))
            character.ChangeMultiplier(2f);*/

        /*var gamepad = Gamepad.current;
        if (gamepad == null)
            return; // no gamepad connected so switch to keyboard

        input = gamepad.leftStick.ReadValue();

        if (!character.Animator.IsMoving) {

            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            //annulla i movimenti diagonali
            if(input.x != 0 && input.y != 0)
            {
                if (firstInputAxis == 0)
                {
                    input.x = 0;
                    firstInputAxis = 1;
                }
                else
                {
                    input.y = 0;
                    firstInputAxis = 0;
                }

            }

            if(input != Vector2.zero)
            {
                StartCoroutine(character.Move(input, OnMoveOver));
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
            Interact();

        if (Input.GetKeyDown(KeyCode.F))
            Fish();*/
    }

    public void Move(InputAction.CallbackContext context)
    {
        StartCoroutine(character.Move(context.ReadValue<Vector2>(), OnMoveOver));
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