﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;
using tabuleiro;

namespace xadrez
{
    internal class PartidaDeXadrez
    {
        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public bool xeque { get; private set; }

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            xeque = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }
        public Peca executaMovimento(Posicao origem, Posicao destino)
        {

            Peca p = tab.retirarPeca(origem);
            p.incrementarQtfMovimento();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);


            }
            return pecaCapturada;
        }
        public void realizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = executaMovimento(origem, destino);
            if (estaEmXeque(jogadorAtual))
            {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Voce não pode se colocar em xeque!");
            }
            if (estaEmXeque(adversaria(jogadorAtual)))
            {

                xeque = true;
            }
            else { xeque = false; }
            turno++;
            mudaJogador();
        }
        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = tab.retirarPeca(destino);
            p.dencrementarQtfMovimento();
            if (pecaCapturada != null)
            {
                tab.colocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            tab.colocarPeca(p, origem);
        }

        public void validarPosicaoDeOrigem(Posicao pos)
        {
            if (tab.peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida! ");
            }
            if (jogadorAtual != tab.peca(pos).cor)
            {
                throw new TabuleiroException("A peça  de origem escolhida não é sua! ");
            }
            if (!tab.peca(pos).existeMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possiveis para a peca de origem escolhida!! ");

            }
        }

        public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
        {

            if (!tab.peca(origem).podeMoverPara(destino))
            {
                throw new TabuleiroException("Posição de destino invalida!");

            }


        }
        private void mudaJogador()
        {
            if (jogadorAtual == Cor.Branca)
            {
                jogadorAtual = Cor.Preta;
            }
            else
            {
                jogadorAtual = Cor.Branca;
            }

        }
        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturadas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }
        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {

                if (x.cor == cor)
                {
                    aux.Add(x);
                }

            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }
        private Cor adversaria(Cor cor)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }

        }
        private Peca rei(Cor cor)
        {
            foreach (Peca x in pecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }
        public bool estaEmXeque(Cor cor)
        {
            foreach (Peca x in pecasEmJogo(adversaria(cor)))
            {
                Peca R = rei(cor);
                if (R == null)
                {
                    throw new TabuleiroException("Nao tem rei da cor " + cor + " no tabuleiro");
                }
                bool[,] mat = x.movimentosPosiveis();
                if (mat[R.posicao.linha, R.posicao.coluna])
                {
                    return true;
                }

            }
            return false;
        }
        public void colocarNovapeca(char coluna, int linha, Peca peca)
        {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);





        }
        private void colocarPecas()
        {

            colocarNovapeca('c', 1, new Torre(tab, Cor.Branca));
            colocarNovapeca('c', 2, new Torre(tab, Cor.Branca));
            colocarNovapeca('d', 2, new Torre(tab, Cor.Branca));
            colocarNovapeca('e', 2, new Torre(tab, Cor.Branca));
            colocarNovapeca('e', 1, new Torre(tab, Cor.Branca));
            colocarNovapeca('d', 1, new Rei(tab, Cor.Branca));

            colocarNovapeca('c', 8, new Torre(tab, Cor.Preta));
            colocarNovapeca('c', 7, new Torre(tab, Cor.Preta));
            colocarNovapeca('d', 8, new Rei(tab, Cor.Preta));
            colocarNovapeca('d', 7, new Torre(tab, Cor.Preta));
            colocarNovapeca('e', 7, new Torre(tab, Cor.Preta));
            colocarNovapeca('e', 8, new Torre(tab, Cor.Preta));

        }
    }
}
