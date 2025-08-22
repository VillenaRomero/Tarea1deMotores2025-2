using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float speed = 5f;
    public Sprite newSprite;             
    public Color newColor = Color.red;   

    private SpriteRenderer sr;
    private Vector3 moveDirection;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float moveY = Input.GetAxisRaw("Vertical");   
        float moveX = Input.GetAxisRaw("Horizontal"); 

        moveDirection = new Vector3(moveX, moveY, 0).normalized;

        transform.position += moveDirection * speed * Time.deltaTime;

        if (moveDirection != Vector3.zero)
        {
            Debug.DrawRay(transform.position, moveDirection, Color.red);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, moveDirection, out hit, 1f))
            {
                GameObject obj = hit.collider.gameObject;

                Debug.Log("Nombre: " + obj.name);
                Debug.Log("Posición: " + obj.transform.position);

            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ColorChanger"))
        {
            SpriteRenderer objSr = collision.gameObject.GetComponent<SpriteRenderer>();
            if (objSr != null)
            {
                sr.color = objSr.color; 
            }
            else
            {
                sr.color = newColor; 
            }
        }

        if (collision.gameObject.CompareTag("SpriteChanger"))
        {
            SpriteRenderer objSr = collision.gameObject.GetComponent<SpriteRenderer>();
            if (objSr != null)
            {
                sr.sprite = objSr.sprite; 
            }
            else
            {
                sr.sprite = newSprite;
            }
        }
    }
}
