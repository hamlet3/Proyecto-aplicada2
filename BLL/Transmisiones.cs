using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BLL
{
    public class Transmisiones : ClaseMaestra
    {
        public int TransmisionId { get; set; }
        public string Descripcion { get; set; }

        ConexionDb conexion = new ConexionDb();

        public Transmisiones()
        {

        }

        public override bool Insertar()
        {
            bool retornar = false;
            try
            {
                conexion.Ejecutar(String.Format("Insert into Transmisiones(Descripcion) Values('{0}')", this.Descripcion));
                retornar = true;
            }catch(Exception ex) { throw ex; }
            return retornar;
        }

        public override bool Editar()
        {
            bool retornar = false;
            try
            {
                conexion.Ejecutar(String.Format("Update Transmisiones set Descripcion='{0}'  where TransmisionId={1}",this.Descripcion,this.TransmisionId));
                retornar = true;
            }catch(Exception ex) { throw ex; }
            return retornar;
        }

        public override bool Eliminar()
        {
            bool retornar = false;
            try
            {
                conexion.Ejecutar(String.Format("Delete from Transmisiones where TransmisionId={0}", this.TransmisionId));
                retornar = true;
            }catch(Exception ex) { throw ex; }
            return retornar;
        }

        public override bool Buscar(int IdBuscado)
        {
            DataTable dt = new DataTable();

            dt = conexion.ObtenerDatos("Select * from Transmisiones where TransmisionId=" + IdBuscado);
            if (dt.Rows.Count > 0)
            {
                this.TransmisionId = (int)dt.Rows[0]["TransmisionId"];
                this.Descripcion = dt.Rows[0]["Descripcion"].ToString();
            }
            return dt.Rows.Count > 0;
        }

        public override DataTable Listado(string Campos, string Condicion, string Orden)
        {
            string ordenar = "";
            if (!Orden.Equals(""))
                ordenar = "Order by " + Orden;
            return conexion.ObtenerDatos("Select " + Campos + " from Transmisiones where " + Condicion + Orden);
        }
    }
}
