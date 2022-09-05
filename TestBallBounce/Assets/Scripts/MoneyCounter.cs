using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MoneyCounter : MonoBehaviour
{
    public CounterPanel counterPanel;
    public TMP_Text moneyCounter;
    public GameObject shatterParticle;
    public GameObject explosionParticel;
    public int myMoney = 0;
    public int newMoney = 0;
    public int coinMoney = 50;
    public int moneyCountToTriggerEffect = 10;
    public float totalCountTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        moneyCounter.text = myMoney.ToString();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            newMoney += coinMoney;
            StopAllCoroutines();
            StartCoroutine(counterPanel._effect.Execute(coinMoney/moneyCountToTriggerEffect));
            StartCoroutine(Counter());
            Instantiate(explosionParticel, other.transform.position, shatterParticle.transform.rotation);
            Instantiate(shatterParticle, other.transform.position, shatterParticle.transform.rotation);
            Destroy(other.gameObject);

        }
    }

    public IEnumerator Counter()
    {   
        float time = 0f;
        int startMoney = myMoney;

        while (myMoney != newMoney)
        {
            time += Time.deltaTime/totalCountTime;
            myMoney = (int)Mathf.Lerp(startMoney, newMoney, time);
            moneyCounter.text = myMoney.ToString("0");

            yield return null;
        }    
    }
}


