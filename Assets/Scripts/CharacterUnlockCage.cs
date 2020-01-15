using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUnlockCage : MonoBehaviour
{
    private bool canUnlock;
    private CharacterSelector playerToUnlock;

    public GameObject message;
    public CharacterSelector[] characterSelectors;
    public SpriteRenderer cagedSR;

    private void Start()
    {
        playerToUnlock = characterSelectors[Random.Range(0, characterSelectors.Length)];
        cagedSR.sprite = playerToUnlock.playerToSpawn.bodySR.sprite;
    }

    private void Update()
    {
        if (canUnlock)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Instantiate(playerToUnlock, transform.position, transform.rotation);
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canUnlock = true;
            message.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canUnlock = false;
            message.SetActive(false);
        }
    }
}
