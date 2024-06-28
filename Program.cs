using System;
using System.Linq;
using System.Collections.Generic;

class Program {
    static void Main(string[] args) {

        List<Field> A = DataBase.ReadData("books.txt");
        IEnumerable<Field> B;

        Console.WriteLine("Исходный список:");
        Console.WriteLine(Field.Header);
        DataBase.DisplayData(A);

        /*
         * I. Романы XIX века
         */
        B = from f in A
        where (f.genre == Genre.Novel) && (f.year > 1800) && (f.year < 1901)
        select f;

        Console.WriteLine("I. Список романов XIX века:");
        Console.WriteLine(Field.Header);
        DataBase.DisplayData(B);

        /*
         * II. Название начинается с буквы О
         */
        B = from f in A
        where f.title.StartsWith('О')
        select f;

        Console.WriteLine("II. Список произведений, названия которых начинаются с О:");
        Console.WriteLine(Field.Header);
        DataBase.DisplayData(B);

        /*
         * III. Найти самое старое произведение
         */
        B = from f in A
        where f.year == (from g in A select g.year).Min()
        select f;

        Console.WriteLine("III. Наиболее старое произведение:");
        Console.WriteLine(Field.Header);
        DataBase.DisplayData(B);

        /*
         * IV. Вывести список комедий и трегедий,
         * отсортированный по году
         */
        B = from f in A
        where (f.genre == Genre.Comedy) || (f.genre == Genre.Tragedy)
        orderby f.year
        select f;

        Console.WriteLine("IV. Список комедий и трагедий, отсортированный по году:");
        Console.WriteLine(Field.Header);
        DataBase.DisplayData(B);

        /*
         * V. Вывести список авторов поэм по алфавиту
         */
        IEnumerable<string> C = (from f in A
        where (f.genre == Genre.Poem)
        orderby f.author
        select f.author).Distinct();

        Console.WriteLine("V. Список авторов поэм, отсортированный по алфавиту:");
        DataBase.DisplayData(C);

        /*
         * VI. Выяснить, каков процент произведений, написанных
         * в нечётные года.
         */
        int odds = (from f in A where f.year % 2 == 1 select f).Count();

        Console.WriteLine($"VI. {odds * 100 / A.Count}% произведений написано в нечётные года.");

        /*
         * VII. Найти произведение с самым длинным названием
         */
        B = from f in A
        where f.title.Length == (from g in A select g.title.Length).Max()
        select f;

        Console.WriteLine("VII. Произведения с наиболее длинными названиями:");
        Console.WriteLine(Field.Header);
        DataBase.DisplayData(B);

        /*
         * VIII. Отсортировать данные по жанру
         */
        B = from f in A
        orderby f.genre
        select f;

        Console.WriteLine("VIII. Исходный список, отсортированный по жанру:");
        Console.WriteLine(Field.Header);
        DataBase.DisplayData(B);

        /*
         * IX. Вывести список произведений зарубежных авторов
         */
        B = from f in A
        where !f.author.Contains('.')
        select f;

        Console.WriteLine("IX. Список произведений зарубежных авторов:");
        Console.WriteLine(Field.Header);
        DataBase.DisplayData(B);

        /*
         * X. Вывести список произведений, чьи названия состоят из 2 слов.
         */
        B = from f in A
        where f.title.Split().Length == 2
        select f;

        Console.WriteLine("X. Список произведений, названия которых состоят из 2 слов:");
        Console.WriteLine(Field.Header);
        DataBase.DisplayData(B);

        int n = UI.askInteger("Введите размер словаря матриц:", 1, int.MaxValue);
        int s = UI.askInteger("Введите максимальный порядок матриц:", 2, int.MaxValue);
        Dictionary<int, List<Matrix>> D = DataBase.CreateMatrixDict(n, s + 1);
        DataBase.DisplayMatrixDict(D);
    }
}