// 1
//Создать приложение, которое на вход будет получать строку.
//Если строка будет иметь чётное количество символов, то программа должна разделить её на две подстроки, каждую подстроку перевернуть и соединять обратно обе подстроки в одну строку.
//Если входная строка будет иметь нечётное количество символов, то программа должна перевернуть эту строку и к ней добавить изначальную строку.


START:
Console.WriteLine("Введите Cтроку");
string line = Console.ReadLine();
if (line == null || line.Trim() == "") goto START;
int length = line.Length;
char[] parts = line.ToCharArray();
if (line.Length % 2 == 0)
{
    Array.Reverse(parts, length / 2, length / 2);
    string output = new string(parts);
    Console.WriteLine(output);
}
else
{
    string part1 = new string(parts);
    Array.Reverse(parts);
    string part2 = new string(parts);
    string output = part1 + part2;
    Console.WriteLine(output);
}
Console.ReadKey();
