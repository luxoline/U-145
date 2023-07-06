using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WateringCorn : MonoBehaviour
{
    bool doneWatering = false;
    bool finishedWatering = false;

    [SerializeField] GameObject waterCan;
    [SerializeField] AudioClip wateringSound;
    private void OnTriggerEnter(Collider other)
    {
        if (finishedWatering) return;
        if (other.CompareTag("Player"))
        {
            InteractionCanvasManager.Instance.gameObject.SetActive(true);
            InteractionCanvasManager.Instance.target = this.transform;
            WaypointManager.Instance.gameObject.SetActive(false);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (finishedWatering) return;
        if (other.CompareTag("Player"))
        {
            var playerAnimator = other.GetComponent<Animator>();
            var playerController = other.GetComponent<PlayerController>();

            var rotation = this.transform.rotation;
            Vector3 lookAtPosition = this.transform.position;
            lookAtPosition.y = playerController.transform.position.y;
            if (Input.GetKeyDown(KeyCode.E))
            {
                playerController.GetComponent<AudioSource>().PlayOneShot(wateringSound);
                playerController.GetComponent<AudioSource>().volume = 0.5f;
                playerController.canWalk = false;
                waterCan.SetActive(true);
                InteractionCanvasManager.Instance.gameObject.SetActive(false);
                playerController.transform.LookAt(lookAtPosition);
                playerAnimator.SetTrigger("Watering");
                StartCoroutine(BeginWatering());
            }

            if (doneWatering)
            {
                playerController.GetComponent<AudioSource>().Stop();
                playerController.GetComponent<AudioSource>().volume = 1f;
                finishedWatering = true;
                doneWatering = false;
                waterCan.SetActive(false);
                transform.rotation = rotation;
                playerController.canWalk = true;
                if (QuestManager.Instance.currentQuest.questNumber == 2)
                {
                    Debug.Log("uzay gemisi dusme sinematigi girecek.");
                }
                QuestManager.Instance.CompleteQuest();
                WaypointManager.Instance.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (finishedWatering) return;
        if (other.CompareTag("Player"))
        {
            InteractionCanvasManager.Instance.gameObject.SetActive(false);
            WaypointManager.Instance.gameObject.SetActive(true);
        }
    }

    IEnumerator BeginWatering()
    {
        yield return new WaitForSeconds(5.15f);
        doneWatering = true;
    }
}
