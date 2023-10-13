using System;
using System.Threading;

namespace Rivel.Framework {
    internal static class Helper {
        /// <summary>
        /// get user input as string value
        /// </summary>
        /// <param name="prompt">The prompt message to show to the user</param>
        /// <returns>The user input as string</returns>
        public static string GetUserInput(string prompt) {
            WriteLineIn(ConsoleColor.Cyan, prompt);
            return Console.ReadLine();
        }

        /// <summary>
        /// get user input as int value
        /// </summary>
        /// <param name="prompt">The prompt message to show to the user</param>
        /// <returns>The user input as int</returns>
        public static int GetUserInputAsInt(string prompt) {
            WriteLineIn(ConsoleColor.Cyan, prompt);
            try {
                int integerInput = int.Parse(Console.ReadLine());
                return integerInput;
            }
            catch (Exception e) {
                Print(e.Message, MessageType.ERROR);
                Print("Please enter a valid integer value", MessageType.INFO);
                return GetUserInputAsInt(prompt);
            }
        }

        /// <summary>
        /// do something after a certain amount of time (in milliseconds)
        /// </summary>
        /// <param name="callback">The function to be executed after the timeout</param>
        /// <param name="milliseconds">Time in  milliseconds</param>
        public static void SetTimeOut(Action callback, int milliseconds = 2000) {
            Thread.Sleep(milliseconds);
            callback();
        }

        /// <summary>
        /// print a message with a color based on the message type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message">Message to be printed</param>
        /// <param name="messageType">Type of the message</param>
        public static void Print<T>(T message, MessageType messageType = MessageType.Normal) {
            switch (messageType) {
                case MessageType.ERROR:
                    Audio.Play("Error");
                    WriteLineIn(ConsoleColor.Red, message);
                    break;
                case MessageType.SUCCESS:
                    Audio.Play("Success");
                    WriteLineIn(ConsoleColor.Green, message);
                    break;
                case MessageType.INFO:
                    WriteLineIn(ConsoleColor.Blue, message);
                    break;
                case MessageType.WARNING:
                    Audio.Play("Warning");
                    WriteLineIn(ConsoleColor.Yellow, message);
                    break;
                default:
                    WriteLineIn(ConsoleColor.DarkGray, message);
                    break;
            }
        }


        /// <summary>
        /// Print a greeting message to the user depending on the time of the day
        /// </summary>
        /// <returns>
        /// A greeting message of type string
        /// </returns>
        public static string GreetingMessage() {
            DateTime now = DateTime.Now;
            int currentHour = now.Hour;
            string greeting = currentHour > 0 && currentHour < 12 ? "Morning" : "Evening";
            return $"Good {greeting}!";
        }


        /// <summary>
        /// print a message in a line with specific color
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="color">Message text color</param>
        /// <param name="message">Message content</param>
        public static void WriteLineIn<T>(ConsoleColor color, T message) {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }


        /// <summary>
        /// print an inline message with specific color
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="color">Message text color</param>
        /// <param name="message">Message content</param>
        public static void WriteIn<T>(ConsoleColor color, T message) {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ResetColor();
        }


        /// <summary>
        /// repeats a charachter n times
        /// </summary>
        /// <param name="charachter">Charachter to be repeated</param>
        /// <param name="count">Number times the charachter should be repeated</param>
        /// <returns>A string of the repeated charachter</returns>
        public static string Repeat(char charachter, int count) => new string(charachter, count);

        // print a message with a decoration
        /// <summary>
        /// Print a message with a decoration
        /// </summary>
        /// <param name="message">Message content</param>
        /// <param name="symbol">Decoration symbol</param>
        /// <returns>A string of the decorated message</returns>
        public static string DecoratedText(string message, char symbol = '-') {
            return $"{Repeat(symbol, message.Length)}\n{message}\n{Repeat(symbol, message.Length)}";
        }


    }


    enum MessageType {
        Normal,
        ERROR,
        SUCCESS,
        INFO,
        WARNING
    }
}
