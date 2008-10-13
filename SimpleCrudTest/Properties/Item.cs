using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCrudTest {
    public class Item {

        private int id;

        private String desc;

        private decimal preco;

        public int Id {
            get {
                return id;
            }
            set {
                id = value;
            }

        }
        public string Desc {
            get {
                return desc;
            }
            set {
                desc = value;
            }
        }
        public decimal Preco {
            get {
                return preco;
            }
            set {
                preco = value;
            }
        }
    }
}
