using System;
using System.Media;


namespace Rivel.Framework
{
    internal class Audio
    {
        /// <summary>
        /// Play a sound from the Assets folder
        /// </summary>
        /// <param name="fileName">The name of the file to play</param>
        public static void Play(string fileName)
        {
            SoundPlayer player = new SoundPlayer
            {
                SoundLocation = AppDomain.CurrentDomain.BaseDirectory + $"\\Assets\\{fileName}.wav"
            };
            player.Play();
        }
    }
}
