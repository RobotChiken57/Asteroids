using UnityEngine;

public class Asteroid : MonoBehaviour
{
   public Sprite[] sprites;
   public float size = 1.0f;
   public float minSize = 0.5f;
   public float maxSize = 1.5f;
   public float speed = 5.0f;
   public float maxLifetime = 30.0f;
   private SpriteRenderer _spriteRederer;
   private Rigidbody2D _rigidbody;

   private void Awake()
   {
        _spriteRederer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
   }
    private void Start()
    {
        _spriteRederer.sprite = sprites[Random.Range(0, sprites.Length)];

        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        this.transform.localScale = Vector3.one * this.size;

        _rigidbody.mass = this.size;
    }

    public void SetTrajectory(Vector2 direction)
    {
        _rigidbody.AddForce(direction * this.speed);

        Destroy(this.gameObject, this.maxLifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if((this.size * 0.5f) >= this.minSize)
            {
                CreateSlip();
                CreateSlip();
            }
            Destroy(this.gameObject);

        }
    }
    private void CreateSlip()
    {
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;

        Asteroid half = Instantiate(this, position, this.transform.rotation);
        half.size = this.size * 0.5f;
        half.SetTrajectory(Random.insideUnitCircle.normalized);

    }

}
