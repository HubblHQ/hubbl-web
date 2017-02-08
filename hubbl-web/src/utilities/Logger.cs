using System;

namespace hubbl.web {

    public class Logger {

        public static void writeLine(string message) {
            Console.WriteLine(message);
        }

        public static void write(string message) {
            Console.Write(message);
        }

    }
}