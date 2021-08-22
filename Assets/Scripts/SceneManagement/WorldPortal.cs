using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldPortal : MonoBehaviour, IPlayerTriggerable
{
    [SerializeField] SceneDetails destinationWorld;
    [SerializeField] Transform spawnPoint;

    Fader fader;

    PlayerController player;
    public void OnPlayerTriggered(PlayerController player)
    {
        this.player = player;
        StartCoroutine(Teleport());
    }

    private void Start()
    {
        fader = FindObjectOfType<Fader>();
    }


    IEnumerator Teleport()
    {
        GameController.Instance.PauseGame(true);
        yield return fader.FadeIn(0.5f);

        player.Character.SetPositionAndSnapToTile(destinationWorld.spawner.transform.position);

        yield return fader.FadeOut(0.5f);
        GameController.Instance.PauseGame(false);
    }

    public Transform SpawnPoint => spawnPoint;
}