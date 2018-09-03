using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour {

    public static List<Particle> Particles;
    
    public Rigidbody ParticleRigidbody;
    
    void FixedUpdate ()
    {
        foreach (Particle particle in Particles)
        {
            if (particle != this)
                Attract(particle);
        }
    }

    #region State Handling
    
    void OnEnable ()
    {
        if (ParticleRigidbody == null);
            ParticleRigidbody = gameObject.AddComponent<Rigidbody>();
        ParticleRigidbody = gameObject.GetComponent<Rigidbody>();
        ParticleRigidbody.mass = Random.Range(1.0f, 10.0f);
        ParticleRigidbody.drag = Random.Range(0.1f, 0.5f);
        ParticleRigidbody.angularDrag = Random.Range(0.1f, 0.5f);
        ParticleRigidbody.useGravity = false;
        ParticleRigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        
        if (Particles == null)
            Particles = new List<Particle>();

        Particles.Add(this);
    }

    void OnDisable ()
    {
        Particles.Remove(this);
    }
    
    public void GraspBegin ()
    {
        Particles.Remove(this);
    }
    
    public void GraspEnd ()
    {
        Particles.Add(this);
    }
    #endregion
    
    void Attract (Particle particleToAttract)
    {
        Rigidbody rigidbodyToAttract = particleToAttract.ParticleRigidbody;

        Vector3 direction = ParticleRigidbody.position - rigidbodyToAttract.position;
        float distance = direction.magnitude;

        float forceMagnitude = (ParticleRigidbody.mass * rigidbodyToAttract.mass) / Mathf.Pow(distance, 1/2);
        Vector3 force = direction.normalized * forceMagnitude;

        rigidbodyToAttract.AddForce(force);
    }
}