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



## 条件语句

同c语言；



















































# unity C#用到的代码

## 移动跳跃代码

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
    if(isGroud)
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
