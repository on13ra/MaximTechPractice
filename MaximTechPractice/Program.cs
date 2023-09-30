START:
Console.WriteLine("Введите Cтроку на английском языке в нижнем регистре");
string line = Console.ReadLine();
if (line == null || line.Trim() == "") goto START;
bool result = AlphabetCheck(line);
if (result == false) goto START;
int length = line.Length;
char[] parts = line.ToCharArray();
if (line.Length % 2 == 0)
{
    Array.Reverse(parts, length / 2, length / 2);
    string output = new string(parts);
    Console.WriteLine("Вывод: {0}", output);
}
else
{
    string part1 = new string(parts);
    Array.Reverse(parts);
    string part2 = new string(parts);
    string output = part1 + part2;
    Console.WriteLine("Вывод: {0}",output);
}
Console.ReadKey();

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