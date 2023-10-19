using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(CapsuleCollider))]
public class DragonController : MonoBehaviour
{
    #region States
    public BossIdleState BossIdleState;
    public BossFollowState BossFollowState;
    private StateBoss currentState;
    #endregion

    #region Parameters
    public  Transform Player;
    public float DistanceToFollow = 4f;
    public float DistanceToAttack = 3f;
    public float Speed = 1f;
    public GameObject prefabStone;
    public Transform FirePoint;
    public Transform FirePointDirection;
    public float CoolDownTime = 1.0f;
    public float HP = 6f;
    public float currentHP;
    public Slider healthBar;
    #endregion

    #region Readonly Properties
    public Rigidbody rb {private set; get;}
    public Animator animator {private set; get;}
    #endregion

    [SerializeField]
    private GameObject bossAmbiance;
    [SerializeField]
    private GameObject winRestart;
    [SerializeField]
    private AudioSource breathSound;
    [SerializeField]
    private AudioSource dragonDeath;
    [SerializeField]
    private AudioSource hitSound;

    private void Awake() 
    {
        BossIdleState = new BossIdleState(this);
        BossFollowState = new BossFollowState(this);

        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        // Seteamos el estado inicial
        currentState = BossIdleState;
    }

    private void Start() 
    {
        currentState.OnStartBoss();
        currentHP = HP;
        UpdateHealthBar(); 
        
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Max(0, currentHP); 
        UpdateHealthBar();

        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = HP/currentHP;
        }
    }

    private void Update() 
    {
        foreach (var transition in currentState.Transitions)
        {
            if (transition.IsValid())
            {
                // Ejecutar Transicion
                currentState.OnFinishBoss();
                currentState = transition.GetNextState();
                currentState.OnStartBoss();
                break;
            }
        }
        currentState.OnUpdateBoss();    
    }

    public void Fire()
    {
        GameObject stone = Instantiate(prefabStone, FirePoint.position, Quaternion.identity);
        stone.GetComponent<StoneMovement>().Direction = Player.position - FirePointDirection.position;
        breathSound.Play();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 7)
        {
            hitSound.Play();
            HP -= 1;
            UpdateHealthBar();
            if (HP <= 0) 
            {
                dragonDeath.Play();
                Destroy(gameObject);
                bossAmbiance.GetComponent<BossAmbiance>().BossDied();
                this.winRestart.SetActive(true);
            }
            
            
        }

    }

}
