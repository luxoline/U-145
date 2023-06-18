using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cicek : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            QuestManager.Instance.questObjects.Add("cicek");
            Destroy(gameObject);
        }
    }
}
