import os
import json
import zipfile
import shutil
import xml.etree.ElementTree as ET

def main():
    # 1. Вывод информации о логических дисках
    display_drive_info()

    # 2. Работа с текстовым файлом
    text_content = input("Введите строку для записи в текстовый файл: ")
    create_and_manage_file("example.txt", text_content)

    # 3. Работа с JSON
    json_name = input("Введите имя для JSON: ")
    json_age = int(input("Введите возраст для JSON: "))
    json_occupation = input("Введите профессию для JSON: ")
    manage_json_file("example.json", json_name, json_age, json_occupation)

    # 4. Работа с XML
    xml_name = input("Введите имя для XML: ")
    xml_age = int(input("Введите возраст для XML: "))
    xml_occupation = input("Введите профессию для XML: ")
    manage_xml_file("example.xml", xml_name, xml_age, xml_occupation)

    # 5. Создание ZIP-архива
    create_and_manage_zip("archive.zip", "example.txt")

    # Удаление исходного файла после архивации
    if os.path.exists("example.txt"):
        os.remove("example.txt")
        print("Текстовый файл удалён.")

# 1. Информация о дисках
def display_drive_info():
    partitions = shutil.disk_usage("/")
    print("Информация о корневом разделе:")
    print(f"  Объем: {partitions.total / (1024 ** 3):.2f} ГБ")
    print(f"  Свободное место: {partitions.free / (1024 ** 3):.2f} ГБ")
    print()

# 2. Работа с текстовым файлом
def create_and_manage_file(path, content):
    with open(path, 'w', encoding='utf-8') as f:
        f.write(content)
    print(f"Файл '{path}' создан и записан.")

    with open(path, 'r', encoding='utf-8') as f:
        print("Содержимое файла:")
        print(f.read())

# 3. Работа с JSON
def manage_json_file(path, name, age, occupation):
    data = {"Name": name, "Age": age, "Occupation": occupation}
    with open(path, 'w', encoding='utf-8') as f:
        json.dump(data, f, indent=4, ensure_ascii=False)
    print(f"JSON-файл '{path}' создан и записан.")

    with open(path, 'r', encoding='utf-8') as f:
        print("Содержимое JSON-файла:")
        print(f.read())

    os.remove(path)
    print(f"JSON-файл '{path}' удалён.")

# 4. Работа с XML
def manage_xml_file(path, name, age, occupation):
    person = ET.Element("Person")
    ET.SubElement(person, "Name").text = name
    ET.SubElement(person, "Age").text = str(age)
    ET.SubElement(person, "Occupation").text = occupation

    tree = ET.ElementTree(person)
    tree.write(path, encoding='utf-8', xml_declaration=True)
    print(f"XML-файл '{path}' создан и записан.")

    with open(path, 'r', encoding='utf-8') as f:
        print("Содержимое XML-файла:")
        print(f.read())

    os.remove(path)
    print(f"XML-файл '{path}' удалён.")

# 5. Работа с ZIP-архивом
def create_and_manage_zip(zip_path, file_path):
    if not os.path.exists(file_path):
        print(f"Файл '{file_path}' не существует.")
        return

    with zipfile.ZipFile(zip_path, 'w') as zf:
        zf.write(file_path)
        print(f"Файл '{file_path}' добавлен в архив '{zip_path}'.")

    print("Содержимое архива:")
    with zipfile.ZipFile(zip_path, 'r') as zf:
        for info in zf.infolist():
            print(f"  {info.filename}: {info.file_size} байт")

    os.remove(zip_path)
    print(f"Архив '{zip_path}' удалён.")

if __name__ == "__main__":
    main()
