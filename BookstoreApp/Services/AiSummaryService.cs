using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using BookstoreApp.Models;
using System.Text;
using System.Text.Json;

namespace BookstoreApp.Services
{
    public class AiSummaryService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public AiSummaryService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        // Generates short summary for a book using Gemini
        public async Task<string> GenerateSummaryAsync(Book book)
        {
            // 👉 Read Gemini key (changed from OpenAI)
            var apiKey = _configuration["Gemini:ApiKey"];

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                return "AI summary is not configured.";
            }

            // Build prompt based on available data (same as before)
            var prompt = BuildPrompt(book);

            // 👉 Gemini request format
            var requestBody = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new { text = prompt }
                        }
                    }
                }
            };

            var requestJson = JsonSerializer.Serialize(requestBody);

            // 👉 Gemini endpoint (free tier model)
            var url =
                $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={apiKey}";

            var response = await _httpClient.PostAsync(
                url,
                new StringContent(requestJson, Encoding.UTF8, "application/json")
            );

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return error;
            }


            var responseJson = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(responseJson);

            // 👉 Gemini response parsing
            var text = doc.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();

            return text?.Trim() ?? "No summary generated.";
        }

        // Builds prompt depending on available fields
        private string BuildPrompt(Book book)
        {
            return
        $"""
You are generating a short professional book description.

First, try to use your known public knowledge about this book
using the given title and author.

If you can confidently recognize this book (by title and author),
write a short factual description.

If you cannot confidently find known information about this book,
then start your answer exactly with this line:

Online information not found. Based on the title and author, here is a reasonable guess:

After that line, write a short guessed description.

Book details:
Title: {book.Title}
Author: {book.Author}
Genre: {book.Genre}

Keep the description short (3–4 lines).
Do not mention AI, training data or model.
""";
        }

    }
}
