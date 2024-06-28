using System;

class Matrix {
    
    private int[] body;
    private int width, height;

    /*
     * Создание квадратной матрицы size x size,
     * заполненной случайными числами
     */
    public Matrix(int size) {
        this.width = size;
        this.height = size;
        this.body = new int[size * size];
        // Закомментируйте код ниже если не требуется заполнение матрицы
        for (int i = 0; i < width * height; i++)
            body[i] = Rand.rnd.Next(-2, 3);
    }

    /*
     * Создание матрицы height x width,
     * заполненной случайными числами
     */
    public Matrix(int height, int width) {
        this.width = width;
        this.height = height;
        this.body = new int[width * height];
        // Закомментируйте код ниже если не требуется заполнение матрицы
        for (int i = 0; i < width * height; i++)
            body[i] = Rand.rnd.Next(-2, 3);
    }

    /*
     * Создание квадратной матрицы size x size,
     * заполненной числами из буфера
     * Размер буфера должен быть не меньше size * size
     */
    public Matrix(int size, int[] buffer) {
        this.width = size;
        this.height = size;
        this.body = buffer;
    }

    /*
     * Создание матрицы height x width,
     * заполненной числами из буфера
     * Размер буфера должен быть не меньше height x width
     */
    public Matrix(int height, int width, int[] buffer) {
        this.width = width;
        this.height = height;
        this.body = buffer;
    }

    public Matrix(Matrix A) {
        this.width = A.width;
        this.height = A.height;
        this.body = new int[width * height];
        for (int i = 0; i < width * height; i++)
            body[i] = A.body[i];
    }

    public int W{
        get { return width; }
    }

    public int H{
        get { return height; }
    }

    public int this[int i, int j] {
        get { return body[i * width + j]; }
        set { body[i * width + j] = value; }
    }

    public override string ToString() {
        string result = "";
        int i, j;
        for (i = 0; i < height; i++) {
            for (j = 0; j < width; j++) {
                result += $" {body[i * width + j], 5}";
            }
            result += "\n";
        }
        return result;
    }

    /*
     * Извлечение дополнительного минора
     * Возвращает матрицу, значения которой совпадают со значениями
     * текущей матрицы, но в которой отсутствуют i строка и j столбец
     * DEBUG: в случае, если i или j выходят из допустимого диапазона,
     * будет получена ошибка времени выполнения
     */

    public Matrix additionalMinor(int i, int j) {

        int[] buffer = new int[(height - 1) * (width - 1)];
        int z, offset;
        offset = 0;
        for (z = 0; z < height * width; z++) {
            if ((z / width == i) || (z % width == j)) {
                offset += 1;
            } else {
                buffer[z - offset] = body[z];
            }
        }
        return new Matrix(height - 1, width - 1, buffer);
    }

    /*
     * Вычисление детерминанта матрицы методом разложения по строке
     * DEBUG: Если применено к неквадратной матрице, будет вычислен
     * детерминант наибольшей квадратной подматрицы, содержащейся в
     * текущей матрице
     */

    public int det() {
        
        int i, result;
        result = 0;
        // Базовый случай
        if (width == 1 || height == 1) {
            result = body[0];
        } else {
            // Разложение по первой строке
            for (i = 0; i < width; i++) {
                if (i % 2 == 0) {
                    result += body[i] * this.additionalMinor(0, i).det();
                } else {
                    result -= body[i] * this.additionalMinor(0, i).det();
                }
            }
        }
        return result;
    }

    /* 
     * Совмещение матриц
     * Перезаписать значения текущей матрицы значениями из матрицы A
     * с заданным смещением
     * Матрица A должна иметь размеры, не превышающие размеры текущей матрицы
     */

    public Matrix merge(Matrix A, int i, int j) {

        Matrix R = new Matrix(this);
        int u, v;

        for (u = 0; u < A.height; u++) {
            for (v = 0; v < A.width; v++) {
                R.body[(u + i) * R.width + (v + j)] = A.body[u * A.width + v];
            }
        }

        return R;
    }

    /*
     * DEBUG: Оператор сложения работает для матриц, содержащих
     * одинаковое число элементов, но, возможно, имеющих
     * разные размеры
     */

    public static Matrix operator +(Matrix A, Matrix B) {

        int[] buffer = new int[A.height * A.width];
        int i;
        for (i = 0; i < A.height * A.width; i++) {
            buffer[i] = A.body[i] + B.body[i];
        }
        return new Matrix(A.height, A.width, buffer);
    }

    public static Matrix operator -(Matrix A) {

        int[] buffer = new int[A.height * A.width];
        int i;
        for (i = 0; i < A.height * A.width; i++) {
            buffer[i] = -A.body[i];
        }
        return new Matrix(A.height, A.width, buffer);
    }

    public static Matrix operator -(Matrix A, Matrix B) {

        int[] buffer = new int[A.height * A.width];
        int i;
        for (i = 0; i < A.height * A.width; i++) {
            buffer[i] = A.body[i] - B.body[i];
        }
        return new Matrix(A.height, A.width, buffer);
    }

    public static Matrix operator *(Matrix A, int k) {

        int[] buffer = new int[A.height * A.width];
        int i;
        for (i = 0; i < A.height * A.width; i++) {
            buffer[i] = A.body[i] * k;
        }
        return new Matrix(A.height, A.width, buffer);
    }

    public static Matrix operator *(int k, Matrix A) {
        return A * k;
    }

    public static Matrix operator *(Matrix A, Matrix B) {

        int[] buffer = new int[A.height * B.width];
        int i, j, z;
        for (i = 0; i < A.height; i++) {
            for (j = 0; j < B.width; j++) {
                for (z = 0; z < A.width; z++) {
                    buffer[i * B.width + j] += A.body[i * A.width + z] * B.body[z * B.width + j];
                }
            }
        }
        return new Matrix(A.height, B.width, buffer);
    }

    public static Matrix operator !(Matrix A) {

        int[] buffer = new int[A.width * A.height];
        int i, j;
        for (i = 0; i < A.height; i++) {
            for (j = 0; j < A.width; j++) {
                buffer[j * A.height + i] = A.body[i * A.width + j];
            }
        }
        return new Matrix(A.width, A.height, buffer);
    }

    public static explicit operator int(Matrix A) {
        return A.det();
    }

    public static bool operator <(Matrix A, Matrix B) {
        return A.det() < B.det();
    }

    public static bool operator >(Matrix A, Matrix B) {
        return A.det() > B.det();
    }

    public static bool operator ==(Matrix A, Matrix B) {
        return A.det() == B.det();
    }

    public static bool operator !=(Matrix A, Matrix B) {
        return A.det() != B.det();
    }

    public static bool operator <=(Matrix A, Matrix B) {
        return A.det() <= B.det();
    }

    public static bool operator >=(Matrix A, Matrix B) {
        return A.det() >= B.det();
    }
}