using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BLL
{
    public class Colores:ClaseMaestra
    {
        public int ColorId { get; set; }
        public string Descripcion { get; set; }

        ConexionDb conexion = new ConexionDb();

        public Colores() { }


        public override bool Insertar()
        {
            bool retornar = false;
            try
            {
                conexion.Ejecutar(String.Format("Insert into Colores(descipcion) Values('{0}')", this.Descripcion));
                retornar = true;
            }catch(Exception ex) { throw ex; }
            return retornar;
        }

        public override bool Editar()
        {
            bool retornar = true;
            try
            {
                conexion.Ejecutar(String.Format("Update Colores set Descripcion='{0}' where ColorId={1}",this.Descripcion, this.ColorId));
                retornar = true;
            }catch(Exception ex){ throw ex; }
            return retornar;
        }

        public override bool Eliminar()
        {
            bool retornar = true;
            try
            {
                conexion.Ejecutar(String.Format("Delete from Colores where ColorId={0}", this.ColorId));
                retornar = true;
            }catch(Exception ex) { throw ex; }
            return retornar;
        }

        public override bool Buscar(int IdBuscado)
        {
            DataTable dt = new DataTable();

            dt = conexion.ObtenerDatos(String.Format("Select * from Colores where ColoId=" + IdBuscado));
            if (dt.Rows.Count > 0)
            {
                this.ColorId = (int)dt.Rows[0]["ColorId"];
                this.Descripcion = dt.Rows[0]["Descripcion"].ToString();
            }
            return dt.Rows.Count > 0;
        }

        public override DataTable Listado(string Campos, string Condicion, string Orden)
        {
            string ordenar = "";
            if (!Orden.Equals(""))
                ordenar = "Orden by " + Orden;
            return conexion.ObtenerDatos("Select " + Campos + " from Colores where " + Condicion + ordenar);
        }
    }
}
