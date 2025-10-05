using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CollectorEntity : MonoBehaviour
{
    public Dictionary<int,int> collections;
    public ParticleSystem collectionEffect;
    public AudioClip collectionSound;
    public CollectableData[] collectableDatas;
    
    void Start()
    {
        collections = new Dictionary<int, int>();
    }
    
    void OnTriggerStay2D(Collider2D other)
    {
        CollectibleEntity collectible = other.GetComponent<CollectibleEntity>();
        if (collectible != null)
        {
            if (collections.ContainsKey(collectible.type))
            {
                collections[collectible.type]++;
            }
            else
            {
                collections[collectible.type] = 1;
            }
            collectionEffect.transform.position = other.transform.position;
            collectionEffect.Play();
            AudioSource.PlayClipAtPoint(collectionSound, other.transform.position);
            Destroy(other.gameObject);
        }
    }
}