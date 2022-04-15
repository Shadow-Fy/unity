# FMS有限状态机



## 什么是有限状态机

一个可以枚举出有限个状态，并且这些状态在特定条件下是能够来回切换的。



具体例子：在游戏中经常看到的一些AI，如敌人巡逻，巡逻过程中看到玩家就追击，追上了就攻击，追不上并且有了一定的距离就返回去继续巡逻。



Unity中的Animator就是一个FSM了，不过Animator是控制角色动画播放的，什么状态的时候播放什么动画。而这里写的FSM是控制角色AI的，什么状态就做什么事。



FSM 跟 Switch case做的事一样一样，类比于 Switch 就是将case中的逻辑封装到各个State中了。



## 有限状态机的使用

结构

BaseState 状态基类

各种State,每个状态机内部实现开关逻辑



BaseState代码

需要用到抽象类 abstract

```c#
public abstract class EnemyBaseState
{
    public abstract void EnterState(Enemy enmey);//进入的时候执行

    public abstract void OnUpdate(Enemy enemy);//状态中执行
}


```



Enemy代码

**获取状态**

获取巡逻状态`public PatrolState patrolState = new PatrolState();`

获取攻击状态`public AttackState attackState = new AttackState();`

刚开始时执行`patrolState.OnUpdate(this);`进行巡逻状态，巡逻状态中OnUpdate会一直触发

当按下J后会执行`attackState.EnterState(this);`进行攻击状态，攻击状态是EnterState 只会在开始的时候触发一次

```C#
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public PatrolState patrolState = new PatrolState();
    public AttackState attackState = new AttackState();
    
    void Start()
    {
        patrolState.OnUpdate(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
        	attackState.EnterState(this);
        }

        currentState.OnUpdate(this);
    }


    public void moveTo()
    {
        Debug.Log("you are moving!");
    }
}
```





**巡逻时的代码**

巡逻状态只运行了OnUpdatee没有运行EnterStat所以只会运行moveTo函数，显示`you are moving!`

```c#
using UnityEngine;

public class PatrolState : EnemyBaseState//巡逻
{
    public override void EnterState(Enemy enmey)
    {
        Debug.Log("change the way");
    }

    public override void OnUpdate(Enemy enemy)//每一帧不断循环
    {
        
        enemy.moveTo();
    }
}
```



**攻击时的代码**

攻击状态只运行了EnterState没有运行OnUpdate所以只会显示`you are ready to attack`

```C#
using UnityEngine;

public class AttackState : EnemyBaseState
{
    public override void EnterState(Enemy enmey)
    {
        Debug.Log("you are ready to attack");
    }

    public override void OnUpdate(Enemy enemy)//每一帧不断循环
    {
        Debug.Log("Attacking");

    }
}

```

