namespace Medisys.UI;

public static class Menu
{
    public static int Show(string title, params string[] options)
    {
        while (true)
        {
            Console.Clear();

            string border = new('=', Console.WindowWidth - 1);

            Console.WriteLine(border);

            Console.WriteLine(title.PadLeft((Console.WindowWidth + title.Length) / 2));

            Console.WriteLine(border);

            Console.WriteLine();

            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }

            Console.Write("\nChoice: ");

            string? input = Console.ReadLine();

            if (int.TryParse(input, out int choice))
            {
                if (choice > 0 && choice <= options.Length)
                {
                    return choice;
                }
            }

            Console.WriteLine("\nInvalid choice.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
