using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL
{
   public class Motores:ClaseMaestra
    {
        public int MotorId { get; set; }
        public string Descripcion { get; set; }
        ConexionDb conexion = new ConexionDb();

        public Motores(int motorId, string descripcion)
        {
            this.MotorId = motorId;
            this.Descripcion = descripcion;
        }

        public Motores() { }

        public override bool Insertar()
        {
            bool retorno = false;
            try
            {
                conexion.Ejecutar(String.Format("Insert into Motores(Descripcion) Values('{0}')", this.Descripcion));
                retorno = true;
            }catch(Exception ex) { throw ex; }
            return retorno;
        }

        public override bool Editar()
        {
            bool retorno = false;
            try
            {
                conexion.Ejecutar(String.Format("Update Motores set Descripcion='{0} where MotorId={1}'", this.Descripcion, this,MotorId));
                retorno = true;
            }catch(Exception ex) { throw ex; }
            return retorno;
        }

        public override bool Eliminar()
        {
            bool retorno = false;
            try
            {
                conexion.Ejecutar(String.Format("Delete from Motores where MotorId={0}",this.MotorId));
            }catch(Exception ex) { throw ex; }
            return retorno;
        }

        public override bool Buscar(int IdBuscado)
        {
            DataTable dt =new DataTable();
            dt = conexion.ObtenerDatos("Select * from Motores where MotorId=" + IdBuscado);

            if (dt.Rows.Count>0)
            {
                this.MotorId = (int)dt.Rows[0]["MotorId"];
                this.Descripcion = dt.Rows[0]["Descripcion"].ToString();
            }
            return dt.Rows.Count > 0;
        }

        public override DataTable Listado(string Campos, string Condicion, string Orden)
        {
            string ordenar = "";
            if (!Orden.Equals(""))
                ordenar = "order by " + Orden;

            return conexion.ObtenerDatos("Select " + Campos + " from Motores where " + Condicion + Orden);
        }
    }
}
