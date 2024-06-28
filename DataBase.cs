using System;
using System.IO;
using System.Collections.Generic;

[Serializable]
enum Genre {
    Comedy, Drama, Tragedy, Ode, Poem, Novel, FairyTale, Story, Total
}

[Serializable]
struct Field {
    public int key;
    public string title;
    public string author;
    public int year;
    public Genre genre;

    public override string ToString() {
        return $"{key, 4} {title, 30} {author, 20} {year, 4} {genre, 10}";
    }

    public static string Header {
        get {
            return $"Ключ {"Название", 30} {"Автор", 20} {"Год", 4} {"Жанр", 10}";
        }
    }
}

class DataBase {

    public static Genre ParseGenre(string s) {

        Genre genre = Genre.Total;
        if (s == "Comedy" || s == "Комедия") genre = Genre.Comedy;
        else
        if (s == "Drama" || s == "Драма") genre = Genre.Drama;
        else
        if (s == "Tragedy" || s == "Трагедия") genre = Genre.Tragedy;
        else
        if (s == "Ode" || s == "Ода") genre = Genre.Ode;
        else
        if (s == "Poem" || s == "Поэма") genre = Genre.Poem;
        else
        if (s == "Novel" || s == "Роман") genre = Genre.Novel;
        else
        if (s == "FairyTale" || s == "Сказка") genre = Genre.FairyTale;
        else
        if (s == "Story" || s == "Рассказ" || s == "Повесть") genre = Genre.Story;
        return genre;
    }

    public static List<Field> ReadData(string path) {

        List<Field> result = new List<Field>();
        StreamReader r = new StreamReader(path);
        Field T;
        string[] line;
        int i = 0;

        while (!r.EndOfStream) {
            line = r.ReadLine().Split(';');
            T.key = i;
            T.title = line[0];
            T.author = line[1];
            T.year = int.Parse(line[2]);
            T.genre = ParseGenre(line[3]);
            result.Add(T);
            i++;
        }

        r.Close();
        return result;
    }

    public static void DisplayData<T>(IEnumerable<T> X) {

        foreach (T x in X) {
            Console.WriteLine(x);
        }
        Console.WriteLine();
    }

    public static Dictionary<int, List<Matrix>> CreateMatrixDict(int n, int s) {

        Dictionary<int, List<Matrix>> result = new Dictionary<int, List<Matrix>>();
        Matrix M;
        int size, key;

        for (int i = 0; i < n; i++) {
            size = Rand.rnd.Next(2, s);
            M = new Matrix(size);
            key = M.det();
            if (!result.ContainsKey(key)) {
                result.Add(key, new List<Matrix>());
            }
            result[key].Add(M);
        }

        return result;
    }

    public static void DisplayMatrixDict(Dictionary<int, List<Matrix>> X) {

        foreach (KeyValuePair<int, List<Matrix>> T in X) {
            Console.WriteLine($"-------------------- det = {T.Key} --------------------");
            foreach (Matrix M in T.Value) {
                Console.WriteLine(M);
            }
        }
    }
}