using Newtonsoft.Json;
using System;
using System.ComponentModel.Design;
using System.Linq;

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
    Console.WriteLine("  Вывод: {0}", output);
}
else
{
    string part1 = new string(parts);
    Array.Reverse(parts);
    string part2 = new string(parts);
    output = part1 + part2;
    Console.WriteLine("  Вывод: {0}", output);
}

#region Задание 3 
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
   Console.WriteLine    ();
#endregion Задание 3 

#region Задание 4
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
for (int i = 0; (i < substr.Length); i++)
    if (target.Contains(substr[i]))
    { Console.WriteLine("\n Наибольшая подстрока на гласную: {0}", substr); break; }
    else { Console.WriteLine("Строка не содержит гласных"); break; }
#endregion Задание 4

#region объявление сортировка 5
    //  метод дерева
char[] outputArray = output.ToCharArray();
TreeSort(outputArray, 0, outputArray.Length - 1);
string output1 = new string(outputArray);

Console.WriteLine(" Отсортированный вывод: {0}", output1);

//  быстрая сортировка
QuickSort(outputArray, 0, outputArray.Length - 1);
string output2 = new string(outputArray);

Console.WriteLine(" Отсортированный вывод (быстрая сортировка): {0}", output2);
#endregion объявление сортировка 5

#region задание 6 объяв
Console.WriteLine();
int takennum = await TAKE();
string remchar = output.Remove(takennum - 1, 1);
Console.WriteLine("  Обработанная строка без одного символа: {0}", remchar);
Console.WriteLine();
#endregion задание 6 объяв

#region Задание 2 - Проверка ввода
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
#endregion Задание 2 - Проверка ввода

#region Сортировка 5
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
#endregion Сортировка 5


//Задание 6 
 async Task<int> TAKE()
{
    string apiKey = "5fcfba1d-6d32-455e-a680-adc094214949";
    int n = 1; // количество чисел
    int min = 1; // от
    int max = output.Length; // до

    // запрос жсон-рпц
    var request = new
    {
        jsonrpc = "2.0",
        method = "generateIntegers",
        @params = new
        {
            apiKey,
            n,
            min,
            max
        },
        id = 42 // уникальный ид
    };

    string requestBody = JsonConvert.SerializeObject(request);
    try
    {
        using (var client = new HttpClient())
        {
            Random random = new Random();

            string apiUrl = "https://api.random.org/json-rpc/4/invoke";

            var content = new StringContent(requestBody, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();

                var jsonResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);

                var randomIntegers = jsonResponse.result.random.data[0].ToObject<int>();

                Console.WriteLine("  Random Integers: " + string.Join(", ", randomIntegers));
                return randomIntegers;
            }
            else
            {
                // тут отправляется число сгенерированное на клиенте
                Console.WriteLine("Ошибка подключения: " + response.StatusCode);
                return random.Next(min, max);
            }

        }
    } 
    catch (Exception ex) 
    {
        //  и тут отправляется число сгенерированное на клиенте
        Random random = new Random();
        return random.Next(min, max); 
    }


}


















