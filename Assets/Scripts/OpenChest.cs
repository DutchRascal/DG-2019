using UnityEngine;

public class OpenChest : MonoBehaviour
{
    private float scaleSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }

    private void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3Int.one, Time.deltaTime * scaleSpeed);
    }
}
