START:
Console.WriteLine("   Введите Cтроку на английском языке в нижнем регистре");
Console.Write("   ");
string line = Console.ReadLine();
if (line == null || line.Trim() == "") goto START;
bool result = AlphabetCheck(line);
if (result == false) goto START;
int length = line.Length;
char[] parts = line.ToCharArray();
string output;
if (line.Length % 2 == 0)
{
    Array.Reverse(parts, length / 2, length / 2);
    output = new string(parts);
    Console.WriteLine(" Вывод: {0}", output);
}
else
{
    string part1 = new string(parts);
    Array.Reverse(parts);
    string part2 = new string(parts);
    output = part1 + part2;
    Console.WriteLine(" Вывод: {0}", output);
}
//  метод дерева
char[] outputArray = output.ToCharArray();
TreeSort(outputArray, 0, outputArray.Length - 1);
output = new string(outputArray);

Console.WriteLine(" Отсортированный вывод: {0}", output);

//  быстрая сортировка
QuickSort(outputArray, 0, outputArray.Length - 1);
output = new string(outputArray);

Console.WriteLine(" Отсортированный вывод (быстрая сортировка): {0}", output);

//Задание 3 - Помимо обработанной строки, необходимо также возвращать пользователю информацию о том, сколько раз повторялся каждый символ в обработанной строке. 
var charCounts = output
           .GroupBy(c => c)
           .Select(g => new
           {
               Character = g.Key,
               Count = g.Count()
           })
           .Where(x => x.Count > 1);

if (charCounts.Any())
{
    Console.WriteLine("  Повторяющиеся символы и их количество:");
    foreach (var charCount in charCounts)
    {
        Console.Write($"  {charCount.Character}: {charCount.Count} раз\t");
    }
}

// Задание 4 - Необходимо найти в обработанной строке наибольшую подстроку, которая начинается и заканчивается на гласную букву из «aeiouy»
char[] target = "aeiouy".ToCharArray();
char[] rebuild2 = output.ToCharArray();
char[] rebuild2Rev = output.ToCharArray();
Array.Reverse(rebuild2Rev);
int first = 0;
int last = 0;
for (int i = 0; i < rebuild2.Length; i++)
    if (target.Contains(rebuild2[i]))
    {
        first += i;
        break;
    }
for (int i = 0; i < rebuild2.Length; i++)
    if (target.Contains(rebuild2Rev[i]))
    {
        last += i;
        break;
    }
string substr = output.Substring(first);
substr = substr.Substring(0, substr.Length - last);
Console.WriteLine("\n Наибольшая подстрока на гласную: {0}", substr);
Console.ReadKey();





//Задание 2 - Проверка ввода
static bool AlphabetCheck(string input)
{
    string alphabet = "abcdefghijklmnopqrstuvwxyz ";
    input = input.ToLower();
    char[] word = input.ToCharArray();
    string issues = "";

    for (int j = 0; j < word.Length; j++)
    {
        string tocheck = word[j].ToString();

        if (alphabet.Contains(tocheck))
        {
            continue;
        }
        else if (!alphabet.Contains(tocheck))
        {
            issues += word[j].ToString();
        }
    }

    if (issues != "")
    {
        Console.WriteLine("Неподходящие значения: {0}", issues);
        return false;
    }
    return true;
}

//Задание 5 - Сортировка
static void TreeSort(char[] arr, int low, int high)
{
    if (low < high)
    {
        int pivotIndex = Partition(arr, low, high);

        // Рекурсивно сортируем элементы перед и после разделителя
        TreeSort(arr, low, pivotIndex - 1);
        TreeSort(arr, pivotIndex + 1, high);
    }
}

// Метод для разделения массива и выбора опорного элемента
static int Partition(char[] arr, int low, int high)
{
    char pivot = arr[high];
    int i = (low - 1);

    for (int j = low; j <= high - 1; j++)
        if (arr[j] < pivot)
        {
            i++;
            // Меняем элементы местами
            char temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }


    char temp1 = arr[i + 1];
    arr[i + 1] = arr[high];
    arr[high] = temp1;

    return (i + 1);
}
// Метод быстрой сортировки
static void QuickSort(char[] arr, int low, int high)
{
    if (low < high)
    {
        int pivotIndex = PartitionQuick(arr, low, high);

        QuickSort(arr, low, pivotIndex - 1);
        QuickSort(arr, pivotIndex + 1, high);
    }
}

//разделение массива и выбор опорного элемента
static int PartitionQuick(char[] arr, int low, int high)
{
    char pivot = arr[high];
    int i = (low - 1);

    for (int j = low; j <= high - 1; j++)
        if (arr[j] < pivot)
        {
            i++;
            char temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

    char temp1 = arr[i + 1];
    arr[i + 1] = arr[high];
    arr[high] = temp1;

    return (i + 1);
}

