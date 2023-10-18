using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Vector3 Speed = new Vector3(4f, 0f, 4f);
    [SerializeField]
    private float JumpSpeed = 4f;
    public float HP = 10f;

    private Rigidbody rb;
    private Animator animator;
    private PlayerInput playerInput;
    private CapsuleCollider capsuleCollider;
    private CapsuleCollider2D test;

    private Vector2 moveDir;
    private Vector3 currDir;

    public bool groundCheck;
    private bool attackSwitcher = true;
    public GameObject PowerBall;
    public GameObject FirePoint;
    public GameObject Sword;
    public GameObject MeleePoint;
    public string SwordPointing; 
    
    [SerializeField]
    private GameObject weaponIcon;
    [SerializeField]
    private GameObject healthMeter;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        test = GetComponent<CapsuleCollider2D>();
    }

    private void Start() 
    {
        DialogueManager.Instance.OnDialogueStart += OnDialogueStartDelegate;
        DialogueManager.Instance.OnDialogueFinish += OnDialogueFinishDelegate;
    }

    private void Update() 
    {
        moveDir.Normalize();
        rb.velocity = new Vector3(
            moveDir.x * Speed.x,
            rb.velocity.y,
            moveDir.y * Speed.z
        );
    }

    public void OnDialogueStartDelegate(Interaction interaction)
    {
        // Cambiar el Input Map al modo Dialogue
        playerInput.SwitchCurrentActionMap("Dialogue");
    }

    public void OnDialogueFinishDelegate()
    {
        // Cambiar el Input Map al modo Player
        playerInput.SwitchCurrentActionMap("Player");
    }

    private void OnMovement(InputValue value)
    {
        moveDir = value.Get<Vector2>();
        SetDir();
        if (Mathf.Abs(moveDir.x) > Mathf.Epsilon || 
            Mathf.Abs(moveDir.y) > Mathf.Epsilon)
        {
            animator.SetBool("IsWalking", true);
            animator.SetFloat("Horizontal", moveDir.x);
            animator.SetFloat("Vertical", moveDir.y);
        }else
        {
            animator.SetBool("IsWalking", false);
        }
        
    }
    private void SetDir() 
    {
        if (moveDir.x ==0 && moveDir.y == 1)
        {
            currDir = new Vector3(0, 0, 1);
            FirePoint.transform.position = new Vector3(0, 0.3f, 0.3f) + transform.position;
            SwordPointing = "up";
        }
        else if (moveDir.x == 1 && moveDir.y == 0) 
        {
            currDir = new Vector3(1, 0, 0);
            FirePoint.transform.position = new Vector3(0.3f, 0.3f, 0) + transform.position;
            SwordPointing = "right";
        }
        else if (moveDir.x == 0 && moveDir.y == -1)
        {
            currDir = new Vector3(0, 0, -1);
            FirePoint.transform.position = new Vector3(0, 0.3f, -0.3f) + transform.position;
            SwordPointing = "down";
        }
        else if (moveDir.x == -1 && moveDir.y == 0)
        {
            currDir = new Vector3(-1, 0, 0);
            FirePoint.transform.position = new Vector3(-0.3f, 0.3f, 0) + transform.position;
            SwordPointing = "left";
        }
    }

    private void OnSWeapon(InputValue value)
    {
        if(attackSwitcher == true)
        {
            attackSwitcher = false;
            weaponIcon.GetComponent<WeaponIcon>().weaponChange();
        }
        else
        {
            attackSwitcher = true;
            weaponIcon.GetComponent<WeaponIcon>().weaponChange();
        } 
    }

    private void OnAttack(InputValue value) 
    {
        if(attackSwitcher == true)
        {
            GameObject Ball = Instantiate(PowerBall, FirePoint.transform.position, Quaternion.identity);
            Ball.GetComponent<EnergyMovement>().Direction = currDir;
        }
        else if (attackSwitcher == false)
        {
            GameObject Knife = Instantiate(Sword, MeleePoint.transform.position, Quaternion.identity);
            Knife.GetComponent<SwordMovement>().Direction = SwordPointing;
        }

    }
    private void OnJump(InputValue value) 
    {
        if(value.isPressed && groundCheck) 
        {
            rb.velocity += new Vector3(0,JumpSpeed,0);
            groundCheck = false;
        }
    }

    private void OnNextInteraction(InputValue value)
    {
        if (value.isPressed)
        {
            // Siguiente Dialogo
            DialogueManager.Instance.NextDialogue();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Dialogue dialogue = other.collider.transform.GetComponent<Dialogue>();
        if (dialogue != null)
        {
            // Iniciar Sistema de Dialogos
            DialogueManager.Instance.StartDialogue(dialogue);
        }
        if (other.gameObject.layer == 6)
        {
            groundCheck = true;
        }
        if (other.gameObject.layer == 9)
        {
            HP -= 2;
            healthMeter.GetComponent<HealthBar>().ReceiveDamage(HP);
            if (HP <= 0) 
            {
                Destroy(gameObject);
            }
        }
        if (other.gameObject.layer == 10)
        {
            HP -= 1;
            healthMeter.GetComponent<HealthBar>().ReceiveDamage(HP);
            if (HP <= 0) 
            {
                Destroy(gameObject);
            } 
        }

    }

}
