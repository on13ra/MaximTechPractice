using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAPI;

[Route("api/processing")]
[ApiController]
public class ProcessingController : ControllerBase
{
    private readonly RequestLimiter _requestLimiter;
    private readonly IConfiguration _configuration;

    public ProcessingController(RequestLimiter requestLimiter, IConfiguration configuration)
    {
        _requestLimiter = requestLimiter;
        _configuration = configuration;
    }
    [HttpGet]
    public async Task<IActionResult> ProcessString(string line)
    {
        // Попытка захвата разрешения для обработки запроса
        if (await _requestLimiter.TryAcquireAsync())
        {
            try
            {
                // Ваша обработка запроса
                if (string.IsNullOrWhiteSpace(line))
                {
                    return BadRequest("Введите Строку.");
                }

                bool result = AlphabetCheck(line);
                if (!result)
                {
                    return BadRequest("Введите строку на английском языке.");
                }

            START:
                Console.WriteLine("Введите строку на английском языке в нижнем регистре");
                Console.Write(" ");

                if (line == null || line.Trim() == "") goto START;

                if (result == false) goto START;

                int length = line.Length;
                char[] parts = line.ToCharArray();
                string output;
                if (line.Length % 2 == 0)
                {
                    Array.Reverse(parts, length / 2, length / 2);
                    output = new string(parts);
                    Console.WriteLine("Вывод: {0}", output);
                }
                else
                {
                    string part1 = new string(parts);
                    Array.Reverse(parts);
                    string part2 = new string(parts);
                    output = part1 + part2;
                    Console.WriteLine("Вывод: {0}", output);
                }

                // Задание 3 - Подсчет повторяющихся символов
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
                    Console.WriteLine("Повторяющиеся символы и их количество:");
                    foreach (var charCount in charCounts)
                    {
                        Console.Write($"{charCount.Character}: {charCount.Count} раз\t");
                    }
                    Console.WriteLine();
                }

                // Задание 4 - Наибольшая подстрока с гласными
                char[] target = "aeiouy".ToCharArray();
                char[] rebuild2 = output.ToCharArray();
                char[] rebuild2Rev = output.ToCharArray();
                Array.Reverse(rebuild2Rev);
                int first = 0;
                int last = 0;
                for (int i = 0; i < rebuild2.Length; i++)
                {
                    if (target.Contains(rebuild2[i]))
                    {
                        first += i;
                        break;
                    }
                }
                for (int i = 0; i < rebuild2.Length; i++)
                {
                    if (target.Contains(rebuild2Rev[i]))
                    {
                        last += i;
                        break;
                    }
                }
                string substr = output.Substring(first);
                substr = substr.Substring(0, substr.Length - last);
                for (int i = 0; i < substr.Length; i++)
                {
                    if (target.Contains(substr[i]))
                    {
                        Console.WriteLine("\nНаибольшая подстрока на гласную: {0}", substr);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Строка не содержит гласных");
                        substr = "undefined";
                        break;
                    }
                }

                // Задание 5 - Сортировка
                char[] outputArray = output.ToCharArray();
                TreeSort(outputArray, 0, outputArray.Length - 1);
                string output1 = new string(outputArray);
                Console.WriteLine("Отсортированный вывод: {0}", output1);

                // Быстрая сортировка
                QuickSort(outputArray, 0, outputArray.Length - 1);
                string output2 = new string(outputArray);
                Console.WriteLine("Отсортированный вывод (быстрая сортировка): {0}", output2);

                // Задание 6 - Обработка и возврат строки без одного символа
                string remchar = await TAKE(output);
                Console.WriteLine();
                Console.WriteLine("Обработанная строка без одного символа: {0}", remchar);
                Console.WriteLine();

                return Ok(new
                {
                    Вывод = output,
                    Повторяющиеся_символы = charCounts,
                    Наибольшая_подстрока_на_гласную = substr,
                    Сортировка = new
                    {
                        Деревом = output1,
                        Быстрая = output2
                    },
                    Строка_без_одного_символа_API = remchar
                });
            }
            finally
            {
                // Важно освободить разрешение после обработки запроса
                _requestLimiter.Release();
            }
        }
        else
        {
            // HTTP 503 Service Unavailable, так как достигнут лимит параллельных запросов
            return StatusCode(503, "Service Unavailable");
        }
    }


    // Задание 2 - Проверка ввода
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

    // Задание 5 - Сортировка
    static void TreeSort(char[] arr, int low, int high)
    {
        if (low < high)
        {
            int pivotIndex = Partition(arr,
            low, high);

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
        {
            if (arr[j] < pivot)
            {
                i++;
                // Меняем элементы местами
                char temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
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

    // Разделение массива и выбор опорного элемента
    static int PartitionQuick(char[] arr, int low, int high)
    {
        char pivot = arr[high];
        int i = (low - 1);

        for (int j = low; j <= high - 1; j++)
        {
            if (arr[j] < pivot)
            {
                i++;
                char temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }

        char temp1 = arr[i + 1];
        arr[i + 1] = arr[high];
        arr[high] = temp1;

        return (i + 1);
    }
    [HttpPost]
    public async Task<string> TAKE(string output)
    {
        string apiKey = "5fcfba1d-6d32-455e-a680-adc094214949";
        int n = 1; // количество чисел
        int min = 1; // от
        int max = output.Length; // до

        // Запрос JSON-RPC
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

                    Console.WriteLine("Random Integers: " + string.Join(", ", randomIntegers));
                    string remchar = output.Remove(randomIntegers - 1, 1);
                    return remchar;
                }
                else
                {
                    // Ошибка подключения, отправляем случайное число
                    Console.WriteLine("Ошибка подключения: " + response.StatusCode);
                    return random.Next(min, max).ToString();
                }
            }
        }
        catch (Exception ex)
        {
            // Ошибка, отправляем случайное число
            Random random = new Random();
            return random.Next(min, max).ToString();
        }
    }
}