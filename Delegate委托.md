# Delegate 委托



## 1.什么是委托

**委托可以认为是一个容器，用于存放函数，在调用时可以同时调用这个容器中的多个函数**



简单来说委托是一种回调机制

C#通过委托来实现回调函数

回调函数是一种很有用的编程机制，可以被广泛应用在观察者模式中



委托多用于当某个值，或者物体状态发生改变的时候

其他一些操作需要同时进行监听

一旦状态发生，改变，所有注册在委托中的函数，都会被调用

具体可参考[Unity 项目中委托Delegate用法案例](https://blog.csdn.net/ChinarCSDN/article/details/80387157)



## 2.如何使用委托

1.声明一个委托类型 `public delegate [函数返回值 例如void int float...] [委托名称(参数)]` 

2.创建委托变量 `public [委托名称] [委托变量名称]` 

3.声明与该**委托类型相同（返回值类型和参数类型相同）**的函数

4.把委托函数添加进委托当中

5.调用委托

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



