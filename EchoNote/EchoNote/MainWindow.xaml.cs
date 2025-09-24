using EchoNote.Models;
using EchoNote.Services;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

namespace EchoNote
{
    public partial class MainWindow : Window
    {
        private readonly DatabaseService _db = new();
        private Stopwatch _sw = new();
        private string _audioPath = ""; // TODO: тут позже появится путь к .wav (NAudio)

        public MainWindow()
        {
            InitializeComponent();
            _ = LoadTranscriptsAsync();
        }

        private async Task LoadTranscriptsAsync()
        {
            var items = await _db.GetTranscriptsAsync();
            TranscriptsList.ItemsSource = items;
        }

        private void StartRecording_Click(object sender, RoutedEventArgs e)
        {
            // Тут позже добавим старт записи через NAudio.
            // Пока просто запускаем таймер «записи».
            _sw.Restart();
            _audioPath = ""; // еще нет файла
            MessageBox.Show("Имитируем старт записи (позже добавим NAudio).");
        }

        private async void StopRecording_Click(object sender, RoutedEventArgs e)
        {
            _sw.Stop();

            var t = new Transcript
            {
                Title = string.IsNullOrWhiteSpace(TitleBox.Text) ? "Untitled" : TitleBox.Text,
                CreatedAt = DateTime.Now,
                DurationSeconds = _sw.Elapsed.TotalSeconds,
                AudioPath = _audioPath
            };

            try
            {
                await _db.AddTranscriptAsync(t);
                MessageBox.Show("Сохранено в SQLite.");
                TitleBox.Text = "";
                await LoadTranscriptsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сохранения: " + ex.Message);
            }
        }
    }
}
