# unity小地图制作



## 1.思路

使用相机中的 `TargetTexture` 属性，可以将相机渲染的图片输出到 `Render Texture` 中，通过UI中的 `Raw Image` 获取 `RenderTexture` 的图片形成小地图



## 2.步骤

在 `Hierarchy` 中创建一个新的 `Camera` 

在 `Project` 中创建一个 `Render Texture` 

将 `Render Texture` 拖入到 `Camera` 中的 `TargetTexture` 

在 `Hierarchy` 创建一个UI中的 `Raw Image` 

将 `Render Texture` 拖入到 `Raw Image` 中的 `Texture` 



## 3.优化



**把小地图设置为圆形**

在 `Hierarchy` 中创建一个新的 `Image` 

给 `Image` 添加 `Mask` 组件用于实现遮罩，并且给 `Image` 的 `Source Image` 设置为 `Knob` 

在把 `Raw Image` 设为  `Image` 的子对象





**`Camera` 的优化**

`Camera` 的 ` Projection `有两种模式 `Perspective` 和 `Orthographic` 

`Perspective` 是透视相机，大部分用于3D

`Orthographic` 是正交相机，可用于unity中制作UI/2D游戏的开发



**`Camera` 跟随方法**

第一种直接将 `Camera` 直接拖拽成为 `player` 的子对象 2d可以直接用，但是3d的话小地图是会跟随人物旋转

第二种是通过代码实现

```c#
//3d的代码可以这样，2d的代码自己稍作修改即可
float posX = Player.instance.transform.position.x;    //角色的position.x
float posZ = Player.instance.transform.position.z;    //角色的position.z
OffsetY = Player.instance.transform.position.y + 20;  //摄像机与角色的偏移距离
transform.position = new Vector3(posX, OffsetY, posZ); 
 
```





