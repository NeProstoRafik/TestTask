﻿using System;
using System.IO;

namespace TestTask
{
    public class ReadOnlyStream : IReadOnlyStream
    {
        private Stream _localStream;
        private StreamReader reader = null;
        /// <summary>
        /// Конструктор класса. 
        /// Т.к. происходит прямая работа с файлом, необходимо 
        /// обеспечить ГАРАНТИРОВАННОЕ закрытие файла после окончания работы с таковым!
        /// </summary>
        /// <param name="fileFullPath">Полный путь до файла для чтения</param>
        public ReadOnlyStream(string fileFullPath)
        {
            //using (_localStream = File.OpenRead(fileFullPath)) ;
            _localStream = File.OpenRead(fileFullPath);
            // IsEof = true;                   
        }

        /// <summary>
        /// Флаг окончания файла.
        /// </summary>
        public bool IsEof
        {
            get; // TODO : Заполнять данный флаг при достижении конца файла/стрима при чтении  +
            private set;
        }

        public void Dispose()
        {
            _localStream.Dispose();
        }

        /// <summary>
        /// Ф-ция чтения следующего символа из потока.
        /// Если произведена попытка прочитать символ после достижения конца файла, метод 
        /// должен бросать соответствующее исключение
        /// </summary>
        /// <returns>Считанный символ.</returns>
        public char ReadNextChar()
        {
            char nextChar = default;
            if (IsEof == true)
            {
                throw new Exception("конец файла");
            }
            if (reader == null)
            {
                reader = new StreamReader(_localStream);
            }
            int nextCharInt = reader.Read();
            if (nextCharInt != -1)
            {
                nextChar = (char)nextCharInt;
                //  Console.WriteLine($"Считанный символ: {nextChar}");
                return nextChar;
            }
            else
            {               
                IsEof = true;
                Dispose();
            }
            
            return default;
            // TODO : Необходимо считать очередной символ из _localStream

        }

        /// <summary>
        /// Сбрасывает текущую позицию потока на начало.
        /// </summary>
        public void ResetPositionToStart()
        {
            if (_localStream == null)
            {
                IsEof = true;
                return;
            }

            _localStream.Position = 0;
            IsEof = false;
        }
    }
}
