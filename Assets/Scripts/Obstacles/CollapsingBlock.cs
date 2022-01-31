using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CollapsingBlock : MonoBehaviour
{
    [SerializeField]
    private bool humanCollapses;
    [SerializeField]
    private bool robotCollapses;
    [SerializeField]
    private float respawnTime;
    [SerializeField]
    private PlayerMode mode;

    private Collider2D blockCollider;
    private TilemapRenderer renderer;
    private AudioSource crumbleSound;

    private bool collapsed;
    private float timeCollapsed;

    private void Start()
    {
        blockCollider = GetComponent<Collider2D>();
        renderer = GetComponent<TilemapRenderer>();



        crumbleSound = gameObject.AddComponent<AudioSource>();
        crumbleSound.clip = Resources.Load("sfx/crumble") as AudioClip;
        crumbleSound.volume = 0.03f;
    }

    public void Update()
    {
        if (collapsed)
        {
            timeCollapsed += Time.deltaTime;

            if (timeCollapsed >= respawnTime)
            {
                Respawn();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (humanCollapses)
        {
            if (collision.gameObject.CompareTag("Player") && !mode.RobotMode)
            {
                Collapse();
            }
        }
        
        if (robotCollapses)
        {
            if (collision.gameObject.CompareTag("Player") && mode.RobotMode)
            {
                Collapse();
            }
        }
    }

    public void Collapse()
    {
        renderer.enabled = false;
        collapsed = true;
        blockCollider.enabled = false;
        crumbleSound.Play();
    }

    public void Respawn()
    {
        renderer.enabled = true;
        timeCollapsed = 0.0f;
        collapsed = false;
        blockCollider.enabled = true;
    }
}
