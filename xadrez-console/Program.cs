using System;
using tabuleiro;
using xadrez;
namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {




            try
            {


                Console.WriteLine("============================\r\n++++++++++++++++++++++++++++\r\n\\  /        |\\\t|\\   __  ___\r\n \\/    /\\   | \\\t| \\ |\t   /\r\n /\\   /--\\  | / | / |==\t  /\r\n/  \\ / \t  \\ |/  | \\ |__\t /___\r\n+++++++++++++++++++++++++++++\r\n=============================");

                PartidaDeXadrez partida = new PartidaDeXadrez();
                while (!partida.terminada)
                {
                    try
                    {
                        Console.Clear();
                        Tela.imprimirPartida(partida);

                        Console.Write("\nOrigem: ");
                        Posicao origem = Tela.lerPosicaoXadrez().toPosicao();
                        partida.validarPosicaoDeOrigem(origem);

                        bool[,] posicoesPossiveis = partida.tab.peca(origem).movimentosPosiveis();

                        Console.Clear();
                        Tela.imprimirTabuleiro(partida.tab, posicoesPossiveis);

                        Console.Write("Destino: ");
                        Posicao destino = Tela.lerPosicaoXadrez().toPosicao();
                        partida.validarPosicaoDeDestino(origem, destino);
                        partida.realizaJogada(origem, destino);

                    }




                    catch (TabuleiroException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();

                    }
                }
            }


            catch (TabuleiroException e)
            {
                Console.WriteLine("Erro: " + e.Message);


            }


        }


    }
}