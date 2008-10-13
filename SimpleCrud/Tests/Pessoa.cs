using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SimpleCrud.Test {
    public class Pessoa {
        private int id;
        private string nome;
        private DateTime? dataNasc;
        private IList<Item> items;

        public bool Equals(Pessoa obj) {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            return obj.id == id && Equals(obj.nome, nome) && obj.dataNasc.Equals(dataNasc) && Equals(obj.items, items);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            if (obj.GetType() != typeof (Pessoa)) {
                return false;
            }
            return Equals((Pessoa) obj);
        }

        public override int GetHashCode() {
            unchecked {
                int result = id;
                result = (result*397) ^ (nome != null ? nome.GetHashCode() : 0);
                result = (result*397) ^ (dataNasc.HasValue ? dataNasc.Value.GetHashCode() : 0);
                result = (result*397) ^ (items != null ? items.GetHashCode() : 0);
                return result;
            }
        }

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
