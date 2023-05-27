﻿namespace DemoApp;

class Program
{
    [ThreadStatic]
    static string client;

    static void HandleJob(int id)
    {
        Console.WriteLine("Thread<{0}> has accepted job<{1}> for {2}", Thread.CurrentThread.ManagedThreadId, id, client);
        Worker.DoWork(id);
        Console.WriteLine("Thread<{0}> has finished job<{1}> for {2}", Thread.CurrentThread.ManagedThreadId, id, client);
    }

    static void Main(string[] args)
    {
        int n = args.Length > 0 ? int.Parse(args[0]) : 1;
        Thread child = new Thread(() => 
        {
            client = "Jack";
            HandleJob(n);            
        });
        //runtime does not wait for a background thread to exit
        child.IsBackground = n > 10;
        child.Start();
        client = "Jill";
        HandleJob(2);
    }
}
