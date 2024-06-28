using System;

class UI {
    public static int askInteger(string prompt, int a, int b) {
        bool good = false;
        int result = 0;
        while (!good) {
            Console.Write($"{prompt} > ");
            good = int.TryParse( Console.ReadLine(), out result );
            if ( !good || result < a || result >= b ) {
                Console.WriteLine("\nПолучено некорректное значение. Повторите ввод.\n");
                good = false;
            }
        }
        return result;
    }
}