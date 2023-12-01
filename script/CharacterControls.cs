using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.PixelHeroes.Scripts
{
    public class CharacterControls : MonoBehaviour
    {
        public GameObject arrowPrefab; 
        public Character Character;

        public float RunSpeed = 1f;
        public float moveSpeed = 5f;
        public float boostMultiplier = 1.5f;
        private float _activityTime;
        private Rigidbody2D rb;
        private Vector2 moveDirection;
        private Vector3 lastMovementDirection;
        public SpriteRenderer spriteRenderer;
        public float arrowSpeed = 10f;
        public GameObject arrowPoint;
        public float fireRate = 1f;
        private float nextFireTime = 0f;

        public void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            Character.SetState(AnimationState.Idle);
        }

        public void ResetAnimation()
        { 
           Character.Animator.SetTrigger("Idle");
        }
        public void Update()
        {
            if(Input.GetKeyDown(KeyCode.M)){GameManager.Instance.MusicToggle();Debug.Log("MusicToggle");}
            //ResetAnimation();
            if (Input.GetKeyDown(KeyCode.Alpha1)) Character.Animator.SetTrigger("Attack");
            else if (Input.GetKeyDown(KeyCode.Alpha2)) Character.Animator.SetTrigger("Jab");
            else if (Input.GetKeyDown(KeyCode.Alpha3)) Character.Animator.SetTrigger("Push");
            else if (Input.GetKeyDown(KeyCode.Alpha4)) Character.Animator.SetTrigger("Hit");
            else if (Input.GetKeyDown(KeyCode.Alpha5)) { Character.SetState(AnimationState.Idle); _activityTime = 0; }
            else if (Input.GetKeyDown(KeyCode.Alpha6)) { Character.SetState(AnimationState.Ready); _activityTime = Time.time; }
            else if (Input.GetKeyDown(KeyCode.Alpha7)) Character.SetState(AnimationState.Blocking);
            else if (Input.GetKeyUp(KeyCode.Alpha8)) Character.SetState(AnimationState.Ready);
            else if (Input.GetKeyDown(KeyCode.Alpha9)) Character.SetState(AnimationState.Dead);
            
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");
            
            if (moveX > 0)
                spriteRenderer.flipX = false;
            else if (moveX < 0)
                spriteRenderer.flipX = true;

            if(moveX > 0 || moveY > 0)
            {
                Character.SetState(AnimationState.Running);
            }
            Vector3 movement = new Vector3(moveX, moveY, 0);

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                movement *= boostMultiplier; 
            }

            rb.velocity = movement * moveSpeed;
            HandleFiring();


        }


        private void HandleFiring()
        {
            
            if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime) 
                {
                    nextFireTime = Time.time + 1f / fireRate; 
                    FireArrowTowardsMouse();
                }
        }

        
        void FireArrowTowardsMouse()
        {
            GameObject arrow = Instantiate(arrowPrefab, arrowPoint.transform.position, Quaternion.identity);
            
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = arrowPoint.transform.position.z; 
            Vector2 direction = (targetPosition - arrowPoint.transform.position).normalized;

            
            arrow.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg );


            Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * arrowSpeed;
            }
        }
        void OnCollisionEnter2D(Collision2D collider)
        {
            
            
            if (collider.gameObject.CompareTag("Enemy"))
            {
                Destroy(gameObject);
            }
        }
        



    }
}