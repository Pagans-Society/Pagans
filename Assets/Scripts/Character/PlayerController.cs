using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour, ISavable
{
    [SerializeField] new string name;
    [SerializeField] Sprite sprite;
    [SerializeField] InventoryUI inventoryUI;

    //const float offsetY = 0.3f;

    private Vector2 input;

    public Action OnEncountered;

    private Character character;

    Vector2 touchOrigin;

    public ItemBase equipedItem;
         
    private void Awake() {
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    public void HandleUpdate()
    {
        if (!character.Animator.IsMoving) {

#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR

            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
            
            if(Input.touchCount > 0)
            {
                Touch touch = Input.touches[Input.touches.Length-1];

                if(touch.phase == TouchPhase.Began)
                {
                    touchOrigin = touch.position;
                }

                else if (touch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
                {
                    Vector2 touchEnd = touch.position;
                    float x = touchEnd.x - touchOrigin.x;
                    float y = touchEnd.y - touchOrigin.y;

                    //touchOrigin.x = -1; // per non ripetere immediatamente

                    if (Mathf.Abs(x) > Mathf.Abs(y))
                        input.x = x > 0 ? 1 : -1;
                    else
                        input.y = y > 0 ? 1 : -1;
                }
            }


#endif

            //annulla i movimenti diagonali
            if (input.x != 0)
                input.y = 0;

            if(input != Vector2.zero)
            {
                StartCoroutine(character.Move(input, OnMoveOver));
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if(GetFrontalCollider() != null)
            {
                var dropItem = Interact().GetComponent<Interactable>().getDropItem();
                if (dropItem.Item != null)
                {
                    Inventory.GetInventory().AddItem(dropItem);
                    inventoryUI.UpdateItemList();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
            Fish();
    }

    Collider2D GetFrontalCollider()
    {
        var facingDir = new Vector3(character.Animator.MoveX, character.Animator.MoveY);
        var interactPos = transform.position + facingDir;

        //Debug.DrawLine(transform.position, interactPos, Color.green, 0.5f);
        //Debug.Log("Drawing Line.");

        var collider = Physics2D.OverlapCircle(interactPos, 0.3f, GameLayers.i.InteractableLayer);

        return collider;
    }

    Collider2D Interact()
    {
        var collider = GetFrontalCollider();

        if(collider != null)
        {
            collider.GetComponent<Interactable>()?.Interact();
        }

        return collider;
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
        if (collider != null && equipedItem != null && equipedItem.Name=="fishing rod")
        {
            Debug.Log("Fishing.");
        }
        else if(collider != null && (equipedItem == null || equipedItem.Name!= "fishing rod"))
        {
            StartCoroutine(DialogManager.Instance.ShowDialog(GlobalSettings.i.ToolTip));
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