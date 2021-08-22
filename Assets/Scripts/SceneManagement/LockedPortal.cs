using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LockedPortal : MonoBehaviour, IPlayerTriggerable
{
    [SerializeField] LockedDestinationsIds destinationPortal;
    [SerializeField] Transform spawnPoint;
    [SerializeField] string password;

    Fader fader;

    PlayerController player;

    private bool isLocked = true;  

    public void OnPlayerTriggered(PlayerController player)
    {
        this.player = player;
        if(isLocked == false)
            StartCoroutine(Teleport());
    }

    public void Unlock(KeyItem key)
    {
        if (key.code == password)
            isLocked = false;
    }

    private void Start()
    {
        fader = FindObjectOfType<Fader>();
    }


    IEnumerator Teleport()
    {
        GameController.Instance.PauseGame(true);
        yield return fader.FadeIn(0.5f);

        var destPortal = FindObjectsOfType<LockedPortal>().First(x => x != this && x.destinationPortal == this.destinationPortal);
        Debug.Log(this.destinationPortal);
        player.Character.SetPositionAndSnapToTile(destPortal.spawnPoint.position);

        yield return fader.FadeOut(0.5f);
        GameController.Instance.PauseGame(false);
    }

    public Transform SpawnPoint => spawnPoint;
}

public enum LockedDestinationsIds
{
    TestLocation
}