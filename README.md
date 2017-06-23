1，SDK简介

本SDK是基于.NET C#开发类库扩展,因此只能在.net平台下使用（要求2.0以上）SDK中用到了很多设计模式，也是你学习代码编写很好的例子.

2，需要支持的平台

淘宝开放平台,微信开放平台,腾讯QQ,腾讯微博,新浪微博,网易微博,人人网,360,豆瓣,Github,Google,MSN,点点,百度,开心网,搜狐,饿了么,百度外卖,美团.

3、主要的核心类



4、配置格式

SDK的配置格式如下（可参考DEMO中的配置）
appId
appSecret
sign


5，接入登录方法

添加ThinkPHP扩展，将整个ThinkSDK目录放入到ThinkPHP的扩展目录下Extend/Library/ORG/。
添加SDK配置，按以上配置格式在项目配置中添加对应的SDK配置。（可参考DEMO中的配置文件）
跳转到授权页面，导入SDK基类import("ORG.ThinkSDK.ThinkOauth")，获取SDK实例$sdk=ThinkOauth::getInstance($type)，跳转到授权页面redirect($sdk->getRequestCodeURL())。（可参考DEMO中的Index/login方法）
获取access_token，在授权成功的回调页面中，调用$sdk->getAccessToken($code, $extend)方法来获取access_token。（可参考DEMO中的Index/callback方法）
6，调用API方法

成功获取到access_token之后就可以调用相应平台的API了，调用方法比较简单，只需要调用$sdk->call($api, $param, $method)方法就可以了，其中：$api为接口名称，$param为接口参数（格式：name1=value1&name2=value2）, $method为请求方法（GET或POST）。

调用示例：

 
