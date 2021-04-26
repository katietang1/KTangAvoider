using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Player")
        {
            if (collision.gameObject.GetComponent<PlayerController>().HasKey)
            {
                Debug.Log("Level cleared!");
                Destroy(this.gameObject);
            }
            else
                Debug.Log("Cannot exit without key.");
        }
    }
}
