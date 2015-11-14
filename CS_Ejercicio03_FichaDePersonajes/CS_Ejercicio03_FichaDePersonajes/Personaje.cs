﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace CS_Ejercicio03_FichaDePersonajes {
    class Personaje {
        private string nombreP, nombreJ, genero, raza, clase;
        private int[] atributos, tagsAtb;
        private bool[] habilidades;
        private int numTirada, habPorSeleccionar, ptosARepartirA;
        private string[] objetosMochila;

        public Personaje() {
            atributos = new int[10];
            tagsAtb = new int[10];
            objetosMochila = new string[4];
            habilidades = new bool[19];
        }
        public Personaje(string nombreP, string nombreJ, string genero, string raza, string clase, int[] atributos, int[] tagsAtb, bool[] habilidades, int numTirada, int habPorSeleccionar, int ptosARepartirA, string[] objetosMochila) {
            this.nombreP = nombreP;
            this.nombreJ = nombreJ;
            this.genero = genero;
            this.raza = raza;
            this.clase = clase;
            this.atributos = atributos;
            this.tagsAtb = tagsAtb;
            this.habilidades = habilidades;
            this.numTirada = numTirada;
            this.habPorSeleccionar = habPorSeleccionar;
            this.ptosARepartirA = ptosARepartirA;
            this.objetosMochila = objetosMochila;
        }
        public string escribirPersonaje() {
            string cadena = ""; int i;
            cadena += nombreP + ",";
            cadena += nombreJ + ",";
            cadena += genero + ",";
            cadena += raza + ",";
            cadena += clase + ",";
            for (i = 0; i < atributos.Length; i++) {
                cadena += atributos[i] + ".";
                if (i == atributos.Length - 1)
                    cadena += ",";
            }
            for (i = 0; i < tagsAtb.Length; i++) {
                cadena += tagsAtb[i] + ".";
                if (i == tagsAtb.Length - 1)
                    cadena += ",";
            }
            for (i = 0; i < habilidades.Length; i++) {
                cadena += habilidades[i] + ".";
                if (i == habilidades.Length - 1)
                    cadena += ",";
            }
            cadena += numTirada + ",";
            cadena += habPorSeleccionar + ",";
            cadena += ptosARepartirA + ",";
            for (i = 0; i < objetosMochila.Length; i++) {
                if (objetosMochila[i] != "")
                    cadena += objetosMochila[i] + ".";
                if (i == objetosMochila.Length - 1)
                    cadena += ",";
            }
            return cadena;
        }
        public static Personaje montarPersonaje(string cadena) {
            Personaje p = new Personaje(); string[] campos = cadena.Split(','); int i;
            string auxAtb , auxTags, auxHab, auxObjMoch;
            string[] auxAtb2, auxTags2, auxHab2, auxObjMoch2;
            if (campos.Length >= 11) {
                p.nombreP = campos[0];
                p.nombreJ = campos[1];
                p.genero = campos[2];
                p.raza = campos[3];
                p.clase = campos[4];
                auxAtb = campos[5];
                auxTags = campos[6];
                auxHab = campos[7];
                p.numTirada = Convert.ToInt32(campos[8]);
                p.habPorSeleccionar = Convert.ToInt32(campos[9]);
                p.ptosARepartirA = Convert.ToInt32(campos[10]);
                auxObjMoch = campos[11];

                auxAtb2 = auxAtb.Split('.');
                for (i = 0; i < auxAtb2.Length - 1; i++)
                    p.atributos[i] = Convert.ToInt32(auxAtb2[i]);

                auxTags2 = auxTags.Split('.');
                for (i = 0; i < auxTags2.Length - 1; i++)
                    p.tagsAtb[i] = Convert.ToInt32(auxTags2[i]);

                auxHab2 = auxHab.Split('.');
                for (i = 0; i < auxHab2.Length - 1; i++)
                    p.habilidades[i] = Convert.ToBoolean(auxHab2[i]);

                auxObjMoch2 = auxObjMoch.Split('.');
                for (i = 0; i < auxObjMoch2.Length - 1; i++)
                    p.objetosMochila[i] = auxObjMoch2[i];
            }
            return p;
        }
        public bool meHanModificado(bool[] habilidades, int[]atributos, string[] objetosMochila) {
            bool r = false; int i;
            if (!r) {
                for (i = 0; !r && i < this.atributos.Length; i++)
                    if (this.atributos[i] != atributos[i])
                        r = true;
            }
            if (!r) {
                for (i = 0; !r && i < this.habilidades.Length; i++)
                    if (this.habilidades[i] != habilidades[i])
                        r = true;
            }
            if (!r) {
                for (i = 0; !r && i < this.objetosMochila.Length; i++)
                    if (String.Equals(this.objetosMochila[i], objetosMochila[i]))
                        r = true;
            }
            return r;
        }
        // Getters
        public int getNumTirada() {
            return numTirada;
        }
        public int getHabPorSeleccionar() {
            return habPorSeleccionar;
        }
        public string[] getObjetosMochila() {
            return objetosMochila;
        }
        public int getPtosARepartirA() {
            return ptosARepartirA;
        }
        public string getNombreP() {
            return nombreP;
        }
        public string getNombreJ() {
            return nombreJ;
        }
        public string getGenero() {
            return genero;
        }
        public string getRaza() {
            return raza;
        }
        public string getClase() {
            return clase;
        }
        public bool[] getHabilidades() {
            return habilidades;
        }
        public int[] getAtributos() {
            return atributos;
        }
        public int[] getTagsAtb() {
            return tagsAtb;
        }
        // Setters.
        public void setNumTirada(int numTirada) {
            this.numTirada = numTirada;
        }
        public void setObjetosMochila(string[] objetosMochila) {
            this.objetosMochila = objetosMochila;
        }
        public void setHabPorSeleccionar(int habPorSeleccionar) {
            this.habPorSeleccionar = habPorSeleccionar;
        }
        public void setPtosARepartirA(int ptosARepartirA) {
            this.ptosARepartirA = ptosARepartirA;
        }
        public void setNombreP(String nombreP) {
            this.nombreP = nombreP;
        }
        public void setNombreJ(string nombreJ) {
            this.nombreJ = nombreJ;
        }
        public void setGenero(string genero) {
            this.genero = genero;
        }
        public void setRaza(string raza) {
            this.raza = raza;
        }
        public void setClase(string clase) {
            this.clase = clase;
        }
        public void setHabilidades(bool[] habilidades) {
            this.habilidades = habilidades;
        }
        public void setAtributos(int[] atributos) {
            this.atributos = atributos;
        }
    }
}
