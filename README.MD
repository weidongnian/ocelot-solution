ocelot for dotnet core 练习

1，创建空的解决方案 ocelot-solution

2，创建一个新的webApi(dotnet core c#)

3，在webApi里创建一个DefaultControlloer,并加入数据，给DefaultControlloer加上属性路由Route("api/[Controller]/[action]")

4，修改Properties/launchsettings.json 的绑定端口:5963

5，运行不调试 ctrl+F5

6，创建一个名为ocelotProject的webApi，nuget 引入ocelot=>Install-Package ocelot

7，在ocelotProject根目录下创建ocelot.json 文件
{
  "ReRoutes": [
    {
      "DownstreamPathTemplate": "/api/Default/{everything}",
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [
        {
          "Host": "localhost",
          "Port": 5963
        }
      ],
      "UpstreamPathTemplate": "/api/Default/{everything}",
      "UpstreamHttpMethod": [ "Get", "Post", "Put", "Delete" ]
    }
  ]
}

8，修改program.cs文件，加入ocelot的配置文件
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            //加入ocelot的配置文件
            .ConfigureAppConfiguration(conf =>
            {
                conf.AddJsonFile("ocelot.json", false, false);
            }).ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });

9，修改Startup文件,引用命名空间
//引入ocelot 注入拓展
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

加入ocelot服务

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //服务配置
            services.AddOcelot(Configuration);
        }

加载Ocelot中间件

           //启用Ocelot中间件
           app.UseOcelot().Wait();

10，运行不调试当前项目 ctrl+F5

浏览器访问 https://localhost:5001/api/default/GetList

它会自动代理到https://localhost:5963/api/default/GetList

简单又神奇，这样子，无论多少项目，对外就只用暴露一个接口域名就Ok了


