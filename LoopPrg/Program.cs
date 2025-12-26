using System;

class Program {
    static void Main() {
        Console.Write("정수 입력: ");
        int n = int.Parse(Console.ReadLine() ?? "1");
        for (int i = 1; i <= n * n; i++) {
            Console.Write($"{i,4}");
            if (i % n == 0) Console.WriteLine();
        }
    }
}