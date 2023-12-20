

namespace tabuleiro
{
    class Peca
    {
        public Posicao posicao { get; set; }
        public Cor cor { get; protected set; }
        public int qteMovimento { get; protected set; }
        public Tabuleiro tabuleiro { get; protected set; }

    }
}
