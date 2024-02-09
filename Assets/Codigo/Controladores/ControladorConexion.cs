using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorConexion : MonoBehaviour
{
    AudioSource audio;
    Rigidbody2D rigidbody;
    
    [SerializeField] float fuerzaMatar;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Matar() 
    {
        audio.Play();
        rigidbody.constraints = RigidbodyConstraints2D.None;
        rigidbody.AddForce(Vector2.right * fuerzaMatar);
    }
}
