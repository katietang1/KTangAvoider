using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScript : MonoBehaviour
{
    public GameObject LevelClearedImage;

    // Start is called before the first frame update
    void Start()
    {
        LevelClearedImage.SetActive(false);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Player")
        {
            if (collision.gameObject.GetComponent<PlayerController>().HasKey)
            {
                LevelClearedImage.SetActive(true);
                Destroy(this.gameObject);
            }
            else
                Debug.Log("Cannot exit without key.");
        }
    }
}
