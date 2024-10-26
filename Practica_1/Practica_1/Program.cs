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
            Console.Write("Введите строку для записи в файл: ");
            string userInput = Console.ReadLine();
            string filePath = "example.txt";
            CreateAndManageFile(filePath, userInput);

            // 3. Работа с JSON
            string jsonFilePath = "example.json";
            ManageJsonFile(jsonFilePath);

            // 4. Работа с XML
            string xmlFilePath = "example.xml";
            ManageXmlFile(xmlFilePath);

            // 5. Создание ZIP-архива
            string zipPath = "archive.zip";
            CreateAndManageZip(zipPath, filePath);

            // Удаление исходного файла после создания архива
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Console.WriteLine($"Файл '{filePath}' удален.");
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

        // 2. Работа с файлами
        static void CreateAndManageFile(string path, string content)
        {
            File.WriteAllText(path, content);
            Console.WriteLine($"Файл '{path}' создан и записан.");

            string readContent = File.ReadAllText(path);
            Console.WriteLine("Содержимое файла:");
            Console.WriteLine(readContent);
        }

        // 3. Работа с JSON
        static void ManageJsonFile(string path)
        {
            var obj = new { Name = "Иван", Age = 30, Occupation = "Программист" };
            string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            File.WriteAllText(path, json);
            Console.WriteLine($"JSON-файл '{path}' создан и записан.");

            string jsonContent = File.ReadAllText(path);
            Console.WriteLine("Содержимое JSON-файла:");
            Console.WriteLine(jsonContent);

            File.Delete(path);
            Console.WriteLine($"JSON-файл '{path}' удален.");
        }

        // 4. Работа с XML
        static void ManageXmlFile(string path)
        {
            var xml = new XElement("Person",
                new XElement("Name", "Иван"),
                new XElement("Age", 30),
                new XElement("Occupation", "Программист")
            );
            xml.Save(path);
            Console.WriteLine($"XML-файл '{path}' создан и записан.");

            string xmlContent = File.ReadAllText(path);
            Console.WriteLine("Содержимое XML-файла:");
            Console.WriteLine(xmlContent);

            File.Delete(path);
            Console.WriteLine($"XML-файл '{path}' удален.");
        }

        // 5. Работа с ZIP-архивом
        static void CreateAndManageZip(string zipPath, string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Файл '{filePath}' не существует.");
                return;
            }

            // Создаем архив
            using (FileStream zipStream = new FileStream(zipPath, FileMode.Create))
            using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
            {
                archive.CreateEntryFromFile(filePath, Path.GetFileName(filePath));
                Console.WriteLine($"Файл '{filePath}' добавлен в архив '{zipPath}'.");
            }

            // Чтение содержимого архива
            Console.WriteLine("Содержимое архива:");
            using (ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    Console.WriteLine($"Файл: {entry.FullName}, Размер: {entry.Length} байт");
                }
            }

            // Удаление архива
            File.Delete(zipPath);
            Console.WriteLine($"Архив '{zipPath}' удален.");
        }
    }
}
