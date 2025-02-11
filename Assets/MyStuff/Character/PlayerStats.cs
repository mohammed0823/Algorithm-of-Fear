using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
//using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Stats")]
    public float maxHealth = 100;
    public float health;
    //private float sanity;
    
    public static bool hasPen;
    public HealthBar healthBar;

    private GameObject player;
    
    
    private Rigidbody rb;

    public GameObject LoseScreen;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            TakeDamage(20);
        }
    }

    private void Start()
    {
        player = GameObject.Find("Player");
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        hasPen = false;
    }

    private void Update()
    {
        
    }

    private void checkHealth()
    {
        if (health <= 0)
            kill();       
    }

    private void kill()
    {
        Destroy(player);

        LoseScreen.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    void TakeDamage(float damage)
    {
        
        health -= damage;
        healthBar.SetHealth(health);
        checkHealth();
    }
}
