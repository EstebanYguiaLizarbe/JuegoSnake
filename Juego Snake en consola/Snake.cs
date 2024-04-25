using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego_Snake_en_consola
{
    internal class Snake
    {
        enum Direccion
        {
            Derecha, Arriba, Abajo,Izquierda
        }
        public bool Vivo {  get; set; }
        public ConsoleColor ColorCabeza { get; set; }
        public ConsoleColor ColorCuerpo { get; set; }
        public Ventana VentanaC {  get; set; }
        public List<Point> Cuerpo {  get; set; }
        public Point Cabeza { get; set; }
        private Direccion _direcion;
        private bool _comiendo;
        public Comida Comida { get; set; }
        public int Puntaje { get; set; }
        public int PuntajeMax { get; set; }
        public Point CabezaInicial { get; set; }

        public Snake(Point posicion, ConsoleColor colorCabeza, ConsoleColor colorCuerpo, Ventana ventana, Comida comida)
        {
            ColorCabeza = colorCabeza;
            ColorCuerpo = colorCuerpo;
            VentanaC = ventana;
            Cabeza = posicion;
            Cuerpo = new List<Point>();
            Comida = comida;
            Puntaje = 0;
            PuntajeMax = 0;
            CabezaInicial = posicion;
        }
        public void Init()
        {
            Cuerpo.Clear();
            Cabeza = CabezaInicial;
            Vivo = true;
            IniciarCuerpo(2);
            Comida.ColocarComida(this);
            _direcion = Direccion.Derecha;
        }

        public void Mover()
        {
            Teclado();
            Point posicionCabezaAnterior = Cabeza;
            MoverCabeza();
            MoverCuerpo(posicionCabezaAnterior);
            ComidaS();
            if (Colisiones())
            {
                Morir();
                VentanaC.GameOver("G A M E  O V E R");
            };
        }
        public void IniciarCuerpo(int cantidad)
        {
            int x = Cabeza.X - 1;
            for (int i = 0; i < cantidad; i++)
            {
                Console.ForegroundColor = ColorCuerpo;
                Console.SetCursorPosition(x, Cabeza.Y);
                Console.WriteLine("▓");
                Cuerpo.Add(new Point(x, Cabeza.Y));
                x--;
            }
        }
        private void MoverCuerpo(Point posicionCabezaAnterior)
        {
            Console.ForegroundColor = ColorCuerpo;
            Console.SetCursorPosition(posicionCabezaAnterior.X, posicionCabezaAnterior.Y);
            Console.WriteLine("▓");
            Cuerpo.Insert(0, posicionCabezaAnterior); //Probar

            if(_comiendo)
            {
                _comiendo = false;
                return;
            }
                Console.SetCursorPosition(Cuerpo[Cuerpo.Count - 1].X, Cuerpo[Cuerpo.Count - 1].Y);
                Console.WriteLine(" ");
                Cuerpo.Remove(Cuerpo[Cuerpo.Count - 1]);
        }
        public void ComidaS()
        {
            if(Comida.Point == Cabeza)
            {
                if (!Comida.ColocarComida(this))
                {
                    Vivo = false;
                    VentanaC.GameOver("G A N A S T E"); //ganasrte Xd
                }
                _comiendo = true;
                Puntaje++;

                if(Puntaje > PuntajeMax)
                {
                    PuntajeMax = Puntaje;
                }
            }
        }

        public void MoverCabeza()
        {
            Console.ForegroundColor = ColorCabeza;
            Console.SetCursorPosition(Cabeza.X, Cabeza.Y);
            Console.WriteLine(" ");
            switch (_direcion)
            {
                case Direccion.Derecha:
                    Cabeza = new Point(Cabeza.X + 1, Cabeza.Y);
                    break;
                case Direccion.Izquierda:
                    Cabeza = new Point(Cabeza.X - 1, Cabeza.Y);
                    break;
                case Direccion.Abajo:
                    Cabeza = new Point(Cabeza.X, Cabeza.Y + 1);
                    break;
                case Direccion.Arriba:
                    Cabeza = new Point(Cabeza.X, Cabeza.Y - 1);
                    break;              
            }
            Marcos();
            Console.SetCursorPosition(Cabeza.X, Cabeza.Y);
            Console.WriteLine("█");
        }
        private void Teclado()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if(key.Key == ConsoleKey.RightArrow && _direcion != Direccion.Izquierda)
                {
                    _direcion = Direccion.Derecha;
                }
                if (key.Key == ConsoleKey.LeftArrow && _direcion != Direccion.Derecha)
                {
                    _direcion = Direccion.Izquierda;
                }
                if (key.Key == ConsoleKey.UpArrow && _direcion != Direccion.Abajo)
                {
                    _direcion = Direccion.Arriba;
                }
                if (key.Key == ConsoleKey.DownArrow && _direcion != Direccion.Arriba)
                {
                    _direcion = Direccion.Abajo;
                }
            }
        }
        private void Marcos()
        {
            if(Cabeza.X <= VentanaC.LimiteSuperior.X)
            {
                Cabeza = new Point(VentanaC.LimiteInferior.X - 1, Cabeza.Y);
            }
            if (Cabeza.X >= VentanaC.LimiteInferior.X)
            {
                Cabeza = new Point(VentanaC.LimiteSuperior.X + 1, Cabeza.Y);
            }
            if (Cabeza.Y <= VentanaC.LimiteSuperior.Y)
            {
                Cabeza = new Point(Cabeza.X, VentanaC.LimiteInferior.Y - 1);
            }
            if (Cabeza.Y >= VentanaC.LimiteInferior.Y)
            {
                Cabeza = new Point(Cabeza.X, VentanaC.LimiteSuperior.Y + 1);
            }
        }

        public bool Colisiones()
        {
            foreach(Point item in Cuerpo)
            {
                if(Cabeza == item)
                {
                    return true;
                }
            }
            return false;
        }

        public void Morir()
        {
            foreach(Point item in Cuerpo)
            {
                if(Cabeza == item)
                {
                    Vivo = false;
                    continue;
                }
                Console.SetCursorPosition(item.X, item.Y);
                Console.WriteLine("░");
                Thread.Sleep(150);

            }
            //Console.Clear();
            //Vivo = false;
        }
        public void PuntajeMostrar(int PuntoUno, int PuntoDos)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(VentanaC.LimiteSuperior.X + PuntoUno, VentanaC.LimiteSuperior.Y - 1);
            Console.WriteLine("Puntaje: " +Puntaje+ " ");
            Console.SetCursorPosition(VentanaC.LimiteSuperior.X + PuntoDos, VentanaC.LimiteSuperior.Y - 1);
            Console.WriteLine("Puntaje Max: " + PuntajeMax + " ");
        }
    }

}
