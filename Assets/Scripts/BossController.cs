using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{

    public static BossController instance;

    public BossAction[] actions;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {

    }

    void Update()
    {

    }
}

[System.Serializable]
public class BossAction
{
    [Header("Action")]
    public float actionLength;
    public float moveSpeed;
    public float timeBetweenShots;
    public bool
        shouldMove,
        shouldChasePlayer,
        moveToPoints,
        shouldShoot;
    public Transform pointToMoveTo;
    public Transform[] shotPoints;
    public GameObject itemToShoot;
}

