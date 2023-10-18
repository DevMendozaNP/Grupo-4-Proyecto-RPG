using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMovement : MonoBehaviour
{
    public string Direction;

    [SerializeField]
    private float timeToDestroy = 1f;

    [SerializeField]
    private RectTransform rectTransform;
    private Rigidbody rb;
    private float timer = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (Direction == "up")
        {
            rectTransform.localRotation = Quaternion.Euler(0, 0f, 0f);
        }
        else if (Direction == "right") 
        {
           rectTransform.localRotation = Quaternion.Euler(0, 90f, 0f);
        }
        else if (Direction == "down")
        {
            rectTransform.localRotation = Quaternion.Euler(0, 180f, 0f);
        }
        else if (Direction == "left")
        {
            rectTransform.localRotation = Quaternion.Euler(0, -90f, 0f);
        }

        
        timer += Time.deltaTime;
        if (timer >= timeToDestroy)
        {
            SwordDestroy();
        }
    }

    private void SwordDestroy()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        // Hay una colision
        // TODO: Falta hacerle danho al jugador
        SwordDestroy();
    }
}
