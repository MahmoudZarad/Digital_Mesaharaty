# 🌙 Ramadan Daily Messenger (The Digital Mesaharaty)

Welcome to the **Digital Mesaharaty**! This project is a specialized .NET 10 background service designed to break the cycle of repetitive daily messages. Using **Google Gemini AI**, it generates and broadcasts unique, heart-to-heart messages to a Telegram channel every morning at 8:00 AM (Cairo Time).

---

## 🛠️ The Tech Stack

Built with a focus on reliability and clean code:

* **Framework:** .NET 10
* **AI Engine:** [Google Gemini API](https://ai.google.dev/) - Our creative brain that ensures every message hits home.
* **Scheduler:** [Hangfire](https://www.hangfire.io/) - The "Boss" of background jobs; it ensures the "Mesaharaty" never misses a beat.
* **Communication:** [Telegram Bot API](https://core.telegram.org/bots) - Seamless delivery to your subscribers.
* **Reliability:** Implements the **Service Pattern** (`AiService` & `TelegramService`) for high maintainability.

---

## ✨ Key Features

1.  **Automated Daily Cron Job:** Scheduled to fire precisely at 8:00 AM.
2.  **Anti-Repetitive Prompting:** Uses a "Rotating Topics" logic combined with the current date to ensure the AI never repeats the same message twice.
3.  **Job Dashboard:** Real-time monitoring via the `/hangfire` dashboard to track, trigger, or retry jobs instantly.
4.  **Flexible Context:** The prompt is dynamically injected with Ramadan-specific themes (spiritual, social, or productivity tips).

---

## 🚀 Quick Setup

1.  **Clone** this repository to your local machine.
2.  **Configure** your `appsettings.json` with your credentials:
    * `GeminiApiKey`: Your Google AI Studio key.
    * `TelegramBotToken`: From BotFather.
    * `ChatId`: Your target Telegram Channel/Group ID.
3.  **Run** the application: `dotnet run`.
4.  **Monitor** via `localhost:{port}/hangfire`.

---

## 📋 Prompting Logic
The bot doesn't just "chat." It follows a structured prompt strategy:
- **Daily Context:** Today's date + Ramadan phase (Beginning, Middle, or Last 10 days).
- **Topic Rotation:** Cycles through a curated list of topics to keep the content fresh.
- **Tone Control:** Instructs the AI to be warm, authentic, and culturally relevant.

---

## 🤝 Contributing
Have a better prompt idea? Want to add an **Image Generation** service (e.g., DALL-E) to the messages? 
Pull Requests are more than welcome!

**Ramadan Mubarak & Happy Coding! 🌙💻**
