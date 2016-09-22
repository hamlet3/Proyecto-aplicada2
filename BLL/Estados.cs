using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BLL
{
    public class Estados: ClaseMaestra
    {
        public int EstadoId { get; set; }
        public string Descripcion { get; set; }

        ConexionDb conexion = new ConexionDb();


        public Estados() { }


        public override bool Insertar()
        {
            bool retornar = false;
            try
            {
                conexion.Ejecutar(String.Format("Insert into Estados(descipcion) Values('{0}')", this.Descripcion));
                retornar = true;
            }
            catch (Exception ex) { throw ex; }
            return retornar;
        }

        public override bool Editar()
        {
            bool retornar = true;
            try
            {
                conexion.Ejecutar(String.Format("Update Estados set Descripcion='{0}' where EstadoId={1}", this.Descripcion, this.EstadoId));
                retornar = true;
            }
            catch (Exception ex) { throw ex; }
            return retornar;
        }

        public override bool Eliminar()
        {
            bool retornar = true;
            try
            {
                conexion.Ejecutar(String.Format("Delete from Estados where EstadoId={0}", this.EstadoId));
                retornar = true;
            }
            catch (Exception ex) { throw ex; }
            return retornar;
        }

        public override bool Buscar(int IdBuscado)
        {
            DataTable dt = new DataTable();

            dt = conexion.ObtenerDatos(String.Format("Select * from Estados where EstadoId=" + IdBuscado));
            if (dt.Rows.Count > 0)
            {
                this.EstadoId = (int)dt.Rows[0]["EstadoId"];
                this.Descripcion = dt.Rows[0]["Descripcion"].ToString();
            }
            return dt.Rows.Count > 0;
        }

        public override DataTable Listado(string Campos, string Condicion, string Orden)
        {
            string ordenar = "";
            if (!Orden.Equals(""))
                ordenar = "Orden by " + Orden;
            return conexion.ObtenerDatos("Select " + Campos + " from Estados where " + Condicion + ordenar); ;
        }
    }
}
