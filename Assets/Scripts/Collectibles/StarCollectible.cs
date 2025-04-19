using UnityEngine;

public class StarCollectible : MonoBehaviour
{
    public AudioClip collectSound;
    public GameObject collectEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.root.CompareTag("Player"))
        {
            Debug.Log("Entered Trigger with:" + other.name);

            if (collectSound)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
            }

            if (collectEffect)
            {
                Instantiate(collectEffect, transform.position, Quaternion.identity);
            }

            // Add Score - added here later for game manager

            // Destorys the star
            Destroy(gameObject);
        }
    }
}
