using EchoNote.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace EchoNote.Services
{
    // Сервіс для роботи з базою даних SQLite
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService()
        {
            // Шлях до файлу бази даних у локальній папці користувача
            var dbPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "echonote.db3");

            // Створюємо або підключаємося до бази даних
            _database = new SQLiteAsyncConnection(dbPath);

            // Створюємо таблицю Transcript, якщо вона ще не існує
            _database.CreateTableAsync<Transcript>().Wait();
        }

        // Додавання нового запису (транскрипту)
        public Task<int> AddTranscriptAsync(Transcript transcript)
        {
            return _database.InsertAsync(transcript);
        }

        // Отримання всіх транскриптів
        public Task<List<Transcript>> GetTranscriptsAsync()
        {
            return _database.Table<Transcript>().ToListAsync();
        }
    }
}
