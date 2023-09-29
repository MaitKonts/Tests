using UnityEngine;
using System.Collections;

public class Pig : MonoBehaviour
{
    public int health = 100;
    public Material pigMaterial; // Reference to the pig's material
    public Color damageColor = Color.red; // Color to flash when damaged
    public float flashDuration = 0.1f; // Duration of the color flash
    public float knockbackForce = 1f; // The force of the knockback
    private Rigidbody rb; // Reference to the Rigidbody component

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
    }

    public void TakeDamage(int damage, Vector3 attackerPosition)
    {
        health -= damage;

        // Calculate the knockback direction
        Vector3 knockbackDirection = (transform.position - attackerPosition).normalized;
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse); // Apply the knockback force

        if (health <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(FlashColor());
        }
    }

    IEnumerator FlashColor()
    {
        pigMaterial.color = damageColor; // Change color to red
        yield return new WaitForSeconds(flashDuration); // Wait for the flash duration
        pigMaterial.color = Color.white; // Change color back to original
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
