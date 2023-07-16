using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayer : MonoBehaviour
{
    bool canWalk = true;
    public float speed = 5f;
    GameObject flower;
    [SerializeField] GameObject tempAbla;
    // Update is called once per frame
    void Update()
    {
        if (canWalk)
        {
            //go forward with rigidbody
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            
        }
    }

    public void StopWalking()
    {
        canWalk = false;
        GetComponent<Animator>().SetTrigger("collect");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Flower")
        {
            flower = other.gameObject;
            StopWalking();
            var lookPos = other.transform.position;
            lookPos.y = transform.position.y;
            transform.LookAt(lookPos);
        }

        if (other.gameObject.name == "LookForAlienLocation")
        {

        }

        if (other.gameObject.name == "CocukTrigger")
        {
            GetComponent<Animator>().SetBool("goidle", true);
            canWalk = false;

            DialogueManager.Instance.StartDialogue(Resources.Load<DialogueData>("Dialogues/Quests/LF/0"));

        }
    }

    public void CollectFlower()
    {
        flower.transform.parent.gameObject.SetActive(false);
    }

    public void Idle()
    {
        GetComponent<Animator>().SetTrigger("idle");
    }
}
