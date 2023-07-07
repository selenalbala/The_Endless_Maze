using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private static PlayerCamera instance;
    public Transform target;
    public float smoothing;

    public Vector2 maxPos;
    public Vector2 minPos;

    private void Awake()
    {
        // Verifica si ya existe una instancia de PlayerCamera y destruye el duplicado
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Guarda la instancia única de PlayerCamera
        instance = this;

        // Mantén el objeto PlayerCamera a lo largo de las escenas
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // Encuentra el objeto del personaje en la escena
        target = FindObjectOfType<PlayerMovement>()?.transform;
    }
    
    void LateUpdate()
    {
        if (target != null && transform.position != target.position)
        {
            Vector3 targetPos = new Vector3(target.position.x, target.position.y, transform.position.z);
            targetPos.x = Mathf.Clamp(targetPos.x, minPos.x, maxPos.x);
            targetPos.y = Mathf.Clamp(targetPos.y, minPos.y, maxPos.y);
            transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
        }
    }
}