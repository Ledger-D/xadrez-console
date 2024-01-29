using System;
using tabuleiro;
using xadrez;
namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {







            Console.WriteLine("============================\r\n++++++++++++++++++++++++++++\r\n\\  /        |\\\t|\\   __  ___\r\n \\/    /\\   | \\\t| \\ |\t   /\r\n /\\   /--\\  | / | / |==\t  /\r\n/  \\ / \t  \\ |/  | \\ |__\t /___\r\n+++++++++++++++++++++++++++++\r\n=============================");
            try
            {

                PartidaDeXadrez partida = new PartidaDeXadrez();
                while (!partida.terminada)
                {
                    Console.Clear();
                    Tela.imprimirTabuleiro(partida.tab);

                    Console.Write("Origem: ");
                    Posicao origem = Tela.lerPosicaoXadrez().toPosicao();
                    
                    bool[,] posicoesPossiveis = partida.tab.peca(origem).movimentosPosiveis();
                    
                    Console.Clear();
                    Tela.imprimirTabuleiro(partida.tab,posicoesPossiveis);

                    Console.Write("Destino: ");
                    Posicao destino = Tela.lerPosicaoXadrez().toPosicao();
                    partida.executaMovimento(origem, destino);

                }






            }
            catch (TabuleiroException e)
            {
                Console.WriteLine("Erro: " + e.Message);


            }


        }

    }
}