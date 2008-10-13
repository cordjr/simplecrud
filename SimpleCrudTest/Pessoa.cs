using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SimpleCrudTest {
    public class Pessoa {
        private int id;
        private string nome;
        private DateTime? dataNasc;
        private IList<Item> items;

        public int Id {
            get {
                return id;
            }
            set {
                id = value;
            }
        }
        public string Nome {
            get {
                return nome;
            }
            set {
                nome = value;
            }
        }
        public DateTime? DataNasc {
            get {
                return dataNasc;
            }
            set {
                dataNasc = value;
            }
        }

        public IList<Item> Items {
            get {
                return items;
            }
            set {
                items = value;
            }
        }
        

    }
}
