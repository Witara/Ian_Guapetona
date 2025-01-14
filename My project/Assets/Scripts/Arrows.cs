using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public float speed = .5f;
    public Vector2 screenBounds;
    public string arrowDirection;

    private AmongUsHandler amongUsHandler;
    private ShrekHandler shrekHandler;
    private SkibidiHandler skibidiHandler;
    private SmurfHandler smurfHandler;
    private RickHandler rickHandler;

    private SpriteRenderer spriteRenderer;
    public Color[] colors = { Color.red, Color.green, Color.blue, Color.yellow, Color.cyan, Color.magenta };
    private int currentColorIndex;
    private bool colorChanged = false;
    void Start()
    {
        amongUsHandler = Object.FindFirstObjectByType<AmongUsHandler>();
        shrekHandler = Object.FindFirstObjectByType<ShrekHandler>();
        skibidiHandler = Object.FindFirstObjectByType<SkibidiHandler>();
        smurfHandler = Object.FindFirstObjectByType<SmurfHandler>();
        rickHandler = Object.FindFirstObjectByType<RickHandler>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Start with a random color
        currentColorIndex = Random.Range(0, colors.Length);
        spriteRenderer.color = colors[currentColorIndex];
    }

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);

        if (transform.position.y > 2.2f && !colorChanged)
        {
            colorChanged = true;
            ChangeColor();
            TriggerAnimations();
        }

        if (transform.position.y > screenBounds.y)
        {
            Destroy(gameObject);
        }
    }

    void ChangeColor()
    {
        currentColorIndex = (currentColorIndex + 1) % colors.Length;
        spriteRenderer.color = colors[currentColorIndex];
    }

    void TriggerAnimations()
    {
        if (amongUsHandler != null) amongUsHandler.SetAnimationDirection(arrowDirection);
        if (shrekHandler != null) shrekHandler.SetAnimationDirection(arrowDirection);
        if (skibidiHandler != null) skibidiHandler.SetAnimationDirection(arrowDirection);
        if (smurfHandler != null) smurfHandler.SetAnimationDirection(arrowDirection);
        if (rickHandler != null) rickHandler.SetAnimationDirection(arrowDirection);
    }
}
