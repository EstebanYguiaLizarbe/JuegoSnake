using Juego_Snake_en_consola;
using System.Drawing;

Ventana ventana;
Snake snake;
Comida comida;

bool empezar = true;
bool jugar = false;

void Iniciar()
{
    ventana = new Ventana("Snake", 65, 20, ConsoleColor.Black, ConsoleColor.White, new Point(5, 3), new Point(59, 18));
    ventana.DibujarMarco();
    comida = new Comida(ConsoleColor.Green, ventana);
    snake = new Snake(new Point(8, 5), ConsoleColor.Red, ConsoleColor.Blue, ventana, comida);
    //snake.IniciarCuerpo(2);
    //comida.ColocarComida(snake);
}

void MoverInicial()
{
    while (empezar)
    {
        ventana.MostrarMenu(ref empezar, ref jugar, snake);
        //empezar = false;
        while (jugar) {
            snake.PuntajeMostrar(0, 15);
            snake.Mover();
            if (!snake.Vivo)
            {
                jugar = false;
                snake.Puntaje = 0;
            }
            Thread.Sleep(100);
        }
        Thread.Sleep(100);
    }
}

Iniciar();
MoverInicial();
//Console.ReadKey();

