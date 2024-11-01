using System;
using System.IO;
using System.IO.Compression;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Вывод информации о логических дисках
            DisplayDriveInfo();

            // 2. Работа с файлами
            Console.Write("Введите строку для записи в текстовый файл: ");
            string textContent = Console.ReadLine();
            CreateAndManageFile("example.txt", textContent);

            // 3. Работа с JSON
            Console.Write("Введите имя для JSON: ");
            string jsonName = Console.ReadLine();
            Console.Write("Введите возраст для JSON: ");
            int jsonAge = int.Parse(Console.ReadLine());
            Console.Write("Введите профессию для JSON: ");
            string jsonOccupation = Console.ReadLine();
            ManageJsonFile("example.json", jsonName, jsonAge, jsonOccupation);

            // 4. Работа с XML
            Console.Write("Введите имя для XML: ");
            string xmlName = Console.ReadLine();
            Console.Write("Введите возраст для XML: ");
            int xmlAge = int.Parse(Console.ReadLine());
            Console.Write("Введите профессию для XML: ");
            string xmlOccupation = Console.ReadLine();
            ManageXmlFile("example.xml", xmlName, xmlAge, xmlOccupation);

            // 5. Создание ZIP-архива
            CreateAndManageZip("archive.zip", "example.txt");

            // Удаление исходного текстового файла после архивации
            if (File.Exists("example.txt"))
            {
                File.Delete("example.txt");
                Console.WriteLine("Текстовый файл удалён.");
            }
        }

        // 1. Информация о дисках
        static void DisplayDriveInfo()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                Console.WriteLine($"Название: {drive.Name}");
                Console.WriteLine($"Тип: {drive.DriveType}");
                if (drive.IsReady)
                {
                    Console.WriteLine($"Объем: {drive.TotalSize / (1024 * 1024 * 1024)} ГБ");
                    Console.WriteLine($"Свободное место: {drive.TotalFreeSpace / (1024 * 1024 * 1024)} ГБ");
                    Console.WriteLine($"Метка тома: {drive.VolumeLabel}");
                    Console.WriteLine($"Файловая система: {drive.DriveFormat}");
                }
                Console.WriteLine();
            }
        }

        // 2. Работа с текстовым файлом
        static void CreateAndManageFile(string path, string content)
        {
            File.WriteAllText(path, content);
            Console.WriteLine($"Файл '{path}' создан и записан.");

            string readContent = File.ReadAllText(path);
            Console.WriteLine("Содержимое файла:");
            Console.WriteLine(readContent);
        }

        // 3. Работа с JSON
        static void ManageJsonFile(string path, string name, int age, string occupation)
        {
            var obj = new { Name = name, Age = age, Occupation = occupation };
            string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            File.WriteAllText(path, json);
            Console.WriteLine($"JSON-файл '{path}' создан и записан.");

            string jsonContent = File.ReadAllText(path);
            Console.WriteLine("Содержимое JSON-файла:");
            Console.WriteLine(jsonContent);

            File.Delete(path);
            Console.WriteLine($"JSON-файл '{path}' удалён.");
        }

        // 4. Работа с XML
        static void ManageXmlFile(string path, string name, int age, string occupation)
        {
            var xml = new XElement("Person",
                new XElement("Name", name),
                new XElement("Age", age),
                new XElement("Occupation", occupation)
            );
            xml.Save(path);
            Console.WriteLine($"XML-файл '{path}' создан и записан.");

            string xmlContent = File.ReadAllText(path);
            Console.WriteLine("Содержимое XML-файла:");
            Console.WriteLine(xmlContent);

            File.Delete(path);
            Console.WriteLine($"XML-файл '{path}' удалён.");
        }

        // 5. Работа с ZIP-архивом
        static void CreateAndManageZip(string zipPath, string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Файл '{filePath}' не существует.");
                return;
            }

            using (FileStream zipStream = new FileStream(zipPath, FileMode.Create))
            using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
            {
                archive.CreateEntryFromFile(filePath, Path.GetFileName(filePath));
                Console.WriteLine($"Файл '{filePath}' добавлен в архив '{zipPath}'.");
            }

            Console.WriteLine("Содержимое архива:");
            using (ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    Console.WriteLine($"Файл: {entry.FullName}, Размер: {entry.Length} байт");
                }
            }

            File.Delete(zipPath);
            Console.WriteLine($"Архив '{zipPath}' удалён.");
        }
    }
}
