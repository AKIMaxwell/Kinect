using System;
using System.Threading;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("主线程开始");
        /*=========================================/ 
        Thread 类拥用4个重载的构造函数，常用的一个接收一个ThreadStart类型的参数 
        public Thread ( ThreadStart start) 
        ThreadStart是一个委托，定义如下 
        public delegate void ThreadStart() 
         /=========================================*/
        Thread th = new Thread(new ThreadStart(ThreadMethod)); //也可简写为new Thread(ThreadMethod);                
        th.Start(); //启动线程  
        for (char i = 'a'; i < 'k'; i++)
        {
            Console.WriteLine("主线程：{0}", i);
            Thread.Sleep(100);
        }
        th.Join(); //主线程等待辅助线程结束  
        Console.WriteLine("主线程结束");
        Console.ReadKey();
    }
    static void ThreadMethod()
    {
        Console.WriteLine("辅助线程开始...");
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine("辅助线程：{0}", i);
            Thread.Sleep(200);
        }
        Console.WriteLine("辅助线程结束.");
    }
}  

