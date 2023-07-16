using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleRemoveAlien : MonoBehaviour
{
    [SerializeField] GameObject alien, graveyard, spaceShip;
    private void OnEnable()
    {
        alien.SetActive(false);
        spaceShip.SetActive(false);
        graveyard.SetActive(true);
    }
}
