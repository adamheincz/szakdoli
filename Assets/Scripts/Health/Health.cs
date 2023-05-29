using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("Settings")]
    public float maxHealth = 100;
    [SerializeField]
    private bool hasInvulnerability = false;

    private float currentHealth;

    public UnityEvent<float> OnHealthChanged;
    public UnityEvent OnIsInvulnerable;
    public UnityEvent OnIsDead;
    public bool isDead = false;
    public bool isInvulnerable;

    private void Start()
    {
        SetHealth(maxHealth);
    }

    public void SetHealth(float health)
    {
        currentHealth = health;
        OnHealthChanged?.Invoke(currentHealth);
    }

    public void LoseHealth(float damage)
    {
        SetHealth(Mathf.Max(currentHealth - damage, 0));
        isDead = currentHealth == 0;
        if(isDead)
        {
            Debug.Log("isDead");
            OnIsDead?.Invoke();
        }
        if (hasInvulnerability)
        {
            StartCoroutine(Invulnerability());
        }
    }

    public IEnumerator Invulnerability()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        for (int i = 0; i < 4; i++)
        {
            Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
            tmp.a = 0.5f;
            gameObject.GetComponent<SpriteRenderer>().color = tmp;
            yield return new WaitForSeconds(0.25f);
            tmp.a = 1f;
            gameObject.GetComponent<SpriteRenderer>().color = tmp;
            yield return new WaitForSeconds(0.25f);
        }
        gameObject.GetComponent<Collider2D>().enabled = true;
    }



}
