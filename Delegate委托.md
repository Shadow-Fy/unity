# Delegate 委托


## 了解什么是回调函数


把一段可执行的代码像参数传递那样传给其他代码，而这段代码会在某个时刻被调用执行，这就叫做回调。如果代码立即被执行就称为同步回调，如果在之后晚点的某个时间再执行，则称之为异步回调。



假设一个场景：

老师给学生布置了作业，学生收到作业后开始写作业，写完之后通知老师查看，老师查看之后就可以回家。

回调的概念，在这里面就体现的淋漓尽致，在这里面有两个角色，一个是老师，一个是学生。老师有两个动作，第一个是布置作业，第二个是查看作业。而学生有一个动作是做作业， 那么问题来了，老师并不知道学生何时才能做完作业，所以比较优雅的解决办法是等学生的通知，也就是学生做完之后告诉老师就可以。这就是典型的回调理念。

那么在编程中，该如何体现？ 从上面的分析中，可以得出来回调模式是双方互通的，老师给学生布置作业，学生做完通知老师查看作业。 关于回调，这里面还分同步回调和异步回调两种模式：

同步模式：

如果老师在放学后，给学生布置作业，然后一直等待学生完成后，才能回家，那么这种方法就是同步模式。

异步模式：

如果老师在放学后，给学生布置作业，这个时候老师并不想等待学生完成，而是直接就回家了，但告诉学生，如果完成之后发短信通知自己查看。这种方式就是异步的回调模式。




## 什么是委托

**委托可以认为是一个容器，用于存放函数，在调用时可以同时调用这个容器中的多个函数**



简单来说委托是一种回调机制

C#通过委托来实现回调函数

回调函数是一种很有用的编程机制，可以被广泛应用在观察者模式中



委托多用于当某个值，或者物体状态发生改变的时候

其他一些操作需要同时进行监听

一旦状态发生，改变，所有注册在委托中的函数，都会被调用

具体可参考[Unity 项目中委托Delegate用法案例](https://blog.csdn.net/ChinarCSDN/article/details/80387157)



## 如何使用委托

1.声明一个委托类型 `public delegate [函数返回值 例如void int float...] [委托名称(参数)]` 

2.创建委托变量 `public [委托名称] [委托变量名称]` 

3.声明与该**委托类型相同（返回值类型和参数类型相同）**的函数

4.把委托函数添加进委托当中

5.调用委托



当我们定义好委托类型后，也就定义了这个容器存放什么样的函数，接下来我们定义一个委托变量, 这个就比较简单了, Delegate_func clicks,如何往容器里面加入函数呢？

一个是赋值=，一个+= 一个是-=;

clicks = xxxx函数; clicks += xxxx函数; clicks -= xxxx函数;





```c#

using UnityEngine;

public class DelegateTest : MonoBehaviour
{

    public delegate void Hurtway(int peopleNo);//声明委托类型
    public Hurtway hurtway;//创建委托变量


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            hurtway = null;//清空委托中的函数
            hurtway += knife;//给委托容器中添加函数knife
            hurtway += gun;//给委托容器中添加函数gun
            hurtway(1);//用1调用委托容器中储存的函数
        }

        if(Input.GetKeyDown(KeyCode.W))
        {
            hurtway = null;//清空委托中的函数
            hurtway += knife;//给委托容器中添加函数knife
            hurtway += gun;//给委托容器中添加函数gun
            hurtway(2);//用2调用委托容器中储存的函数
        }
    }

    void knife(int peopleNo)//传入人编号调用函数选择受伤的方式
    {
        Debug.Log("person " + way +" are hurt by knife");


    }

    void gun(int peopleNo)//传入人编号调用函数选择受伤的方式
    {
        Debug.Log("person " + way +" are hurt by gun");
    }
}

```

按下Q显示结果为 `person 1 are hurt by knife` 

​							   `person 1 are hurt by gun` 

按下W显示结果为 `person 2 are hurt by knife`   

​                               `person 2 are hurt by gun` 



