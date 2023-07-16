using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempAbla : MonoBehaviour
{
    [SerializeField] GameObject tempPlayer;
    bool canWalk = false;
    [SerializeField] float walkSpeed = 2f;
    Animator animator;




    private void FixedUpdate()
    {
        if (!canWalk) return;

        transform.Translate(Vector3.forward * Time.deltaTime * walkSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "AblaTrigger")
        {
            Debug.Log("abla trigger");
            DialogueManager.Instance.StartDialogue(Resources.Load<DialogueData>("Dialogues/Quests/AblaGameEnd/0"));
            Debug.Log(Resources.Load<DialogueData>("Dialogues/Quests/AblaGameEnd/0").dialogueText);
            canWalk = false;
            animator.SetBool("walk", false);
        }
    }

    private void OnEnable()
    {
        canWalk = true;
        animator = GetComponent<Animator>();
        animator.SetBool("walk", true);
        tempPlayer.GetComponent<Animator>().SetTrigger("idle");

    }
}
