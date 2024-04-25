using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Juego_Snake_en_consola
{
    internal class Comida
    {
        public ConsoleColor ColorComida { get; set; }
        public Ventana VentanaC { get; set; }
        public Point Point {  get; set; }
        public Comida(ConsoleColor colorComida, Ventana ventanaC)
        {
            ColorComida = colorComida;
            VentanaC = ventanaC;

        }
        public void Color()
        {
            Console.ForegroundColor = ColorComida;
            Console.SetCursorPosition(Point.X, Point.Y);
            Console.WriteLine("█");
        }
        public bool ColocarComida(Snake snakeParam)
        {
            int longSerpiente = snakeParam.Cuerpo.Count + 1;
            if ((VentanaC.AreaVentana - longSerpiente) <= 0)
            {
                return false;
            }
            

            Random random = new Random();
            int x = random.Next(VentanaC.LimiteSuperior.X + 1,VentanaC.LimiteInferior.X);
            int y = random.Next(VentanaC.LimiteSuperior.Y + 1, VentanaC.LimiteInferior.Y);

            Point = new Point(x, y);

            foreach(Point param in snakeParam.Cuerpo)
            {
                if ((param.X == x && param.Y == y) || (snakeParam.Cabeza.X == x && snakeParam.Cabeza.Y == y))
                {
                    if (ColocarComida(snakeParam))
                    {
                        //Console.Write("hola");
                        return true;
                    }
                }
            }
            
            Color();
            return true;
        }
     }
}
