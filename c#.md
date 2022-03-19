# unity c#学习



## 创建变量

1、创建

```c#
public/private    int/float/bool      speed = 5.5f;
//公开/私有            变量类型         变量名称

//int整型       float浮点型       bool(true/false)
public bool isJump;//默认false

public string name = "abcde";//string字符型

public GameObject player;//unity中创建的项目，可以直接拖拽获取项目，可以使用此项目中的所有conponent(组件)

public Transform playermovement;//具体更改Transform的值



```



2、void start()   游戏开始后执行其中的代码



3、void update()  每一帧执行一次代码



4、void Awake()  无论script（此代码）是否启动，都在开始的时候运行



ps：

```c#
Debug.Log("123");//Console窗口会显示，用于测试bug
```



## 变量的计算

```c#
public float 当前血量;
public  health = 5;
public float attack = 3.5f;
public string name = "jack"

void start()
{	
    当前血量 = health - attack;
    Debug.Log(name + "'s health is " + 当前血量)；
}
```



## 乱七八糟

```c#
两点之间的距离 比较      当前位置             目标位置
if(Vector2.Distance(transform.position, movePos.position) < 0.1f)
    
时间控制
if(waitTime <= 0)
{
    movePos.position = GetRandomPos();
    waitTime = startWaitTime;
}
else
{
    waitTime -= Time.deltaTime;//按照当前的时间减少
}

两个点之间移动                                    起点                  终点               移动速度
 transform.position = Vector2.MoveTowards(transform.position, movePos.position, speed*Time.deltaTime);

```















































# unity C#用到的代码

## Move && Jump

```c#
public Rigibody2D rb;
public float speed = 10f;

public bool isGround, isJump;

public Transform groundCheck; //需要在外部拖动这个项目来获取位置
public LayerMask ground;//需要在外部设置

bool jumpPressed;

int jumpCount;//跳跃次数

void Start()
{
    rb = GetComponent<Rigidbody2D>();
    
}

void update()
{
    if(Input.GetButtonDown("jump") && jumpcount > 0)
    {
        jumpRessed = true;//是否按下跳跃
    }
}

private void FixedUpdate()
{
    isGround = Physics2D.OverlapCircle(groundCheck.position, 0.4f, ground);//用一个空项目放在脚下，通过射线范围检测脚下是否有地板(地板图层为ground)
    
    GroundMovement();
    
    Jump();
}


void GroundMovement()//左右移动
{
    float horizontalmove = Input.GetAxisRaw("Hoirzontal");
    //横向移动
    rb.velocity = new Vector2(horizontalmove * speed, rb.velosity.y);// 左（-1）,0,右（1）
    
    if(horizontalmove != 0)
    {
        transform.localScale = new Vector3(horizontalmove, 1, 1);//方向
    }
}


void Jump()//跳跃
{
    if(isGround)
    {
        jumpCount = 2;//二连跳
        isJump = false;
    }
    if(jumpPressed && isGroud)
    {
        isJump = true;
        rb.velocity = new Vector2(rb.velosity.x, jumpForce);
        jumpCount--;
        jumpPressed = false;
    }
    else if(jumpPressed && jumpcount > 0 && !isGround)
    {
        rb.velocity = new Vector2(rb.velosity.x, jumpForce);
        jumpCount--;
        jumpPressed = false;
    }
    
}
```

## Enemy(面向对象)

```c#
public abstract class Enemy:MonoBehaviour//成为父类
{
    public int health;//设置生命值
	public int damage;//设置伤害
    
    
    public float FlashTime;//受伤闪烁时间
    private SpriteRenderer sr;
    private Color oringinalColor; 

    public void start()//必须用public使得其他项目可以调用
    {
		sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    public void Update()//必须用public使得其他项目可以调用
    {
		if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)//必须用public使得其他项目可以调用（在下面攻击中调用了）
    {
        health -= damage;
         FlashColor(FlashTime)；
    }
    
    //受伤闪烁变色
    void FlashColor(float time)
    {
        sr.color = Color.red;
        Invoke("ResetColor", time);//延迟恢复颜色
    }
    
    //颜色还原
    void ResetColor()
    {
        sr.color = originalColor;
    }
}

```

```c#
//运用到攻击上面的代码
public int damage;//伤害值

void OnTriggerEnter2D(Collider2D other)
{ 
    if(other.gameObject.CompareTag("Enemy"))
    {
        other.GetComponet<Enemy>().TakeDamage(damage);
    }
}
```

```c#
public class EnemyBat : Enemy//归属于上面父类中的子类
{

    public float speed;
    public float startWaitTime;
    private float waitTime;
    
    public Transform movePos;
    public Transform leftDownPos;
    public Transform rightUpPos;
    
    
	public void Start()//public必用
    {
        base.Start();
        waitTime = startWaitTime;
        movePos.position = GetRandomPos();
    }
    
    public void Update()//public必用
    {
        base.Update();//此行用于调用父类的Update
        transform.position = Vector2.MoveTowards(transform.position, movePos.position, speed*Time.deltaTime);
        
        if(Vector2.Distance(transform.position, movePos.position) < 0.1f)//距离控制
        {
            if(waitTime <= 0)
            {
                movePos.position = GetRandomPos();
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;//时间控制
            }
        }
    }
    
	
    
	void GetRandomPos()
    {
        Vector2 rndPos = new Vector2(Random.Range(leftDownPos.position,x, rightUpPos.x), Random.Range(leftDownPos.position.y, rightUpPos.position.y));
        return rndPos;
    }
}

```

```c#
public class EnemySmartBat : Enemy
{
    public float speed;
    public float radius;
    
    public void Start()
    {
        base.Start();
        playerTransform = GameObject.FindGameObjectWithTag("player").GetComponent<Transform>();
    }
    
    public boid Update()
    {
        base.Update();
        if(playerTransform != NULL)
        {
            float distance = (transform.position - playerTransform.position).sqrMagnitude;
            
            if(distance < radius)
            {
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed *Time.deltaTime);
            }
        }
    }
}
```

