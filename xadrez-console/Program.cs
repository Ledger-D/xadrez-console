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
                Tabuleiro tab = new Tabuleiro(8, 8);

                tab.colocarPeca(new Rei(tab, Cor.Preta), new Posicao(9, 7));
                tab.colocarPeca(new Torre (tab, Cor.Preta), new Posicao(2, 7));


                Cor c = Cor.Branca;
                Tela.imprimirTabuleiro(tab);


            }
            catch(TabuleiroException e) {
                Console.WriteLine("Erro: "+ e.Message );

                
            }
 
 
 
 }
   
    }
}