using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchGameplay : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("test");
            collision.GetComponent<MovePlayer>().OnSheep();
        }
    }
}
