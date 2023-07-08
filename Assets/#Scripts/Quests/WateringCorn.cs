using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WateringCorn : MonoBehaviour
{
    public bool doneWatering = false;
    public bool finishedWatering = false;
    bool closeToCorn = false;
    bool startedWatering = false;
    bool began = false;

    [SerializeField] GameObject waterCan;
    [SerializeField] AudioClip wateringSound;


    private void Update()
    {
        if (finishedWatering) return;
        if (!closeToCorn) return;
        if (began) return;
        if (Input.GetKeyDown(KeyCode.E))
        {
            startedWatering = true;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (finishedWatering) return;
        if (other.CompareTag("Player"))
        {
            InteractionCanvasManager.Instance.EnableCanvas();
            InteractionCanvasManager.Instance.target = this.transform;
            WaypointManager.Instance.DisableCanvas();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (finishedWatering)
        {
            InteractionCanvasManager.Instance.DisableCanvas();
            return;
        }
        closeToCorn = true;
        if (other.CompareTag("Player"))
        {
            var playerAnimator = other.GetComponent<Animator>();
            var playerController = other.GetComponent<PlayerController>();

            var rotation = this.transform.rotation;
            Vector3 lookAtPosition = this.transform.position;
            lookAtPosition.y = playerController.transform.position.y;
            if (startedWatering)
            {
                startedWatering = false;
                began = true;
                playerController.GetComponent<Rigidbody>().velocity = Vector3.zero;
                playerController.GetComponent<AudioSource>().PlayOneShot(wateringSound);
                playerController.GetComponent<AudioSource>().volume = 0.5f;
                playerController.canWalk = false;
                waterCan.SetActive(true);
                InteractionCanvasManager.Instance.DisableCanvas();
                playerController.transform.LookAt(lookAtPosition);
                playerAnimator.SetTrigger("Watering");
                //StartCoroutine(BeginWatering());
            }

            //if (doneWatering)
            //{
            //    transform.rotation = rotation;
            //    DoneWatering(playerController);
            //}
        }
    }

    public void DoneWatering(PlayerController playerController)
    {
        playerController.GetComponent<AudioSource>().Stop();
        playerController.GetComponent<AudioSource>().volume = 1f;
        finishedWatering = true;
        doneWatering = false;
        waterCan.SetActive(false);
        playerController.canWalk = true;
        if (QuestManager.Instance.currentQuest.questNumber == 2)
        {
            Debug.Log("uzay gemisi dusme sinematigi girecek.");
        }
        InteractionCanvasManager.Instance.DisableCanvas();
        QuestManager.Instance.CompleteQuest();
        WaypointManager.Instance.EnableCanvas();
    }

    private void OnTriggerExit(Collider other)
    {
        if (finishedWatering) return;
        closeToCorn = false;
        if (other.CompareTag("Player"))
        {
            InteractionCanvasManager.Instance.DisableCanvas();
            WaypointManager.Instance.DisableCanvas();
        }
    }


    IEnumerator BeginWatering()
    {
        yield return new WaitForSeconds(5.15f);
        doneWatering = true;
    }
}
