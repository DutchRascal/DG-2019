using UnityEngine;

public class Room : MonoBehaviour
{

    [HideInInspector] public bool roomActive = false;

    public bool closeWhenEnterred;
    public GameObject[] doors;
    public GameObject mapHider;

    public void OpenDoors()
    {
        foreach (GameObject door in doors)
        {
            door.SetActive(false);
            closeWhenEnterred = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            CameraController.instance.ChangeTarget(transform);
            HandleDoors();
        }
    }

    private void HandleDoors()
    {
        if (closeWhenEnterred)
        {
            foreach (GameObject door in doors)
            {
                door.SetActive(true);
            }
        }
        roomActive = true;
        mapHider.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
            roomActive = false;
    }
}
