using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDetector : MonoBehaviour
{
    [SerializeField] private AudioClip _detectorSound;
    private AudioSource _detectorSource;
    private float nextPlayTime;
    // Start is called before the first frame update
    void Start()
    {
        _detectorSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<FirstPersonMovement>()) {
            float colliderRadius = GetComponent<SphereCollider>().radius;
            float realRadius = colliderRadius * transform.localScale.x; //Asumes restricted scaling
            
            // Get distance
            Vector3 bombPosition = gameObject.transform.position;
            Vector3 playerPosition = other.gameObject.transform.position;

            float distance = Vector3.Distance(bombPosition, playerPosition);
            
            // A better implementation should be possible, but this is fine for now.
            if (distance > (2*realRadius / 3)) {
                if (Time.time >= nextPlayTime)
                    PlaySound(1f);
            } else if (distance <= (2*realRadius / 3) && distance >= (realRadius / 3)) {
                if (Time.time >= nextPlayTime)
                    PlaySound(0.6f);
            } else {
                if (Time.time >= nextPlayTime) 
                    PlaySound(0.3f);
            }
        }
    }

    void PlaySound(float delay)
    {
        Debug.Log("Play Sound");
        _detectorSource.PlayOneShot(_detectorSound);
        nextPlayTime += delay;
    }
}
