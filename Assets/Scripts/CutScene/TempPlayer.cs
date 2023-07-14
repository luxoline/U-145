using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayer : MonoBehaviour
{
    bool canWalk = true;
    public float speed = 5f;
    GameObject flower;
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
