using AESApplication;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

internal class Program
{
    private const string DataPatch = "Data.txt";

    static async Task Main()
    {
        //var test = new AESData() {IV = IV , Key = Key };

        //using StreamReader sr = new("Data.txt");

        //var m = JsonConvert.DeserializeObject<AESData>(await sr.ReadToEndAsync());

        //string json = JsonConvert.SerializeObject(test);


        while (true) 
        {
            Console.WriteLine("Choose what to do");
            Console.WriteLine("[1]:Generate new Key and IV");
            Console.WriteLine("[2]:AES encrypt");
            Console.WriteLine("[3]:AES decrypt string");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Specify selection number like: 1");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Eneter number:");
            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case (1): { CreateNewAESData(); } break;
                case (2): { await AESEncrypt(); } break;
            }

        }

        await Task.Delay(1000);
    }

    public static void CreateNewAESData() 
    {
        AESData result = new();
        Aes aes = Aes.Create();
        aes.GenerateIV();
        aes.GenerateKey();

        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Yore new AES Key is:[{0}]", String.Join(",", aes.Key));
        Console.WriteLine("Yore new AES Key length is:[{0}]", aes.Key.Length);
        Console.WriteLine("Yore new AES Key Text is:[{0}]", Encoding.Default.GetString(aes.Key));

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("Yore new AES IV is:[{0}]", String.Join(",", aes.IV));
        Console.WriteLine("Yore new AES IV length is:[{0}]", aes.IV.Length);
        Console.WriteLine("Yore new AES IV Text is:[{0}]", Encoding.Default.GetString(aes.IV));

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Enter to continue");

        Console.ReadKey();
        Console.Clear();
    }

    public static async Task AESEncrypt()
    {
        Console.Write("Enter text:");
        var text = Console.ReadLine();
        var aesData = await  GetAESData();
        var result = AESCryptor.Encrypt(text, aesData.Key, aesData.IV);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Yore new text byte is:[{0}]", String.Join(",", result));
        Console.WriteLine("Yore new text byte length is:[{0}]", result.Length);
        Console.WriteLine("Yore new text is:[{0}]", Encoding.Default.GetString(result));

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Enter to continue");

        Console.ReadKey();
        Console.Clear();
    }

    public static async Task<AESData> GetAESData() 
    {
        using StreamReader sr = new("Data.txt");
        var result = JsonConvert.DeserializeObject<AESData>(await sr.ReadToEndAsync());

        return result;
    }


}