using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using LibraryManagement.Models;

namespace LibraryManagement.Managers
{
    public class ReaderManager
    {
        public List<Reader> Readers { get; private set; }
        private const string FilePath = "readers.json";
        private int _lastReaderId = 0;

        public ReaderManager()
        {
            Readers = new List<Reader>();
            LoadReaders();
        }

        public void AddReader(Reader reader)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader), "Читатель не может быть null.");

            ValidateReader(reader);

            reader.Id = (++_lastReaderId).ToString();
            Readers.Add(reader);
            SaveReaders();
        }

        public void RemoveReader(string readerId)
        {
            var reader = GetReader(readerId);
            if (reader == null)
                throw new KeyNotFoundException($"Читатель с ID {readerId} не найден.");

            Readers.Remove(reader);
            SaveReaders();
        }

        public Reader GetReader(string readerId)
        {
            return Readers.Find(r => r.Id == readerId);
        }

        public List<Reader> GetAllReaders()
        {
            return Readers;
        }

        private void LoadReaders()
        {
            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                Readers = JsonSerializer.Deserialize<List<Reader>>(json) ?? new List<Reader>();
                _lastReaderId = Readers
                    .Select(r => int.TryParse(r.Id, out int id) ? id : 0)
                    .DefaultIfEmpty(0)
                    .Max();
            }
        }

        private void SaveReaders()
        {
            string json = JsonSerializer.Serialize(Readers, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);
        }

        private void ValidateReader(Reader reader)
        {
            if (!IsValidName(reader.Name))
                throw new ArgumentException("Имя должно содержать только буквы русского или английского алфавита.");

            if (!IsValidEmail(reader.Email))
                throw new ArgumentException("Email должен быть на английском языке с использованием цифр и допустимых символов.");
        }

        private bool IsValidName(string name)
        {
            // Проверяем, что имя содержит только буквы русского или английского алфавита
            return !string.IsNullOrEmpty(name) && name.All(c =>
                char.IsLetter(c) &&
                ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || (c >= 'А' && c <= 'я') || c == 'Ё' || c == 'ё'));
        }

        private bool IsValidEmail(string email)
        {
            // Email должен быть на английском языке с использованием цифр и символов @._-+
            return !string.IsNullOrEmpty(email) &&
                email.All(c =>
                    (c >= 'A' && c <= 'Z') || // Заглавные английские буквы
                    (c >= 'a' && c <= 'z') || // Строчные английские буквы
                    char.IsDigit(c) || // Цифры
                    c == '@' || c == '.' || c == '_' || c == '-' || c == '+') &&
                email.Contains("@") && // Обязательное наличие символа "@"
                email.IndexOf("@") > 0 && // Убедимся, что "@" не стоит в начале
                email.IndexOf(".") > email.IndexOf("@") + 1; // Убедимся, что есть точка после "@"
        }
    }
}
