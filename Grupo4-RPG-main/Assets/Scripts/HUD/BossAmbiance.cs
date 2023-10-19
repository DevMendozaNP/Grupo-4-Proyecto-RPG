using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BossAmbiance : MonoBehaviour
{
    [SerializeField]
    GameObject bossHUD;
    
    [SerializeField] 
    AudioSource startBGM;

    [SerializeField]
    AudioSource bossBGM;

    [SerializeField]
    AudioSource winBGM;

    [SerializeField]
    GameObject dragon;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
           startBGM.Stop();
           bossBGM.Play();
           this.bossHUD.SetActive(true);
        }
    }

    public void BossDied()
    {
        bossBGM.Stop();
        winBGM.Play();
    }    
}
