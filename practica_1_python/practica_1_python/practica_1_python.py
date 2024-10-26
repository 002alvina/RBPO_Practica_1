import os
import json
import zipfile
import shutil
import xml.etree.ElementTree as ET
from pathlib import Path

def display_drive_info():
    """Вывод информации о доступных дисках."""
    drives = [f"{d}:\\" for d in 'ABCDEFGHIJKLMNOPQRSTUVWXYZ' if os.path.exists(f"{d}:\\")]
    for drive in drives:
        total, used, free = shutil.disk_usage(drive)
        print(f"Название: {drive}")
        print(f"Объем: {total // (1024 ** 3)} ГБ")
        print(f"Свободное место: {free // (1024 ** 3)} ГБ\n")

def create_and_manage_file(path, content):
    """Создание, чтение и удаление текстового файла."""
    with open(path, 'w', encoding='utf-8') as f:
        f.write(content)
    print(f"Файл '{path}' создан и записан.")

    with open(path, 'r', encoding='utf-8') as f:
        print("Содержимое файла:")
        print(f.read())

    os.remove(path)
    print(f"Файл '{path}' удален.")

def manage_json_file(path):
    """Создание, чтение и удаление JSON-файла."""
    obj = {"Name": "Иван", "Age": 30, "Occupation": "Программист"}
    with open(path, 'w', encoding='utf-8') as f:
        json.dump(obj, f, indent=4, ensure_ascii=False)
    print(f"JSON-файл '{path}' создан и записан.")

    with open(path, 'r', encoding='utf-8') as f:
        print("Содержимое JSON-файла:")
        print(f.read())

    os.remove(path)
    print(f"JSON-файл '{path}' удален.")

def manage_xml_file(path):
    """Создание, чтение и удаление XML-файла."""
    root = ET.Element("Person")
    ET.SubElement(root, "Name").text = "Иван"
    ET.SubElement(root, "Age").text = "30"
    ET.SubElement(root, "Occupation").text = "Программист"
    tree = ET.ElementTree(root)
    tree.write(path, encoding='utf-8', xml_declaration=True)
    print(f"XML-файл '{path}' создан и записан.")

    with open(path, 'r', encoding='utf-8') as f:
        print("Содержимое XML-файла:")
        print(f.read())

    os.remove(path)
    print(f"XML-файл '{path}' удален.")

def create_and_manage_zip(zip_path, file_path):
    """Создание ZIP-архива, чтение содержимого и удаление архива."""
    if not os.path.exists(file_path):
        print(f"Файл '{file_path}' не существует.")
        return

    with zipfile.ZipFile(zip_path, 'w') as zipf:
        zipf.write(file_path, os.path.basename(file_path))
    print(f"Файл '{file_path}' добавлен в архив '{zip_path}'.")

    print("Содержимое архива:")
    with zipfile.ZipFile(zip_path, 'r') as zipf:
        for info in zipf.infolist():
            print(f"Файл: {info.filename}, Размер: {info.file_size} байт")

    os.remove(zip_path)
    print(f"Архив '{zip_path}' удален.")

if __name__ == "__main__":
    # 1. Вывод информации о логических дисках
    display_drive_info()

    # 2. Работа с текстовыми файлами
    user_input = input("Введите строку для записи в файл: ")
    file_path = "example.txt"
    create_and_manage_file(file_path, user_input)

    # 3. Работа с JSON
    json_file_path = "example.json"
    manage_json_file(json_file_path)

    # 4. Работа с XML
    xml_file_path = "example.xml"
    manage_xml_file(xml_file_path)

    # 5. Создание ZIP-архива
    zip_path = "archive.zip"
    create_and_manage_zip(zip_path, file_path)

