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

    private bool collapsed;
    private float timeCollapsed;

    private void Start()
    {
        blockCollider = GetComponent<Collider2D>();
        renderer = GetComponent<TilemapRenderer>();
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
    }

    public void Respawn()
    {
        renderer.enabled = true;
        timeCollapsed = 0.0f;
        collapsed = false;
        blockCollider.enabled = true;
    }
}
