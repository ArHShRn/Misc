using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AsyncTest
{
    public delegate int AddHandler(int a, int b);

    public class AddClass
    {
        public static int Add(int a, int b)
        {
            Console.WriteLine("开始计算：" + a + "+" + b);
            Thread.Sleep(3000); //模拟该方法运行三秒
            Console.WriteLine("计算完成！");
            return a + b;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===== 异步回调 AsyncInvokeTest =====");
            AddHandler handler = new AddHandler(AddClass.Add);
            //异步操作接口(注意BeginInvoke方法的不同！)
            IAsyncResult result = handler.BeginInvoke(1, 2, new AsyncCallback(回调函数), "AsycState:OK");
            while(true)
            {
                Console.WriteLine("继续做别的事情。。。");
                Thread.Sleep(200);
            }
        }

        static void 回调函数(IAsyncResult result)
        {
            Console.WriteLine(result.AsyncState);
            
        }
    }
}
