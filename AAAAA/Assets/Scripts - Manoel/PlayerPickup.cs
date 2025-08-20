using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public Transform itemHolder;
    private GameObject currentItem;
    private float pickupCooldown = 0f;

    void Update()
    {
        if (pickupCooldown > 0)
            pickupCooldown -= Time.deltaTime;

        if (currentItem != null && Input.GetKeyDown(KeyCode.E))
        {
            currentItem.transform.SetParent(null);
            currentItem.transform.position = transform.position + new Vector3(1f, 0, 0);

            Collider2D col = currentItem.GetComponent<Collider2D>();
            if (col != null)
                col.enabled = true;

            currentItem = null;
            pickupCooldown = 0.3f;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (pickupCooldown <= 0 && other.CompareTag("Item") && currentItem == null)
        {
            currentItem = other.gameObject;
            currentItem.transform.SetParent(itemHolder);
            currentItem.transform.localPosition = Vector3.zero;

            Collider2D col = currentItem.GetComponent<Collider2D>();
            if (col != null)
                col.enabled = false;
        }
    }
}
