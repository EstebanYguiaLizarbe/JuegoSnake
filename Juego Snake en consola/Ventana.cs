using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Juego_Snake_en_consola
{
    internal class Ventana
    {
        public string Titulo { get; set; }
        public int Ancho { get; set; }
        public int Altura { get; set; }
        public ConsoleColor ColorFondo { get; set; }
        public ConsoleColor ColorLetra { get; set; }
        public Point LimiteSuperior { get; set; }
        public Point LimiteInferior { get; set; }
        public int AreaVentana { get; set; }


        public Ventana(string titulo, int ancho, int altura, ConsoleColor colorFondo, ConsoleColor colorLetra, Point limiteSuperior, Point limiteInferior)
        {
            Titulo = titulo;
            Ancho = ancho;
            Altura = altura;
            ColorFondo = colorFondo;
            ColorLetra = colorLetra;
            LimiteSuperior = limiteSuperior;
            LimiteInferior = limiteInferior;
            AreaVentana = ((LimiteInferior.X - LimiteSuperior.X) - 1) * ((LimiteInferior.Y - LimiteSuperior.Y) - 1);
            Init();
        }

        public void Init()
        {
            Console.SetWindowSize(Ancho, Altura);
            Console.Title = Titulo;
            Console.CursorVisible = false;
            Console.BackgroundColor = ColorFondo;
            Console.Clear();
        }

        public void MostrarMenu(ref bool empezar, ref bool jugar, Snake snake)
        {
            Console.SetCursorPosition(LimiteInferior.X / 2, LimiteSuperior.Y + 2);
            Console.WriteLine("Menu");
            Console.SetCursorPosition((LimiteInferior.X / 2) - 8, LimiteSuperior.Y + 4);
            Console.WriteLine("Empezar (Presiona Enter)");
            Console.SetCursorPosition((LimiteInferior.X / 2) - 6, LimiteSuperior.Y + 6);
            Console.WriteLine("Salir (Presiona Esc)");

            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    //empezar = false;
                    jugar = true;
                    Console.Clear();
                    DibujarMarco();
                    snake.Init();
                }
                if (key.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }
            }
        }

        public void GameOver(string Texto)
        {
            Console.Clear();
            DibujarMarco();
            Console.SetCursorPosition(LimiteInferior.X / 2 - 10, LimiteSuperior.Y + 4);
            Console.WriteLine(Texto);
            Thread.Sleep(3000);
            Console.Clear();
            DibujarMarco();
        }

        public void DibujarMarco()
        {
            Console.ForegroundColor = ColorLetra;

            for (int i = LimiteSuperior.X; i < LimiteInferior.X; i++)
            {
                Console.SetCursorPosition(i, LimiteSuperior.Y);
                Console.WriteLine("═");
                Console.SetCursorPosition(i, LimiteInferior.Y);
                Console.WriteLine("═");
            }
            for (int i = LimiteSuperior.Y; i < LimiteInferior.Y; i++)
            {
                Console.SetCursorPosition(LimiteSuperior.X, i);
                Console.WriteLine("║");
                Console.SetCursorPosition(LimiteInferior.X, i);
                Console.WriteLine("║");
            }

            Console.SetCursorPosition(LimiteSuperior.X, LimiteSuperior.Y);
            Console.Write("╔");
            Console.SetCursorPosition(LimiteInferior.X, LimiteSuperior.Y);
            Console.Write("╗");
            Console.SetCursorPosition(LimiteSuperior.X, LimiteInferior.Y);
            Console.WriteLine("╚");
            Console.SetCursorPosition(LimiteInferior.X, LimiteInferior.Y);
            Console.Write("╝");
        }
    }
}
