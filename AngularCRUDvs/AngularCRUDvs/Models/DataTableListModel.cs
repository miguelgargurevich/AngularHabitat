using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using AngularCRUDvs.Entidades;

namespace AngularCRUDvs.Models
{
    public class DataTableListModel
    {
        public UsersModel user { get; set; }
        public List<string> cabeceras { get; set; }
        public List<int> mesesList { get; set; }
        //public List<int>? aniosList { get; set; }

        //public List<string>? Matriz { get; set; }
        public string MatrizString { get; set; }

        public Dictionary<string, string> Totales { get; set; }
        public List<Recibo> recibos { get; set; }
        public Recibo recibo { get; set; }
        //public Unidad? unidad { get; set; }
    }
}