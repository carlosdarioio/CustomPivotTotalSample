using DevExpress.XtraPivotGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomPivotTotalSample
{
    public partial class Form1 : Form
    {
        private DataSet xDataSet;
        public Form1()
        {
            InitializeComponent();
            //Llamando funciones
            CrearTablaManual();           
            BindData();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }



        // Creando Tabla
        private void CrearTablaManual()
        {
            // Crenado Nueva DataTable de facturas
            DataTable xDT = new DataTable("Facturas");
            DataColumn dataColumns;
            DataRow xDataRow;

            // Creando y nombrando columnas.    
            dataColumns = new DataColumn();
            dataColumns.DataType = typeof(String);
            dataColumns.ColumnName = "Factura";
            dataColumns.Caption = "Factura";
            dataColumns.AutoIncrement = false;
            dataColumns.ReadOnly = false;
            dataColumns.Unique = false;            
            xDT.Columns.Add(dataColumns);

            
            dataColumns = new DataColumn();
            dataColumns.DataType = typeof(String);
            dataColumns.ColumnName = "Producto";
            dataColumns.Caption = "Producto";
            dataColumns.AutoIncrement = false;
            dataColumns.ReadOnly = false;
            dataColumns.Unique = false;            
            xDT.Columns.Add(dataColumns);

            
            dataColumns = new DataColumn();
            dataColumns.DataType = typeof(double);
            dataColumns.ColumnName = "Costo";
            dataColumns.Caption = "Costo";
            dataColumns.ReadOnly = false;
            dataColumns.Unique = true;            
            xDT.Columns.Add(dataColumns);

            
            dataColumns = new DataColumn();
            dataColumns.DataType = typeof(double);
            dataColumns.ColumnName = "Venta";
            dataColumns.Caption = "Venta";
            dataColumns.ReadOnly = false;
            dataColumns.Unique = false;            
            xDT.Columns.Add(dataColumns);
            
            // Creando DataSet  
            xDataSet = new DataSet();
            xDataSet.Tables.Add(xDT);

            //Creando valores
            xDataRow = xDT.NewRow();
            xDataRow["Factura"] = "150";
            xDataRow["Producto"] = "A20";
            xDataRow["Costo"] = 4000;
            xDataRow["Venta"] = 6000;
            xDT.Rows.Add(xDataRow);

            xDataRow = xDT.NewRow();
            xDataRow["Factura"] = "150";
            xDataRow["Producto"] = "A10";
            xDataRow["Costo"] = 3000;
            xDataRow["Venta"] = 4800;
            xDT.Rows.Add(xDataRow);

            xDataRow = xDT.NewRow();
            xDataRow["Factura"] = "161";
            xDataRow["Producto"] = "Portatil";
            xDataRow["Costo"] = 3300;
            xDataRow["Venta"] = 4500;
            xDT.Rows.Add(xDataRow);

            xDataRow = xDT.NewRow();
            xDataRow["Factura"] = "161";
            xDataRow["Producto"] = "Tablet";
            xDataRow["Costo"] = 800;
            xDataRow["Venta"] = 2000;
            xDT.Rows.Add(xDataRow);
        }        
        private void BindData()
        {            
            DataColumn custCol = xDataSet.Tables["Facturas"].Columns["id"];            
            BindingSource bs = new BindingSource();
            bs.DataSource = xDataSet.Tables["Facturas"];            
            pivotGridControl1.DataSource = bs;
        }
        /*
         custom grand total pivot grid devexpress /  Personalizar Grand Total de un PivotGrid DevExpress c#
         
         Para poder mostrar este ejemplo de Devexpress c# primero crearemos una datatable manual
         Ponerle valores y asignarsela a un pivotGridControl Para poder Personalizar el Total por columna de la tabla

            Por ejemplo en la columna de costos mostraremos el porcentaje total que representa el costo por cada factura (Venta/costo)
            mientras que en la de ventas mostraremos la ganacia total brindada por cada factura (Venta-costo)

             */

            //Funcion para personalizar totales
        private void pivotGridControl1_CustomCellValue(object sender, DevExpress.XtraPivotGrid.PivotCellValueEventArgs e)
        {
            //Venta
            double summaryValue1 = Convert.ToDouble(e.GetFieldValue(pivotGridField3).ToString());
            //Costo
            double summaryValue2 = Convert.ToDouble(e.GetFieldValue(pivotGridField1).ToString());
            //Costo
            if (e.DataField.Name == "pivotGridField1" && e.Value != null)
            {
                
                //asignando el tipo de valor a modificar (Total)
                if (e.RowValueType == PivotGridValueType.Total) //GrandTotal
                {
                    //obtendiendo valores                    
                    e.Value = summaryValue1 / summaryValue2;
                }
            }

            //Venta
            if (e.DataField.Name == "pivotGridField3" && e.Value != null)
            {
                //asignando el tipo de valor a modificar (Total)
                if (e.RowValueType == PivotGridValueType.Total) //GrandTotal
                {                    
                    e.Value = summaryValue1 - summaryValue2;
                }
            }
        }
    }
}
